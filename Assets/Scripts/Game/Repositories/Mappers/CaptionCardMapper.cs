using Assets.Scripts.API.ServiceModels.Models;
using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;

namespace Assets.Scripts.Game.Repositories.Mappers
{
    public static class CaptionCardMapper
    {
        public static void CopyFromModel(this CaptionCard card, CaptionCardModel cardModel)
        {
            card.Suit = (CardSuitsEnum) cardModel.CardSuit;
            card.CardText  = cardModel.CardText;

            card.name = $"Card ({cardModel.CardSuit.ToString()})";
        }
    }
}
