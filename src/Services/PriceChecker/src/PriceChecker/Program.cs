using PriceChecker;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Processor>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.MapGet("/", (string text) => $"Hello World: {text}!");
var Processor = app.Services.GetService<Processor>();
app.MapGet("/p", async (int id) =>
{
    var card = await Processor.CallScraperToGetCard(id);
    System.Console.WriteLine(card);
    await Processor.SendMessageToCardController(card);
});
app.Run();
