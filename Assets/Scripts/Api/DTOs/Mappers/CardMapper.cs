using MatchingGame.Api.DTOs.Enums;
using MatchingGame.Api.ServiceModels.Enums;
using MatchingGame.Api.ServiceModels.Messages;
using MatchingGame.Api.ServiceModels.Models;

namespace MatchingGame.Api.DTOs.Mappers
{
    public static class CardMap
    {
        public static Card ToDto(this ReadCardRequest request)
            => new Card
            {
                CardValue = (CardValue) request.CardValue
            };

        public static MemoryCardModel ToServiceModel(this Card card)
            => new MemoryCardModel
            {
                CardValue = (CardValueModel) card.CardValue,
                CardText = card.CardText
            };
    }
}
