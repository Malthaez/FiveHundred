using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Behaviors.Layout;
using Assets.Scripts.Game.Behaviors.Mappers;
using Assets.Scripts.Game.DataSource;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    [RequireComponent(typeof(ScoreManager))]
    [RequireComponent(typeof(CardManager))]
    [RequireComponent(typeof(LayoutManager))]
    [RequireComponent(typeof(FiveHundredGameManager))]
    [RequireComponent(typeof(GameDataSource))]
    public class GameManager : MonoBehaviour
    {
        private ScoreManager _scoreManager;
        private CardManager _cardManager;
        private LayoutManager _layoutManager;
        private FiveHundredGameManager _fiveHundredGameManager;
        private GameDataSource _gameDataSource;

        public void Initialize()
        {
            _scoreManager = GetComponent<ScoreManager>();
            _cardManager = GetComponent<CardManager>();
            _layoutManager = GetComponent<LayoutManager>();
            _fiveHundredGameManager = GetComponent<FiveHundredGameManager>();
            _gameDataSource = GetComponent<GameDataSource>();

            _scoreManager.Initialize(_gameDataSource.CardPairsCount);
            _cardManager.Initialize(_scoreManager);
            _layoutManager.Initialize();
            _fiveHundredGameManager.Initialize();

            // StartMemoryGame(_gameDataSource.CardPairsCount);
            StartFiveHundredGame(_gameDataSource.Players, _gameDataSource.Kitty);
        }

        //private void StartMemoryGame(int cardPairsCount)
        //{
        //    var cards = _cardManager.GetCardPairs(cardPairsCount);

        //    _cardManager.Register(cards);
        //    _cardManager.Shuffle(cards);

        //    _layoutManager.SetLayout(new CardGridLayout(cards.ToTransforms(), new[] { 0.8f, 1.1f }, 6));
        //}

        private void StartFiveHundredGame(List<Player> players, Dealable kitty)
        {
            _layoutManager.SetLayout(new TestLayout(players.ToTransforms()));

            var deck = _cardManager.GetPlayingCardDeck();
            deck.transform.position = new Vector3(-5f, -3f, 0);

            StartCoroutine(_fiveHundredGameManager.StartFiveHundredGame(players, deck, kitty));
        }
    }
}
