namespace RobertMQ.Application
{
    public class Ping : ICommand
    {
        public int Round { get; set; }
    }
}