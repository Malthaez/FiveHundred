using MatchingGame.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.DataSource
{
    public class GameDataSource : MonoBehaviour
    {
        [SerializeField] private int _cardPairsCount;
        [SerializeField] private List<Player> _players;
        [SerializeField] private Dealable _kitty;

        public int CardPairsCount => _cardPairsCount;
        public List<Player> Players => _players;
        public Dealable Kitty => _kitty;
    }
}
