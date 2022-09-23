using IDPService.Business.Interfaces;
using IDPService.Data.Interfaces;
using IDPService.Business.Interfaces.Gateways;
using IDPService.Entities.Entities;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using IDPService.Business.Gateways;
using Microsoft.Extensions.Configuration;

namespace IDPService.Business.Services
{
    public class DocumentService : IDocumentService
    {
        IDocumentRepository _DocumentRepository;
        IBlobGateway _BlobGateway;
        IQueueGateway _Queuegateway;
        ICategorySetRepository _CategorySetRepository;
        ICategoryRepository _CategoryRepository;
        IConfiguration _Configuration;
        
        public DocumentService(IDocumentRepository DocumentRepository, IBlobGateway BlobGateway, IQueueGateway Queuegateway, ICategorySetRepository CategorySetRepository,
        ICategoryRepository CategoryRepository, IConfiguration Configuration)
        {
            this._DocumentRepository = DocumentRepository;
            this._BlobGateway = BlobGateway;
            this._Queuegateway = Queuegateway;
            this._CategorySetRepository = CategorySetRepository;
            this._CategoryRepository = CategoryRepository;
            this._Configuration = Configuration;
        }
        public IEnumerable<DocumentEntity> GetAll()
        {
            return _DocumentRepository.GetAll();
        }

        public DocumentEntity Save(DocumentEntity Document)
        {
            _DocumentRepository.Save(Document);
            return Document;
        }

        public DocumentEntity Update(string id, DocumentEntity Document)
        {
            return _DocumentRepository.Update(id, Document);
        }

        public bool Delete(string id)
        {
            return _DocumentRepository.Delete(id);
        }

        public bool UploadInvoiceFiles(List<IFormFile> files, string createdBy, string pageRange, string batchId = null)
        {
            string categorySetId = "";
            string categoryId = "";

            CategorySet categorySet = _CategorySetRepository.GetBuiltInCategorySetByName("Built-In-Invoice");

            if (categorySet == null)
            {
                categorySetId = _CategorySetRepository.SaveBuiltInCategorySet(new CategorySet()
                {
                    Name = "Built-In-Invoice"
                });
            }
            else
            {
                categorySetId = categorySet.Id;
            }

            Category category = _CategoryRepository.GetBuiltInCategoryByName("Built-In-Invoice");

            if (category == null)
            {
                categoryId = _CategoryRepository.Save(new Category()
                {
                    CategorySetId = categorySetId,
                    Name = "Built-In-Invoice"
                });
            }
            else
            {
                categoryId = category.Id;
            }

            UploadFile uploadFiles = new UploadFile() { Files = files, CreatedBy = createdBy, CategorySetId = categorySetId };

            // _fileValidator.ValidateAndThrow(uploadFiles);

            string containerName = _Configuration["Blob:ContainerName"];

            foreach (IFormFile file in files)
            {
                IFormFile fileMod = file;
                try
                {
                    var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    PdfDocument pdfDocument = PdfReader.Open(memoryStream, PdfDocumentOpenMode.Modify);

                    int i = 0;
                    while (i < pdfDocument.Pages.Count)
                    {
                        if (pdfDocument.Pages[i].Contents.Elements.Count == 0)
                        {
                            pdfDocument.Pages.RemoveAt(i);
                            i -= 1;
                        }
                        i += 1;
                    }

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var stream = new MemoryStream();
                    pdfDocument.Save(stream, false);
                    pdfDocument.Close();
                    byte[] docBytes = stream.ToArray();
                    fileMod = new FormFile(stream, 0, docBytes.Length, "name", file.FileName);
                }
                catch (InvalidOperationException)
                {
                    fileMod = file;
                }

                var uploadFileName = Guid.NewGuid().ToString();

                string uploadedUrl = _BlobGateway.UploadFileAsyn(fileMod, uploadFileName, containerName).Result;

                if (!string.IsNullOrEmpty(uploadedUrl))
                {
                    DocumentEntity document = GenerateDocument(file, uploadFileName, uploadedUrl, createdBy, categoryId, categorySetId, batchId, pageRange);

                    Save(document);
                    // if (!string.IsNullOrEmpty(categoryId))
                    // {
                    //     document.SelectedClassificationType = _category.GetById(categoryId);
                    // }

                    // UpdateStatus(document.Id, DocumentStatus.Ingested);
                    _Queuegateway.Send(document, EventTypeEnum.formRecognizerAnalyzeInvoiceForm);
                    //_eventGridGateway.RaiseEvent("AnalyzeInvoiceFormEndPoint", "AnalyzeInvoiceFormAccessKey", document, "formRecognizerAnalyzeInvoiceForm");
                }
            }

            return true;
        }

        private DocumentEntity GenerateDocument(IFormFile file, string uploadFileName, string uploadedUrl, string createdBy, string classId, string categorySetId, string batchId = null, string pageRange = "")
        {
            var document = new DocumentEntity()
            {
                DocId = uploadFileName,
                FileName = file.FileName,
                ContentType = file.ContentType,
                CreatedBy = createdBy,
                SubscriptionId = "60cc1c99bf1bb610383af80f",
                // CreateDatetime = DateTime.UtcNow,
                FilePath = uploadedUrl,
                // ClassificationTypes = new List<ClassificationScore>(),
                // SelectedClassificationId = string.IsNullOrEmpty(classId) ? null : classId,
                // Status = string.IsNullOrEmpty(classId) ? DocumentStatus.Ingested.ToString() : DocumentStatus.Classified.ToString(),
                // UpdateDateTime = DateTime.UtcNow,
                UpdatedBy = "",
                // StatusHistory = new List<StatusHistory>(),
                // isManuallyClassified = string.IsNullOrEmpty(classId),
                CategorySetId = categorySetId,
            };

            return document;
        }

    }
}
