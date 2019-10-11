using MatchingGame.DataSource;
using UnityEngine;

namespace MatchingGame.Managers
{
    [RequireComponent(typeof(ScoreManager))]
    [RequireComponent(typeof(CardManager))]
    [RequireComponent(typeof(LayoutManager))]
    [RequireComponent(typeof(GameDataSource))]
    public class GameManager : MonoBehaviour
    {
        private ScoreManager   _scoreManager;
        private CardManager    _cardManager;
        private LayoutManager  _layoutManager;
        private GameDataSource _gameDataSource;

        public void Initialize()
        {
            _scoreManager   = GetComponent<ScoreManager>();
            _cardManager    = GetComponent<CardManager>();
            _layoutManager  = GetComponent<LayoutManager>();
            _gameDataSource = GetComponent<GameDataSource>();
            
            _scoreManager.Initialize(_gameDataSource.CardPairsCount);
            _cardManager.Initialize(_scoreManager);
            _layoutManager.Initialize();

            StartGame(_gameDataSource.CardPairsCount);
        }

        private void StartGame(int cardPairsCount)
        {
            var cards = _cardManager.GetCardPairs(cardPairsCount);

            _cardManager.Register(cards);
            _cardManager.Shuffle(cards);

            _layoutManager.SetLayoutElements(cards);
        }
    }
}
