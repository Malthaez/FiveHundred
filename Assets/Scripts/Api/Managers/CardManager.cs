using MatchingGame.Api.DTOs;
using MatchingGame.Api.Interfaces.Managers;
using MatchingGame.Api.Interfaces.Repositories;

namespace MatchingGame.Api.Managers
{
    public class CardManager : ICardManager
    {
        private readonly ICardRepository _cardRepository;

        public CardManager(ICardRepository cardRepository) => _cardRepository = cardRepository;

        public Card Get(Card card) => _cardRepository.Get(card.CardValue);
    }
}
