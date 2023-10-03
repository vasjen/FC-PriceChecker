namespace Scraper;

public interface IRequestWithProxy
{
   Task<HttpResponseMessage> MakeRequestUsingRandomProxy(string url);

}