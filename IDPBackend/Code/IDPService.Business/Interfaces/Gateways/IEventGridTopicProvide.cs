using IDPService.Business.Gateways;

namespace IDPService.Business.Interfaces.Gateways
{
    public interface IEventGridTopicProvider
    {
        public EventGridTopic GetEventGridTopic(EventTypeEnum eventType);
    }
}