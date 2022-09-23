using System;
using System.Collections.Generic;
using IDPService.Business.Interfaces;
using IDPService.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace IDPService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        IDocumentService _DocumentService;
        public DocumentController(IDocumentService DocumentService)
        {
            _DocumentService = DocumentService;
        }

        // GET: api/Document
        [HttpGet]
        public ActionResult<IEnumerable<DocumentEntity>> Get()
        {
            return Ok(_DocumentService.GetAll());
        }

        [HttpPost]
        public ActionResult<DocumentEntity> Save(DocumentEntity Document)
        {
            return Ok(_DocumentService.Save(Document));

        }

        [HttpPut("{id}")]
        public ActionResult<DocumentEntity> Update([FromRoute] string id, DocumentEntity Document)
        {
            return Ok(_DocumentService.Update(id, Document));

        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete([FromRoute] string id)
        {
            return Ok(_DocumentService.Delete(id));

        }

        [HttpPost("UploadInvoices")]
        public ActionResult<bool> UploadInvoices([FromForm] List<IFormFile> files, [FromForm] string createdBy, [FromForm] string pageRange)
        {
            Console.Write(files[0].FileName);
            _DocumentService.UploadInvoiceFiles(files, createdBy, pageRange);
            return Ok(true);
        }
    }
}
