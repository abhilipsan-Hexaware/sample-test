using NSubstitute;
using IDPService.Test.Framework;
using IDPService.Api.Controllers;
using IDPService.Business.Interfaces;


namespace IDPService.Test.Api.DocumentControllerSpec
{
    public abstract class UsingDocumentControllerSpec : SpecFor<DocumentController>
    {
        protected IDocumentService _documentService;

        public override void Context()
        {
            _documentService = Substitute.For<IDocumentService>();
            subject = new DocumentController(_documentService);

        }

    }
}
