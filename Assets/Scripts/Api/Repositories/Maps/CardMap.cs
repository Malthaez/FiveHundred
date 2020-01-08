using Assets.Scripts.API.DTOs;
using System.Collections.Generic;

namespace Assets.Scripts.API.Repositories.Maps
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
