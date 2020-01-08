using Assets.Scripts.Game.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.DataSource
{
    /// <summary>
    /// All values in this class should be set in the inspector
    /// </summary>
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
