using System;
using IDPService.Business.Gateways;

namespace IDPService.Business.Interfaces.Gateways
{
    public interface IQueueGateway
    {
         void Send(Object message, EventTypeEnum eventType);
    }
}