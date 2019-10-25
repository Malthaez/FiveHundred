using MatchingGame.Behaviors;
using MatchingGame.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingGame.Behaviors.Deck;

namespace MatchingGame.Mediators
{
    public class FiveHundredGameMediator : MonoBehaviour
    {
        [SerializeField] private List<Player> _players;
        [SerializeField] private Deck _deck;

        private int _dealerIndex;
        private int _drawingPlayerIndex;

        public OnDraw OnDrawStart;
        public OnDraw OnEachDrawFrame;
        public OnDraw OnDrawEnd;

        public void Initialize(List<Player> players)
        {
            _players = players;
        }

        public bool Register(Deck deck)
        {
            bool isRegistered = false;

            try
            {
                if (deck == null) { return false; }
                _deck = deck;
                deck.transform.SetParent(deck.transform);
                isRegistered = true;
            }
            catch { }

            return isRegistered;
        }

        public bool Register(List<Player> players)
        {
            bool isRegistered = false;

            try
            {
                if (players == null) { return false; }
                _players = players;
                isRegistered = true;
            }
            catch { }

            return isRegistered;
        }

        private void DealUntilFirstJack() => StartCoroutine(Deal((int)CardValuesEnum.Jack));

        public IEnumerator Draw(Player player, int count)
        {
            OnDrawStart?.Invoke();

            if (count <= 0) { yield break; }

            for (int i = 0; i < count; i++)
            {
                if (_deck.Cards.Count == 0)
                {
                    Debug.Log("Can't draw. There are no more cards.");
                    yield break;
                }

                OnEachDrawFrame?.Invoke();

                var card = _deck.Draw();

                card.OnMoveEnd = player.AddToHand;
                yield return card.MoveTo(player.GetNextCardPosition(), 45.0f);
                card.OnMoveEnd = null;
            }

            OnDrawEnd?.Invoke();
        }

        public IEnumerator Deal(int? stopCardValue)
        {
            // Start dealing to the left of the dealer
            _drawingPlayerIndex = _dealerIndex + 1;

            Debug.Log($"Player {_drawingPlayerIndex} is dealing");

            int count = _deck.Cards.Count;

            _deck.OnSuccessfulDraw = () => count--;

            while (count > 0)
            {
                _drawingPlayerIndex = _drawingPlayerIndex >= _players.Count - 1 ? 0 : _drawingPlayerIndex + 1;
                // var pause = stopCardValue != null && card.Value == stopCardValue ? 10.0f : 0f;

                Debug.Log(count);
                yield return Draw(_players[_drawingPlayerIndex], 1);
            }

            _deck.OnSuccessfulDraw = null;
        }
    }
}
