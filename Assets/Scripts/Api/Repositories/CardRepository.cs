using MatchingGame.Api.Clients.Interfaces;
using MatchingGame.Api.DTOs;
using MatchingGame.Api.DTOs.Enums;
using MatchingGame.Api.Interfaces.Repositories;
using System.Collections.Generic;

namespace MatchingGame.Api.Repositories
{
    public class CardRepository : ICardRepository 
    {
        private readonly ISampleServiceClient _sampleServiceClient;

        public CardRepository(ISampleServiceClient sampleServiceClient)
        {
            _sampleServiceClient = sampleServiceClient;
        }

        public Card Get(CardValue? cardValue) => ReadCard(_sampleServiceClient.Get((int) cardValue));

        public Card ReadCard(Dictionary<string, object> reader)
            => new Card
            {
                CardValue = (CardValue?) reader["@CardValue"],
                CardText  = (string)     reader["@CardText"]
            };
    }
}
