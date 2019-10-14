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
        public CaptionCard _cardPrefab; // Must be set in editor.
        public List<Sprite> _cardArtImages; // Must be set in editor.
        public CardService _cardService;
        
        public void Initialize(CardService cardService)
        {
            _cardService = cardService;
        }

        public CaptionCard CreateMemoryCardPrefab(CardSuitsEnum cardValue)
        {
            var card = Instantiate(_cardPrefab);
            card.Art = _cardArtImages[(int) cardValue];

            card.CopyFromModel(_cardService.ReadCaptionCard(cardValue.ToRequest()).CaptionCard);

            return card;
        }
    }
}
