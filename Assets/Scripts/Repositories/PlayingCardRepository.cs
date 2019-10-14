using MatchingGame.Api.ServiceDefinitions;
using MatchingGame.Behaviors;
using MatchingGame.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Repositories
{
    public class PlayingCardRepository : MonoBehaviour
    {
        public Card _cardPrefab; // Must be set in editor.
        public List<Sprite> _playingCardArtImages; // Must be set in editor.
        public CardService _cardService;

        public void Initialize(CardService cardService) => _cardService = cardService;

        public Card CreatePlayingCardPrefab(CardSuitsEnum cardSuit, CardValuesEnum cardValue)
        {
            var card = Instantiate(_cardPrefab);

            card.Suit = cardSuit;
            card.Value = (int)cardValue;
            card.Art = _playingCardArtImages[(int)cardSuit * (int)cardValue];

            return card;
        }
    }
}
