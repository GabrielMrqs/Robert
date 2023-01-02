using NServiceBus;
using RobertMQ.Application;

namespace RobertMQ.API.Extensions
{
    public static class HostExtensions
    {
        public static void ConfigureNServiceBus(this IHostBuilder host)
        {
            host.UseNServiceBus(context =>
            {
                var endpointConfiguration = new EndpointConfiguration("Sender");

                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

                transport.UseConventionalRoutingTopology(QueueType.Classic);

                var connectionString = context.Configuration.GetConnectionString("RabbitMqConnectionString");

                transport.ConnectionString(connectionString);

                transport.Routing().RouteToEndpoint(typeof(Ping).Assembly, "Receiver");

                endpointConfiguration.EnableInstallers();

                endpointConfiguration.AuditProcessedMessagesTo("audit");

                return endpointConfiguration;
            });
        }
    }
}
