using Assets.Scripts.API.DTOs;
using Assets.Scripts.API.Interfaces.Managers;
using Assets.Scripts.API.Interfaces.Repositories;

namespace Assets.Scripts.API.Managers
{
    public class CardManager : ICardManager
    {
        private readonly ICardRepository _cardRepository;

        public CardManager(ICardRepository cardRepository) => _cardRepository = cardRepository;

        public Card Get(Card card) => _cardRepository.Get(card.CardValue);
    }
}
