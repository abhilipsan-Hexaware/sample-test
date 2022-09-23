using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Shouldly;
using IDPService.Entities.Entities;

namespace IDPService.Test.Business.DocumentServiceSpec
{
    public class When_getting_all_document : UsingDocumentServiceSpec
    {
        private IEnumerable<DocumentEntity> _result;

        private IEnumerable<DocumentEntity> _all_document;
        private DocumentEntity _document;

        public override void Context()
        {
            base.Context();

            _document = new DocumentEntity{
                DocId = "DocId",
                ContentType = "ContentType",
                FileName = "FileName",
                CreatedBy = "CreatedBy",
                UpdatedBy = "UpdatedBy",
                FilePath = "FilePath",
                CategorySetId = "CategorySetId"
            };

            _all_document = new List<DocumentEntity> { _document};
            _documentRepository.GetAll().Returns(_all_document);
        }
        public override void Because()
        {
            _result = subject.GetAll();
        }

        [Test]
        public void Request_is_routed_through_repository()
        {
            _documentRepository.Received(1).GetAll();

        }

        [Test]
        public void Appropriate_result_is_returned()
        {
            _result.ShouldBeOfType<List<DocumentEntity>>();

            List<DocumentEntity> resultList = _result as List<DocumentEntity>;

            resultList.Count.ShouldBe(1);

            resultList.ShouldBe(_all_document);
        }
    }
}
