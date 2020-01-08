using Assets.Scripts.Game.Behaviors.Layout.Factories;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Behaviors.Layout
{
    public class TestLayout : CardLayout
    {
        private List<Player> _players;
        private Deck _deck;

        public TestLayout(List<Transform> layoutElements) : base(layoutElements) { }

        public override void Refresh()
        {
            int n = 0;
            foreach (var element in _layoutElements)
            {
                element.transform.position = GetPosition(SeatPositionFactory.GetSeatCoordinates(element.GetComponent<Player>().Seat), new[] { 0.25f * n, 0f * n });
                n++;
            }
        }

        private Vector3 GetPosition(int[] coordinates, float[] offset)
            => new Vector3
            {
                x = 4.5f * coordinates[0],
                y = 4.5f * coordinates[1]
            };
    }
}
