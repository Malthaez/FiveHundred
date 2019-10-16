using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors.Mappers
{
    public static class PlayerMapper
    {
        public static List<Transform> ToTransforms(this List<Player> players)
        {
            var playerTransforms = new List<Transform>();

            foreach (var player in players) { playerTransforms.Add(player.transform); }

            return playerTransforms;
        }
    }
}
