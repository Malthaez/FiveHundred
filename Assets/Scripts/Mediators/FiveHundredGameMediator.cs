using MatchingGame.Behaviors;
using MatchingGame.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Mediators
{
    public class FiveHundredGameMediator : MonoBehaviour
    {
        [SerializeField] private List<Player> _players;
        [SerializeField] private Deck _deck;

        private int _dealerIndex;
        private int _drawingPlayerIndex;

        public void Initialize(List<Player> players)
        {
            _players = players;
        }

        private void DealUntilFirstJack() => StartCoroutine(Deal((int)CardValuesEnum.Jack));

        public IEnumerator Draw(Player player, int count)
        {
            for (int i = 0; i <= count; i++)
            {
                var card = _deck.Draw();

                card.OnMoveEnd = player.AddToHand;
                yield return card.MoveTo(player.GetNextCardPosition(), 60.0f);
                card.OnMoveEnd = null;
            }
        }

        public IEnumerator Deal(int? stopCardValue)
        {
            // Start dealing to the left of the dealer
            _drawingPlayerIndex = _dealerIndex + 1;

            Debug.Log($"Player {_drawingPlayerIndex} is dealing");

            foreach (var card in _deck.Cards)
            {
                _drawingPlayerIndex = _drawingPlayerIndex >= _players.Count - 1 ? 0 : _drawingPlayerIndex + 1;
                var pause = stopCardValue != null && card.Value == stopCardValue ? 10.0f : 0f;

                yield return Draw(_players[_drawingPlayerIndex], 1);
            }
        }
    }
}
