using MatchingGame.Api.ServiceDefinitions;
using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Repositories.Mappers;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Repositories
{
    public class MemoryCardRepository : MonoBehaviour
    {
        public MemoryCard _cardPrefab; // Must be set in editor.
        public List<Sprite> _cardArtImages; // Must be set in editor.
        public CardService _cardService;
        
        public void Initialize(CardService cardService)
        {
            _cardService = cardService;
        }

        public MemoryCard CreateCardPrefab(CardValuesEnum cardValue)
        {
            var card = Instantiate(_cardPrefab);
            card.CardArt = _cardArtImages[(int) cardValue];

            card.CopyFromModel(_cardService.ReadCard(cardValue.ToRequest()).Card);

            return card;
        }
    }
}
