using MatchingGame.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.DataSource
{
    public class GameDataSource : MonoBehaviour
    {
        [SerializeField] private int _cardPairsCount;
        [SerializeField] private List<Player> _players;

        public int CardPairsCount { get => _cardPairsCount; set => _cardPairsCount = value; }
        public List<Player> Players { get => _players; set => _players = value; }
    }
}
