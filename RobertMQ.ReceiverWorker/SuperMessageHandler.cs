using NServiceBus;
using RobertMQ.Application;

namespace RobertMQ.ReceiverWorker
{
    public class SuperMessageHandler : IHandleMessages<SuperMessage>
    {
        public Task Handle(SuperMessage message, IMessageHandlerContext context)
        {
            var a = message;

            return Task.CompletedTask;
        }
    }
}
