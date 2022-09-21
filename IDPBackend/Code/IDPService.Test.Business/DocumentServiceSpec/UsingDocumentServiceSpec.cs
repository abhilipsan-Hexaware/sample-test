using NSubstitute;
using IDPService.Test.Framework;
using IDPService.Business.Services;
using IDPService.Data.Interfaces;

namespace IDPService.Test.Business.DocumentServiceSpec
{
    public abstract class UsingDocumentServiceSpec : SpecFor<DocumentService>
    {
        protected IDocumentRepository _documentRepository;

        public override void Context()
        {
            _documentRepository = Substitute.For<IDocumentRepository>();
            subject = new DocumentService(_documentRepository);

        }

    }
}
