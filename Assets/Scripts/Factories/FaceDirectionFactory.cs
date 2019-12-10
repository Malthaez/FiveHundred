using MatchingGame.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Factories
{
    public static class FaceDirectionFactory
    {
        private static readonly Dictionary<FaceDirection, Vector3> _baseScores = new Dictionary<FaceDirection, Vector3>()
        {
            { FaceDirection.Up, new Vector3(0f, 0f, 180f) },
            { FaceDirection.Down, new Vector3(0f, 0f, 0f) },
        };

        public static Vector3 GetFaceDirectionRotation(FaceDirection direction) => _baseScores[direction];
    }
}
