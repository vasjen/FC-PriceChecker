using Cards.Data;
using Cards.Models;

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
                Description = cardCreateRequest.Description
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
                card.Description = cardUpdateRequest.Description;
                _context.Cards.Update(card);
                await _context.SaveChangesAsync();
            }
        }
    }
}
