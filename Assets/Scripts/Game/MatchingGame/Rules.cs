using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public static class Rules
    {
        public static bool CheckForJack(Card card) => card.Value == (int)CardValuesEnum.Jack;

        //Returns false if any card's suit in a collection are "NotSet"
        public static bool CardSuitsAreSet(IEnumerable<Card> cards)
        {
            foreach (Card card in cards) { if (card.Suit == CardSuitsEnum.NotSet) { return false; } }

            return true;
        }

        //Returns false if any card's value in a collection isn't the same
        public static bool CardValuesMatch(IEnumerable<Card> cards)
        {
            int x = ((List<Card>)cards)[0].Value;

            foreach (Card card in cards) { if (card.Value != x) { return false; } }

            return true;
        }

        //Returns false if any card's suit in a collection isn't the same
        public static bool CardSuitsMatch(IEnumerable<Card> cards)
        {
            CardSuitsEnum x = ((List<Card>)cards)[0].Suit;

            foreach (Card card in cards) { if (card.Suit != x) { return false; } }

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
