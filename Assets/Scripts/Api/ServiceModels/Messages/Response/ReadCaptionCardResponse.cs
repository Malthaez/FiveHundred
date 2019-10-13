using MatchingGame.Api.ServiceModels.Models;

namespace MatchingGame.Api.ServiceModels.Messages
{
    public class ReadCardResponse /*: IHasResponseStatus*/
    {
        //[ApiMember(Description = "Contains any/all exceptions.")]
        public bool ResponseStatus { get; set; }

        //[ApiMember(Description = "")]
        public CaptionCardModel CaptionCard { get; set; }
    }
}
