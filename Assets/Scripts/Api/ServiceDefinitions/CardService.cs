using Assets.Scripts.API.DTOs.Mappers;
using Assets.Scripts.API.Interfaces.Managers;
using Assets.Scripts.API.ServiceModels.Messages;

namespace Assets.Scripts.API.ServiceDefinitions
{
    public class CardService /*: Service*/
    {
        public ICardManager _cardManager;

        public CardService(ICardManager cardManager) => _cardManager = cardManager;

        //public PostSampleResponse Post(PostSampleRequest request) => new PostSampleResponse { Id = _sampleManager.Post(request.ToDto()) };

        public ReadCardResponse ReadCaptionCard(ReadCardRequest request) => new ReadCardResponse { CaptionCard = _cardManager.Get(request.ToDto()).ToServiceModel() };

        //public PutSampleResponse Put(PutSampleRequest request) => new PutSampleResponse { ResponseStatus = _sampleManager.Put(request.Id, request.ToDto()) };

        //public DeleteSampleResponse Delete(DeleteSampleRequest request) => new DeleteSampleResponse { ResponseStatus = _sampleManager.Delete(request.Id) };
    }
}
