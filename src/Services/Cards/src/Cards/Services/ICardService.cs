using Cards.Models;

namespace Cards.Services
{
    public interface ICardService
    {
            Task<Card> CreateCard(CardCreateRequest card);
            Task<Card?> GetCard(int id);
            Task UpdateCard(CardUpdateRequest card);
            Task DeleteCard(int id);
    }
}
