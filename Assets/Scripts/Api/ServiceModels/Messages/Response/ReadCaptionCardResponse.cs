using Assets.Scripts.API.ServiceModels.Models;

namespace Assets.Scripts.API.ServiceModels.Messages
{
    public class ReadCardResponse /*: IHasResponseStatus*/
    {
        //[APIMember(Description = "Contains any/all exceptions.")]
        public bool ResponseStatus { get; set; }

        //[APIMember(Description = "")]
        public CaptionCardModel CaptionCard { get; set; }
    }
}
