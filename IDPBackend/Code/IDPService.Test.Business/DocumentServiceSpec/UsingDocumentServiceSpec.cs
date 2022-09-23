using NSubstitute;
using IDPService.Test.Framework;
using IDPService.Data.Interfaces;
using IDPService.Business.Services;
using IDPService.Business.Interfaces.Gateways;
using Microsoft.Extensions.Configuration;

namespace IDPService.Test.Business.DocumentServiceSpec
{
    public abstract class UsingDocumentServiceSpec : SpecFor<DocumentService>
    {
        protected IDocumentRepository _documentRepository;
        protected IBlobGateway _blobgateway;
        protected IQueueGateway Queuegateway;
        protected ICategorySetRepository CategorySetRepository;
       protected ICategoryRepository CategoryRepository;
       protected IConfiguration Configuration;
        public override void Context()
        {
            _documentRepository = Substitute.For<IDocumentRepository>();
            _blobgateway = Substitute.For<IBlobGateway>();
            Queuegateway = Substitute.For<IQueueGateway>();
            CategorySetRepository = Substitute.For<ICategorySetRepository>();
            CategoryRepository = Substitute.For<ICategoryRepository>();
            Configuration = Substitute.For<IConfiguration>();
            subject = new DocumentService(_documentRepository,_blobgateway,Queuegateway,CategorySetRepository,CategoryRepository, Configuration);

        }

    }
}
