using MatchingGame.Api.DTOs.Mappers;
using MatchingGame.Api.Interfaces.Managers;
using MatchingGame.Api.ServiceModels.Messages;

namespace MatchingGame.Api.ServiceDefinitions
{
    public class CardService /*: Service*/
    {
        public ICardManager _cardManager;

        public CardService(ICardManager cardManager)
        {
            _cardManager = cardManager;
        }

        //public PostSampleResponse Post(PostSampleRequest request) => new PostSampleResponse { Id = _sampleManager.Post(request.ToDto()) };

        public ReadCardResponse ReadCard(ReadCardRequest request) => new ReadCardResponse { Card = _cardManager.Get(request.ToDto()).ToServiceModel() };

        //public PutSampleResponse Put(PutSampleRequest request) => new PutSampleResponse { ResponseStatus = _sampleManager.Put(request.Id, request.ToDto()) };

        //public DeleteSampleResponse Delete(DeleteSampleRequest request) => new DeleteSampleResponse { ResponseStatus = _sampleManager.Delete(request.Id) };
    }
}
