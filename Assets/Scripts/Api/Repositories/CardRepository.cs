using Assets.Scripts.API.Clients.Interfaces;
using Assets.Scripts.API.DTOs;
using Assets.Scripts.API.DTOs.Enums;
using Assets.Scripts.API.Interfaces.Repositories;
using System.Collections.Generic;

namespace Assets.Scripts.API.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ISampleServiceClient _sampleServiceClient;

        public CardRepository(ISampleServiceClient sampleServiceClient) => _sampleServiceClient = sampleServiceClient;

        public Card Get(CardSuit? cardSuit) => ReadCaptionCard(_sampleServiceClient.Get((int)cardSuit));

        public Card ReadCaptionCard(Dictionary<string, object> reader)
            => new Card
            {
                CardValue = (CardSuit?)reader["@CardValue"],
                CardText = (string)reader["@CardText"]
            };
    }
}
