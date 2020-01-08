using Assets.Scripts.API.DTOs.Enums;
using Assets.Scripts.API.ServiceModels.Enums;
using Assets.Scripts.API.ServiceModels.Messages;
using Assets.Scripts.API.ServiceModels.Models;

namespace Assets.Scripts.API.DTOs.Mappers
{
    public static class CardMap
    {
        public static Card ToDto(this ReadCardRequest request)
            => new Card
            {
                CardValue = (CardSuit) request.CardSuit
            };

        public static CaptionCardModel ToServiceModel(this Card card)
            => new CaptionCardModel
            {
                CardSuit = (CardSuitModel) card.CardValue,
                CardText = card.CardText
            };
    }
}

