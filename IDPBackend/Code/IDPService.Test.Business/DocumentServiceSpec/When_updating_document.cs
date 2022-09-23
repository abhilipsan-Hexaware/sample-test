using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Shouldly;
using IDPService.Entities.Entities;


namespace IDPService.Test.Business.DocumentServiceSpec
{
    public class When_updating_document : UsingDocumentServiceSpec
    {
        private DocumentEntity _result;
        private DocumentEntity _document;

        public override void Context()
        {
            base.Context();

            _document = new DocumentEntity
            {
                DocId = "DocId",
                ContentType = "ContentType",
                FileName = "FileName",
                CreatedBy = "CreatedBy",
                UpdatedBy = "UpdatedBy",
                FilePath = "FilePath",
                CategorySetId = "CategorySetId"
            };

            _documentRepository.Update(_document.Id, _document).Returns(_document);
            
        }
        public override void Because()
        {
            _result = subject.Update(_document.Id, _document);
        }

        [Test]
        public void Request_is_routed_through_repository()
        {
            _documentRepository.Received(1).Update(_document.Id, _document);

        }

        [Test]
        public void Appropriate_result_is_returned()
        {
            _result.ShouldBeOfType<DocumentEntity>();

            _result.ShouldBe(_document);
        }
    }
}
