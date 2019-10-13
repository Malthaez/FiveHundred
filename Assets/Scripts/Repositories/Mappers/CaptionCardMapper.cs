using MatchingGame.Api.ServiceModels.Models;
using MatchingGame.Behaviors;
using MatchingGame.Enums;

namespace MatchingGame.Repositories.Mappers
{
    public static class CaptionCardMapper
    {
        public static void CopyFromModel(this CaptionCard card, CaptionCardModel cardModel)
        {
            card.CardValue = (CardSuitsEnum) cardModel.CardSuit;
            card.CardText  = cardModel.CardText;

            card.name = $"Card ({cardModel.CardSuit.ToString()})";
        }
    }
}
