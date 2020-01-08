using Assets.Scripts.API.ServiceModels.Enums;

namespace Assets.Scripts.API.ServiceModels.Models
{
    public class CardModel
    {
        //[APIMember(Description = "Card's suit.")]
        public CardSuitModel? CardSuit { get; set; }
    }
}
