using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertMQ.Application
{
    public class Pong : IMessage
    {
        public string? Acknowledgement { get; set; }
    }
}
