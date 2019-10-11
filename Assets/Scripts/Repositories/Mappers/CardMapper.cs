using MatchingGame.Api.ServiceModels.Enums;
using MatchingGame.Api.ServiceModels.Messages;
using MatchingGame.Api.ServiceModels.Models;
using MatchingGame.Behaviors;
using MatchingGame.Enums;

namespace MatchingGame.Repositories.Mappers
{
    public static class CardMapper
    {
        public static ReadCardRequest ToRequest(this CardValuesEnum cardValue)
            => new ReadCardRequest
            {
                CardValue = (CardValueModel)cardValue
            };

        public static void CopyFromModel(this Card card, CardModel cardModel)
        {
            card.CardValue = (CardValuesEnum) cardModel.CardValue;

            card.name = $"Card ({cardModel.CardValue.ToString()})";
        }
    }
}
