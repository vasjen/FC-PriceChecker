using System.Net;

namespace Scraper
{
    public class WebShareService : IWebshareService
    {
        private readonly string link;
        public WebShareService(IConfiguration configuration)
        {
           link = configuration.GetValue<string>("webshareApi") + "/-/any/username/direct/-/";
        }
        public async Task<List<WebProxy>> GetProxies()
        {
            var list = await GetProxiesFromWebShare(link);
            return ConnvertToWebProxy(list);
        }
        private async Task<string> GetProxiesFromWebShare(string url)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://proxy.webshare.io/api/v2/proxy/list/download/")
            };
            var response = await client.GetAsync(url);
            return  await response.Content.ReadAsStringAsync();
        }
        private List<WebProxy> ConnvertToWebProxy(string proxy)
        {
            var list = proxy.Split(Environment.NewLine);
            var webProxies = new List<WebProxy>();
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                var split = item.Split(":");
                var ip = split[0];
                var port = int.Parse(split[1]);
                var username = split[2];
                var password = split[3];
                var proxyAddress = new WebProxy(ip, port);
                proxyAddress.Credentials = new NetworkCredential(username, password);
                webProxies.Add(proxyAddress);
            }
            return webProxies;
        }
    }

    public interface IWebshareService
    {
        Task<List<WebProxy>> GetProxies();
    }
}