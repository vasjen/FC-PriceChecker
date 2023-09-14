using Microsoft.AspNetCore.Mvc;

namespace Scraper.Services
{
    public interface IScraperService
    {
        Task<string?> GetCard(string id);
    }
}