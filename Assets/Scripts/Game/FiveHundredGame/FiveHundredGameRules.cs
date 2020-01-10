using Assets.Scripts.Game.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Game.FiveHundredGame
{
    public class FiveHundredGameRules
    {
        private static readonly HashSet<CardSuitsEnum> _fiveHundredSuits = new HashSet<CardSuitsEnum>
            {
                CardSuitsEnum.Club,
                CardSuitsEnum.Spade,
                CardSuitsEnum.Diamond,
                CardSuitsEnum.Heart,
                CardSuitsEnum.NoTrump
            };

        private static readonly HashSet<BidsEnum> _fiveHundredTricks = new HashSet<BidsEnum>
            {
                BidsEnum.Inkle,
                BidsEnum.Seven,
                BidsEnum.Eight,
                BidsEnum.Nine,
                BidsEnum.Ten
            };

        public static HashSet<CardSuitsEnum> FiveHundredSuits => _fiveHundredSuits;
        public static HashSet<BidsEnum> FiveHundredTricks => _fiveHundredTricks;

        // Bidding

        public void BidAceNoFace()
        {

        }
    }
}
