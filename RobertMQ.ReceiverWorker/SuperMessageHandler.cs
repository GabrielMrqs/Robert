using NServiceBus;
using RobertMQ.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
