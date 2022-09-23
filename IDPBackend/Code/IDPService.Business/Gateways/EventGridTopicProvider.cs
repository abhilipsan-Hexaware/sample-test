using System.Collections.Generic;
using IDPService.Business.Interfaces.Gateways;
using Microsoft.Extensions.Configuration;

namespace IDPService.Business.Gateways
{
    public class EventGridTopicProvider : IEventGridTopicProvider
    {
        private Dictionary<EventTypeEnum, EventGridTopic> _eventGridTopicDictionary;
        private readonly IConfiguration _configuration;

        public EventGridTopicProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            PopulateEventGridTopicDictionary();
        }

        public EventGridTopic GetEventGridTopic(EventTypeEnum eventType)
        {
            return _eventGridTopicDictionary[eventType];
        }

        private void PopulateEventGridTopicDictionary()
        {
            _eventGridTopicDictionary = new Dictionary<EventTypeEnum, EventGridTopic>()
            {
                [EventTypeEnum.formRecognizerAnalyzeInvoiceForm] = new EventGridTopic { Endpoint = _configuration["AzureEventGrid:AnalyzeInvoiceFormEndPoint"], Key = _configuration["AzureEventGrid:AnalyzeInvoiceFormAccessKey"] },
            };
        }
    }
}