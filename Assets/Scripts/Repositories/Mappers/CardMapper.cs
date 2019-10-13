using MatchingGame.Api.ServiceModels.Enums;
using MatchingGame.Api.ServiceModels.Messages;
using MatchingGame.Api.ServiceModels.Models;
using MatchingGame.Behaviors;
using MatchingGame.Enums;

namespace MatchingGame.Repositories.Mappers
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
            card.CardValue = (CardSuitsEnum) cardModel.CardSuit;

            card.name = $"Card ({cardModel.CardSuit.ToString()})";
        }
    }
}
