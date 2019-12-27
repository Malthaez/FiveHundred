using MatchingGame.Behaviors;
using UnityEngine;

namespace MatchingGame.Utilities
{
    public static class DealableExtensions
    {
        // Use Z Axis to rotate around from above
        public static float GetRotation(this Dealable dealable) => dealable.transform.rotation.eulerAngles.z;

        private static Vector3 GetStartPosition(Dealable dealable) => dealable.transform.position + new Vector3((dealable.Cards.Count / 2f) * 0.25f, 0f, 0f);

        public static Vector3 GetCardPositionByIndex(this Dealable dealable, int cardIndex) => GetStartPosition(dealable) + (new Vector3(-0.25f, 0f, 0.15f) * cardIndex);

        public static Vector3 GetLastCardPosition(this Dealable dealable) => GetCardPositionByIndex(dealable, dealable.Cards.Count - 1);
    }
}
