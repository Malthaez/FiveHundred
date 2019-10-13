using MatchingGame.Api.ServiceModels.Enums;

namespace MatchingGame.Api.ServiceModels.Messages
{
    //[ApiRoute(Route = "GET", Description = "Get Card from Card Value")]
    public class ReadCardRequest /*: IReturn<GetCardResponse> */
    {
        //[ApiMember(Description = "Card's suit.")]
        public CardSuitModel? CardSuit { get; set; }
    }
}
