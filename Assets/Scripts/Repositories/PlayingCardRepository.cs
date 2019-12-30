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
        private Dictionary<CardSuitsEnum, List<Sprite>> _playingCardArtImages;

        [SerializeField] private List<Sprite> _clubsArtImages; //Must be set in editor.
        [SerializeField] private List<Sprite> _spadesArtImages; //Must be set in editor.
        [SerializeField] private List<Sprite> _heartsArtImages; //Must be set in editor.
        [SerializeField] private List<Sprite> _diamondsArtImages; //Must be set in editor.
        [SerializeField] private List<Sprite> _jokerArtImages; //Must be set in editor.

        public CardService _cardService;

        private Dictionary<CardValuesEnum, int> artIndexFromValue = new Dictionary<CardValuesEnum, int>
        {
            { CardValuesEnum.Ace, 0 },
            { CardValuesEnum.Two, 1 },
            { CardValuesEnum.Three, 2 },
            { CardValuesEnum.Four, 3 },
            { CardValuesEnum.Five, 4 },
            { CardValuesEnum.Six, 5 },
            { CardValuesEnum.Seven, 6 },
            { CardValuesEnum.Eight, 7 },
            { CardValuesEnum.Nine, 8 },
            { CardValuesEnum.Ten, 9 },
            { CardValuesEnum.Jack, 10 },
            { CardValuesEnum.Queen, 11 },
            { CardValuesEnum.King, 12 },
            { CardValuesEnum.Joker, 0 }
        };

        public void Initialize(CardService cardService)
        {
            _cardService = cardService;

            _playingCardArtImages = new Dictionary<CardSuitsEnum, List<Sprite>>
            {
                { CardSuitsEnum.Club, _clubsArtImages },
                { CardSuitsEnum.Spade, _spadesArtImages },
                { CardSuitsEnum.Heart, _heartsArtImages },
                { CardSuitsEnum.Diamond, _diamondsArtImages },
                { CardSuitsEnum.Joker,  _jokerArtImages }
            };
        }

        public Card CreatePlayingCardPrefab(CardSuitsEnum cardSuit, CardValuesEnum cardValue)
        {
            var card = Instantiate(_cardPrefab);

            card.Suit = cardSuit;
            card.Value = (int)cardValue;
            card.Art = _playingCardArtImages[cardSuit][artIndexFromValue[cardValue]];

            return card;
        }
    }
}
