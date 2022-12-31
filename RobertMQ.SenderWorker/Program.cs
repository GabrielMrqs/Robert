using RobertMQ.Application;
using RobertMQ.SenderWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logging) =>
    {
        logging.AddConfiguration(context.Configuration.GetSection("Logging"));

        logging.AddConsole();
    })
    .UseConsoleLifetime()
    .UseNServiceBus(context =>
    {
        // Configure the NServiceBus endpoint
        var endpointConfiguration = new EndpointConfiguration("Sender");

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

        transport.UseConventionalRoutingTopology(QueueType.Classic);
        
        var connectionString = context.Configuration.GetConnectionString("RabbitMqConnectionString");
        
        transport.ConnectionString(connectionString);

        transport.Routing().RouteToEndpoint(typeof(Ping), "Receiver");

        endpointConfiguration.EnableInstallers();

        endpointConfiguration.AuditProcessedMessagesTo("audit");

        return endpointConfiguration;
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<SenderWorker>();
    })
    .Build();

await host.RunAsync();
