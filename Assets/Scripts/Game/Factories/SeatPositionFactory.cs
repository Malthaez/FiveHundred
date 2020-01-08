using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Factories
{
    public static class SeatPositionFactory
    {
        private static readonly Dictionary<SeatPositionEnum, Vector3> _seatPositionPositionModifiers = new Dictionary<SeatPositionEnum, Vector3>()
        {
            { SeatPositionEnum.NONE, new Vector3(0f, 0f, 1f) },
            { SeatPositionEnum.Bottom, new Vector3(1f, 0f, 1f) },
            { SeatPositionEnum.Left, new Vector3(0f, 1f, 1f) },
            { SeatPositionEnum.Top, new Vector3(-1f, 0f, 1f) },
            { SeatPositionEnum.Right, new Vector3(0f, -1f, 1f) },
        };

        public static Dictionary<SeatPositionEnum, Vector3> SeatPositionPositionModifiers => _seatPositionPositionModifiers;

        public static SeatPositionEnum GetSeatPosition(Dealable dealable) => (dealable is Player) ? (dealable as Player).Seat : SeatPositionEnum.NONE;
    }
}
