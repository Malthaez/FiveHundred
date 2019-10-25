using MatchingGame.Behaviors;
using MatchingGame.Behaviors.Layout;
using MatchingGame.Behaviors.Mappers;
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
        private FiveHundredGameManager _fiveHundredGameManager;

        public void Initialize()
        {
            _scoreManager = GetComponent<ScoreManager>();
            _cardManager = GetComponent<CardManager>();
            _layoutManager = GetComponent<LayoutManager>();
            _gameDataSource = GetComponent<GameDataSource>();
            _fiveHundredGameManager = GetComponent<FiveHundredGameManager>();

            _scoreManager.Initialize(_gameDataSource.CardPairsCount);
            _cardManager.Initialize(_scoreManager);
            _layoutManager.Initialize();
            _fiveHundredGameManager.Initialize(_gameDataSource.Players);

            //StartMemoryGame(_gameDataSource.CardPairsCount);
            StartFiveHundredGame(_gameDataSource.Players);
        }

        private void StartMemoryGame(int cardPairsCount)
        {
            var cards = _cardManager.GetCardPairs(cardPairsCount);

            _cardManager.Register(cards);
            _cardManager.Shuffle(cards);

            _layoutManager.SetLayout(new CardGridLayout(cards.ToTransforms(), new[] { 0.8f, 1.1f }, 6));
        }

        private void StartFiveHundredGame(List<Player> players)
        {
            var deck = _cardManager.GetPlayingCardDeck();

            _cardManager.Register(deck.Cards);
            _fiveHundredGameManager.Register(deck);
            _fiveHundredGameManager.Register(players);
            _layoutManager.SetLayout(new TestLayout(players.ToTransforms()));

            StartCoroutine(_fiveHundredGameManager.FiveHundredGame(deck));
        }
    }
}
