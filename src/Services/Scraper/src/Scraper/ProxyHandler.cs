using System.Net;

namespace Scraper
{
    public class ProxyHandler

    {
        private readonly string _userName;
        private readonly string _userPassword;
        public readonly List<string>? _proxies;

        public ProxyHandler(IConfiguration config)
        {
            _userName = config.GetValue<string>("Proxy:Login");
            _userPassword=config.GetValue<string>("Proxy:Password");
            _proxies=config.GetSection("Proxy:Uri").Get<List<string>>();
                 
        }
        public List<HttpClientHandler> GetHandlersList(){
            List<HttpClientHandler> Proxies = new();
            for (int i = 0; i < _proxies?.Count; i++)
            {
                var handler = new HttpClientHandler
                {
                    Proxy = new WebProxy(_proxies?.ElementAtOrDefault(i))
                    {
                        Credentials = (new NetworkCredential
                        {
                            UserName = _userName,
                            Password = _userPassword
                        })
                    },
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                Proxies.Add(handler);
            }
           return Proxies;
        }

    }
}