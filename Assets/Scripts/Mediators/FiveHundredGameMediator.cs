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
        [SerializeField] private Player _dealer;
        [SerializeField] private Deck _deck;

        private int _drawingPlayerIndex;

        public void Initialize(List<Player> players)
        {
            _players = players;
        }

        public void SetDealer(int playerIndex)
        {
            _dealer = _players[_drawingPlayerIndex];
            Debug.Log($"Player {_players.IndexOf(_dealer)} is the new dealer");
        }

        public void IncreaseIndex()
        {
            _drawingPlayerIndex = _drawingPlayerIndex >= _players.Count - 1 ? 0 : _drawingPlayerIndex + 1;
            // var pause = stopCardValue != null && card.Value == stopCardValue ? 10.0f : 0f;
        }

        private IEnumerator DealUntilFirstJack()
        {
            // Before Deal

            SetDealer(0);
            _deck.AtEndOfEachDrawFrame = IncreaseIndex;
            _deck.OnDrawEnd = SetDealer;

            // Deal
            yield return _deck.Deal(_players, 0, (int)CardValuesEnum.Jack);

            // After Deal
            _deck.OnDrawEnd = null;
        }

        public IEnumerator Draw(Player player, int count)
        {
            yield return player.Draw(_deck);
        }

        public IEnumerator Deal(List<Player> players, int dealerIndex, int? stopCardValue)
        {
            int n = dealerIndex + 1;
            Debug.Log($"Player {n} is dealing");

            foreach (var card in _deck.Cards)
            {
                OnEachDealFrame?.Invoke(this);

                n = n >= players.Count - 1 ? 0 : n + 1;
                var pause = stopCardValue != null && card.Value == stopCardValue ? 10.0f : 0f;

                card.OnDrawEnd = players[n].AddToHand;
                yield return players[n].Draw(card);
                card.OnDrawEnd = null;
            }

            Cards = null;

            OnDealEnd?.Invoke(this);
        }

        public IEnumerator Deal(List<Player> players, int dealerIndex) => Deal(players, dealerIndex, null);

        public IEnumerator Deal(List<Player> players) => Deal(players, 0);
    }
}
