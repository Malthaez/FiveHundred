using Assets.Scripts.Behaviors.Layout;
using Assets.Scripts.Behaviors.Mappers;
using MatchingGame.Behaviors;
using MatchingGame.DataSource;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Managers
{
    [RequireComponent(typeof(ScoreManager))]
    [RequireComponent(typeof(CardManager))]
    [RequireComponent(typeof(LayoutManager))]
    [RequireComponent(typeof(GameDataSource))]
    public class GameManager : MonoBehaviour
    {
        private ScoreManager _scoreManager;
        private CardManager _cardManager;
        private LayoutManager _layoutManager;
        private GameDataSource _gameDataSource;

        public void Initialize()
        {
            _scoreManager = GetComponent<ScoreManager>();
            _cardManager = GetComponent<CardManager>();
            _layoutManager = GetComponent<LayoutManager>();
            _gameDataSource = GetComponent<GameDataSource>();

            _scoreManager.Initialize(_gameDataSource.CardPairsCount);
            _cardManager.Initialize(_scoreManager);
            _layoutManager.Initialize();

            StartMemoryGame(_gameDataSource.CardPairsCount);
            //StartFiveHundredGame(_gameDataSource.Players);
        }

        private void StartMemoryGame(int cardPairsCount)
        {
            var cards = _cardManager.GetCardPairs(cardPairsCount);

            _cardManager.Register(cards);
            _cardManager.Shuffle(cards);

            _layoutManager.SetLayout(new MemoryGameLayout(cards.ToTransforms()));
        }

        private void StartFiveHundredGame(List<Player> players)
        {
            var cards = _cardManager.GetPlayingCardDeck();

            _cardManager.Register(cards);
            _cardManager.Shuffle(cards);
            _cardManager.Deal(players, cards);

            _layoutManager.SetLayout(new TestLayout(players.ToTransforms()));
        }
    }
}
