using MatchingGame.Api.ServiceDefinitions;
using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Repositories.Mappers;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Repositories
{
    public class PlayingCardRepository : MonoBehaviour
    {
        public Card _cardPrefab; // Must be set in editor.
        public List<Sprite> _playingCardArtImages; // Must be set in editor.
        public CardService _cardService;
        
        public void Initialize(CardService cardService)
        {
            _cardService = cardService;
        }

        public Card CreatePlayingCardPrefab(CardValuesEnum cardValue)
        {
            var card = Instantiate(_cardPrefab);
            card.CardArt = _playingCardArtImages[(int) cardValue];

            card.CopyFromModel(_cardService.ReadCard(cardValue.ToRequest()).Card);

            return card;
        }
    }
}
