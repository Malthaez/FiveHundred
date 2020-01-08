using Assets.Scripts.API.ServiceModels.Enums;

namespace Assets.Scripts.API.ServiceModels.Messages
{
    //[APIRoute(Route = "GET", Description = "Get Card from Card Value")]
    public class ReadCardRequest /*: IReturn<GetCardResponse> */
    {
        //[APIMember(Description = "Card's suit.")]
        public CardSuitModel? CardSuit { get; set; }
    }
}
