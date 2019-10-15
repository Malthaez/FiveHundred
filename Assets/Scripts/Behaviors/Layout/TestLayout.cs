using Assets.Scripts.Behaviors.Mappers;
using MatchingGame.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Layout
{
    public class TestLayout : Layout
    {
        private List<Player> _players;

        public TestLayout(List<Transform> layoutElements) : base(layoutElements) { }

        public override void Refresh()
        {
            foreach (var element in _layoutElements)
            {
                element.transform.position = GetPosition(SeatPositionFactory.GetSeatCoordinates(element.GetComponent<Player>().Seat));
            }
        }

        private Vector3 GetPosition(int[] coordinates)
            => new Vector3
            {
                x = 2 * coordinates[0],
                y = 2 * coordinates[1]
            };
    }
}
