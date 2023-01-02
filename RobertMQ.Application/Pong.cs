namespace RobertMQ.Application
{
    public class Pong : IMessage
    {
        public string? Acknowledgement { get; set; }
    }
}
