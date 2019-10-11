using MatchingGame.Api.ServiceModels.Enums;

namespace MatchingGame.Api.ServiceModels.Models
{
    public class CardModel
    {
        //[ApiMember(Description = "Card's value.")]
        public CardValueModel? CardValue { get; set; }
    }
}
