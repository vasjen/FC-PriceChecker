using Scraper;
using Scraper.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("fb", c => 
{
    c.BaseAddress = new Uri("https://www.futbin.com/");
    c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4464.5 Safari/537.36 Edg/91.0.866.0");
});
builder.Services.AddTransient<ProxyHandler>();
builder.Services.AddTransient<IScraperService, ScraperService>();
builder.Services.AddTransient<IInitialService,InitialService>();
builder.Services.AddTransient<IRequestWithProxy,RequestWithProxy>();
builder.Services.AddTransient<IWebshareService, WebShareService>();
var app = builder.Build();
var service = app.Services.GetRequiredService<IScraperService>();
var init = app.Services.GetRequiredService<IInitialService>();
var share = app.Services.GetRequiredService<IWebshareService>();
app.MapGet("/{id}",  async (int id) => await service.GetCard(id));
app.MapGet("/max",  async () => await service.GetMaxId());
// Receive.Wait();
await init.Init();
var proxies = await share.GetProxies();
// System.Console.WriteLine("Total proxies: {0}", proxies.Count);
// foreach (var item in proxies)
// {
    // System.Console.WriteLine("{0} : {1}", item.Address.Host, item.Address.Port);
// }
// System.Console.WriteLine(proxies);
app.Run();
