using UnityEngine;

namespace MatchingGame.DataSource
{
    public class GameDataSource : MonoBehaviour
    {
        public int _cardPairsCount;

        public int CardPairsCount { get => _cardPairsCount; set => _cardPairsCount = value; }
    }
}
