using Cards.Data;
using BuildingBlocks.Core.Models;

namespace Cards.Services
{
    public class CardService : ICardService
    {
        private readonly AppDbContext _context;

        public CardService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Card> CreateCard(CardCreateRequest cardCreateRequest)
        {
            var card = new Card
            {
                Name = cardCreateRequest.Name,
                Revision = cardCreateRequest.Description
            };
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return card;
            
        }

        public async Task DeleteCard(int id)
        {
            var card = await _context.FindAsync<Card>(id);
            if (card is not null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Card?> GetCard(int id)
            => await _context.FindAsync<Card>(id);

        public async Task UpdateCard(CardUpdateRequest cardUpdateRequest)
        {
            var card = await _context.FindAsync<Card>(cardUpdateRequest.Id);
            if (card is not null)
            {
                card.Name = cardUpdateRequest.Name;
                card.Revision = cardUpdateRequest.Description;
                _context.Cards.Update(card);
                await _context.SaveChangesAsync();
            }
        }
    }
}
