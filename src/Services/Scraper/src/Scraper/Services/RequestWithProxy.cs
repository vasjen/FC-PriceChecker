using System.Net;

namespace Scraper;
public class RequestWithProxy : IRequestWithProxy
{
    private readonly List<WebProxy> _proxies;

    public RequestWithProxy(IWebshareService webshareService)
    {
        _proxies = webshareService.GetProxies().Result;
    }
    

    public async Task<HttpResponseMessage> MakeRequestUsingRandomProxy(string url)
    {
      Random random = new Random();
      int index = random.Next(_proxies.Count());
     
      HttpClientHandler handler = new HttpClientHandler
      {
            Proxy = _proxies[index],
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };
        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://www.futbin.com/")
        };
        client.DefaultRequestHeaders
        .Add("User-Agent","User Agent	Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko)");
        var result = await client.GetAsync(url);
        
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                System.Console.WriteLine("Not found for url: {0}. Ip: {1}", url,handler.Proxy.ToString());
                return null;
            }
            if (result.StatusCode == HttpStatusCode.TooManyRequests)
            {
                System.Console.WriteLine("Too many requests for url: {0}. Ip: {1}", url, handler.Proxy.ToString());
                return null;
            }
            if (result.StatusCode == HttpStatusCode.Forbidden)
            {
                System.Console.WriteLine("Forbidden for url: {0}. Ip: {1}", url, await client.GetStringAsync("https://api.ipify.org"));
                return null;
            }
        // System.Console.WriteLine(result);    
        return result;
      
    }
    }
