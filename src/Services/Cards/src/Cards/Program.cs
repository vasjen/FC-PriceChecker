using Cards;
using Cards.Data;
using Cards.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddTransient<Receive>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// var rcv = app.Services.GetService<Receive>();
// CancellationTokenSource cts = new CancellationTokenSource();
// await rcv.ConsumeCardMessage(cts.Token);
// Console.WriteLine("Press [enter] to exit.");
// Console.ReadLine();
// cts.Cancel();
app.UseAuthorization();

app.MapControllers();
app.Run();
