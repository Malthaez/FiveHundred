using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Factories;
using UnityEngine;

namespace MatchingGame.Utilities
{
    public static class DealableExtensions
    {
        public static Vector3 GetCardPosition(this Dealable dealable, int cardIndex) => dealable.transform.position + (CardPositionFactory.StartPositionBase(dealable.Cards.Count) - CardPositionFactory.CardPositionBase(cardIndex)).Multiply(SeatPositionFactory.SeatPositionPositionModifiers[SeatPositionFactory.GetSeatPosition(dealable)]).Multiply(CardPositionFactory.SpacingModifier);
    }
}
