using BuildingBlocks.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Scraper.Services
{
    public interface IScraperService
    {
        Task<Card?> GetCard(int id);
        Task<int> GetMaxId();

    }
}