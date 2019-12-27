using MatchingGame.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Factories
{
    public static class FaceDirectionFactory
    {
        private static readonly Dictionary<FaceDirection, Vector3> _baseScores = new Dictionary<FaceDirection, Vector3>()
        {
            { FaceDirection.Up, new Vector3(0f, 180f, 0f) },
            { FaceDirection.Down, new Vector3(0f, 0f, 0f) },
        };

        private static readonly Dictionary<SeatPositionEnum, Vector3> _baseSeatRotations = new Dictionary<SeatPositionEnum, Vector3>()
        {
            { SeatPositionEnum.NONE, new Vector3(0f, 0f, 0f) },
            { SeatPositionEnum.Bottom, new Vector3(0f, 0f, 0f) },
            { SeatPositionEnum.Left, new Vector3(0f, 0f, 270f) },
            { SeatPositionEnum.Top, new Vector3(0f, 0f, 180f) },
            { SeatPositionEnum.Right, new Vector3(0f, 0f, 90f) },
        };

        public static Vector3 GetFaceDirectionRotation(FaceDirection direction) => _baseScores[direction];

        public static Vector3 GetSeatRotation(SeatPositionEnum seatPosition) => _baseSeatRotations[seatPosition];
    }
}
