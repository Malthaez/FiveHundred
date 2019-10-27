using MatchingGame.Enums;
using System.Collections.Generic;

namespace MatchingGame.Behaviors.Layout.Factories
{
    public class SeatPositionFactory
    {
        private static Dictionary<SeatPositionEnum, int[]> seatPositions = new Dictionary<SeatPositionEnum, int[]>
        {
            { SeatPositionEnum.Bottom, new[] { 0, -1 } },
            { SeatPositionEnum.Left, new[] { -1, 0 } },
            { SeatPositionEnum.Top, new[] { 0, 1 } },
            { SeatPositionEnum.Right, new[] { 1, 0 } },
        };

        public static int[] GetSeatCoordinates(SeatPositionEnum seat) => seat != SeatPositionEnum.NONE ? seatPositions[seat] : null;
    }
}
