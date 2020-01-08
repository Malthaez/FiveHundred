using Assets.Scripts.API.ServiceDefinitions;
using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using Assets.Scripts.Game.Repositories.Mappers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Repositories
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
