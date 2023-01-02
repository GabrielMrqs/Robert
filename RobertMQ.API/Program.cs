using NServiceBus;
using RobertMQ.API.Extensions;
using RobertMQ.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Host.ConfigureNServiceBus();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("ping", async (IMessageSession _messageSession) =>
{
    await _messageSession
          .Send(new Ping())
          .ConfigureAwait(false);

    return "deu boa";

}).WithName("ping");

app.MapGet("supermessage", async (IMessageSession _messageSession) =>
{
    await _messageSession
          .Send(new SuperMessage() { Message = "oiiii" })
          .ConfigureAwait(false);

    return "deu boa";

}).WithName("supermessage");

app.Run();