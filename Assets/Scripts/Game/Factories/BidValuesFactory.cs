using Assets.Scripts.Game.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Factories
{
    public static class BidValuesFactory
    {
        private static Dictionary<CardSuitsEnum, int> _baseScores = new Dictionary<CardSuitsEnum, int>()
        {
            { CardSuitsEnum.Spade, 40 },
            { CardSuitsEnum.Club, 60 },
            { CardSuitsEnum.Diamond, 80 },
            { CardSuitsEnum.Heart, 100 },
            { CardSuitsEnum.NoTrump, 120 },
        };

        public static int GetBid(CardSuitsEnum suit, int tricks) => suit != CardSuitsEnum.NotSet ? _baseScores[suit] : 0;
    }
}
