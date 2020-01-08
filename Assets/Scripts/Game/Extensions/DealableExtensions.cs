using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Factories;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Game.Extensions
{
    public static class DealableExtensions
    {
        public static Vector3 GetCardPosition(this Dealable dealable, int cardIndex) => dealable.transform.position + (CardPositionFactory.StartPositionBase(dealable.Cards.Count) - CardPositionFactory.CardPositionBase(cardIndex)).Multiply(SeatPositionFactory.SeatPositionPositionModifiers[SeatPositionFactory.GetSeatPosition(dealable)]).Multiply(CardPositionFactory.SpacingModifier);
    }
}
