using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertMQ.Application
{
    public class SuperMessage : ICommand
    {
        public string? Message { get; set; }
    }
}
