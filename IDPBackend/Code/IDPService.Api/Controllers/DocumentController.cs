using System.Collections.Generic;
using IDPService.Business.Interfaces;
using IDPService.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Document>> Get()
        {
            return Ok(_DocumentService.GetAll());
        }

        [HttpPost]
        public ActionResult<Document> Save(Document Document)
        {
            return Ok(_DocumentService.Save(Document));

        }

        [HttpPut("{id}")]
        public ActionResult<Document> Update([FromRoute] string id, Document Document)
        {
            return Ok(_DocumentService.Update(id, Document));

        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete([FromRoute] string id)
        {
            return Ok(_DocumentService.Delete(id));

        }


    }
}
