using MatchingGame.Behaviors;
using MatchingGame.Enums;
using System.Collections.Generic;

namespace MatchingGame
{
    public static class Rules
    {
        //Returns false if any card's value in a collection are "NotSet"
        public static bool CardValuesAreSet(IEnumerable<Card> cards)
        {
            foreach (Card card in cards) { if (card.CardValue == CardValuesEnum.NotSet) { return false; } }

            return true;
        }

        //Returns false if any card's value in a collection isn't the same
        public static bool CardValuesMatch(IEnumerable<Card> cards)
        {
            CardValuesEnum x = CardValuesEnum.NotSet;

            foreach (Card card in cards) { x = card.CardValue; }
            foreach (Card card in cards) { if (card.CardValue != x) { return false; } }

            return true;
        }

        //Returns false if the cards collection size is smaller than the match-checking size
        public static bool ValidateCardCount(int compareSize, IEnumerable<Card> cards)
        {
            int count = 0;

            foreach (Card card in cards) { count++; }

            return (count >= compareSize);
        }
    }
}
