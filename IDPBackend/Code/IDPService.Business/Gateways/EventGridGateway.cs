using System;
using System.Collections.Generic;
using IDPService.Business.Interfaces.Gateways;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.EventGrid;

namespace IDPService.Business.Gateways
{
    public class EventGridGateway : IQueueGateway
    {
        private readonly IEventGridTopicProvider _eventGridTopicProvider;

        public EventGridGateway(IEventGridTopicProvider eventGridTopicProvider)
        {
            _eventGridTopicProvider = eventGridTopicProvider;
        }

        public void Send(Object data, EventTypeEnum eventType)
        {
            // Get EventGridTopic with Endpoint and Key based on provided event Type.
            EventGridTopic eventGridTopic = _eventGridTopicProvider.GetEventGridTopic(eventType);

            string topicHostname = new Uri(eventGridTopic.Endpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(eventGridTopic.Key);
            EventGridClient eventGridClient = new EventGridClient(topicCredentials);
            eventGridClient.PublishEventsAsync(topicHostname, GetEventsList(data, eventType.ToString())).GetAwaiter().GetResult();
            Console.Write("Published events to Event Grid.");
        }
        private IList<EventGridEvent> GetEventsList(Object data, string eventType)
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>
            {
                new EventGridEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = eventType,
                    Data = data,
                    EventTime = DateTime.Now,
                    Subject = eventType,
                    DataVersion = "2.0"
                }
            };

            return eventsList;
        }
    }
}