using NServiceBus;

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
        var endpointConfiguration = new EndpointConfiguration("Receiver");

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

        transport.UseConventionalRoutingTopology(QueueType.Classic);

        var connectionString = context.Configuration.GetConnectionString("RabbitMqConnectionString");

        transport.ConnectionString(connectionString);

        //transport.Routing().RouteToEndpoint(typeof(Ping), "Receiver");

        endpointConfiguration.EnableInstallers();

        endpointConfiguration.AuditProcessedMessagesTo("audit");

        return endpointConfiguration;
    })
    .ConfigureServices(services =>
    {
        //services.AddHostedService<ReceiverWorker>();
    })
    .Build();

await host.RunAsync();
