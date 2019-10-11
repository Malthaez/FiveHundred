using MatchingGame.Api.DTOs;
using System.Collections.Generic;

namespace MatchingGame.Api.Repositories.Maps
{
    public static class CardMap
    {
        public static Dictionary<string, object> ToDto(this Card card)
            => new Dictionary<string, object>
                {
                    { "@CardValue" , card.CardValue},
                    { "@CardText" ,  card.CardText }
                };
    }
}
