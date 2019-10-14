using Assets.Scripts.Behaviors.Mappers;
using MatchingGame.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Layout
{
    public class TestLayout : Layout
    {
        private List<Player> _players;

        public TestLayout(List<Player> players) : base(players.ToTransforms())
        {
            _players = players;
        }

        public override void Refresh()
        {
            foreach (var player in _players)
            {
                player.transform.position = GetPosition(SeatPositionFactory.GetSeatCoordinates(player.Seat));
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
