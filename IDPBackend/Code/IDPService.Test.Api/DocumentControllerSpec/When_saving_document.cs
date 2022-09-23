using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using IDPService.Entities.Entities;

namespace IDPService.Test.Api.DocumentControllerSpec
{
    public class When_saving_document : UsingDocumentControllerSpec
    {
        private ActionResult<DocumentEntity> _result;

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

            _documentService.Save(_document).Returns(_document);
        }
        public override void Because()
        {
            _result = subject.Save(_document);
        }

        [Test]
        public void Request_is_routed_through_service()
        {
            _documentService.Received(1).Save(_document);

        }

        [Test]
        public void Appropriate_result_is_returned()
        {
            _result.Result.ShouldBeOfType<OkObjectResult>();

            var resultListObject = (_result.Result as OkObjectResult).Value;

            resultListObject.ShouldBeOfType<DocumentEntity>();

            var resultList = (DocumentEntity)resultListObject;

            resultList.ShouldBe(_document);
        }
    }
}
