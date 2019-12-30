using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Factories;
using UnityEngine;

namespace MatchingGame.Utilities
{
    public static class DealableExtensions
    {
        public static Vector3 GetCardPosition(this Dealable dealable, SeatPositionEnum seatPosition, int cardIndex) => dealable.transform.position + (CardPositionFactory.StartPositionBase(dealable.Cards.Count) - CardPositionFactory.CardPositionBase(cardIndex)).Multiply(SeatPositionFactory.SeatPositionPositionModifiers[seatPosition]).Multiply(CardPositionFactory.SpacingModifier);

        // private static Vector3 GetStartPosition(Dealable dealable) => dealable.transform.position + new Vector3((dealable.Cards.Count / 2f) * 0.25f, 0f, 0f);

        // public static Vector3 GetCardPositionByIndex(this Dealable dealable, int cardIndex) => GetStartPosition(dealable) + (new Vector3(-0.25f, 0f, 0.15f) * cardIndex);

        // public static Vector3 GetLastCardPosition(this Dealable dealable) => GetCardPositionByIndex(dealable, dealable.Cards.Count - 1);
    }
}
