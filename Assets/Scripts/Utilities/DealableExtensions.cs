using MatchingGame.Behaviors;
using UnityEngine;

namespace MatchingGame.Utilities
{
    public static class DealableExtensions
    {
        public static Vector3 GetNextCardPosition(this Dealable dealable, int cardIndex, Vector3 startPosition) => startPosition + (new Vector3(-0.25f, 0f, 0.15f) * cardIndex);

        public static Vector3 GetNextCardPosition(this Dealable dealable, int cardIndex) => GetNextCardPosition(dealable, cardIndex, dealable.transform.position);
    }
}
