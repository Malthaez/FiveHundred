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

        public CardService _cardService;

        public void Initialize(CardService cardService)
        {
            _cardService = cardService;

            _playingCardArtImages = new Dictionary<CardSuitsEnum, List<Sprite>>
            {
                { CardSuitsEnum.Club, _clubsArtImages },
                { CardSuitsEnum.Spade, _spadesArtImages },
                { CardSuitsEnum.Heart, _heartsArtImages },
                { CardSuitsEnum.Diamond, _diamondsArtImages }
            };
        }

        public Card CreatePlayingCardPrefab(CardSuitsEnum cardSuit, CardValuesEnum cardValue)
        {
            var card = Instantiate(_cardPrefab);

            card.Suit = cardSuit;
            card.Value = (int)cardValue;
            card.Art = _playingCardArtImages[cardSuit][(int)cardValue - 1];

            return card;
        }
    }
}
