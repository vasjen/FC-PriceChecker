using Scraper.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("fb", c => 
{
    c.BaseAddress = new Uri("https://www.futbin.com/");
    c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4464.5 Safari/537.36 Edg/91.0.866.0");
});
builder.Services.AddTransient<IScraperService, ScraperService>();
var app = builder.Build();
var service = app.Services.GetRequiredService<IScraperService>();
app.MapGet("/",  async (int id) => await service.GetCard(id));
app.MapGet("/max",  async () => await service.GetMaxId());

app.Run();
