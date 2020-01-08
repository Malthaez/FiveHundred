using Assets.Scripts.API.ServiceModels.Enums;
using Assets.Scripts.API.ServiceModels.Messages;
using Assets.Scripts.API.ServiceModels.Models;
using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;

namespace Assets.Scripts.Game.Repositories.Mappers
{
    public static class CardMapper
    {
        public static ReadCardRequest ToRequest(this CardSuitsEnum cardValue)
            => new ReadCardRequest
            {
                CardSuit = (CardSuitModel)cardValue
            };

        public static void CopyFromModel(this Card card, CardModel cardModel)
        {
            card.Suit = (CardSuitsEnum) cardModel.CardSuit;

            card.name = $"Card ({cardModel.CardSuit.ToString()})";
        }
    }
}
