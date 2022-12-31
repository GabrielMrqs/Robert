using RobertMQ.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("a", (IMessageProducer _messageProducer) =>
{
    var message = new Message("ping");
    _messageProducer.SendMessage(message);
}).WithName("a");

app.Run();

class Message
{
    public string Text { get; set; }

    public Message(string text)
    {
        Text = text;
    }

    public override string ToString()
    {
        return Text;
    }
}