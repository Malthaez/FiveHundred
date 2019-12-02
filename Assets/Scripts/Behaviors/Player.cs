using MatchingGame.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SeatPositionEnum _seat;
        [SerializeField] private List<Card> _hand;

        public SeatPositionEnum Seat { get => _seat; set => _seat = value; }
        public List<Card> Hand => _hand;

        public void AddToHand(Card card)
        {
            _hand.Add(card);
            card.transform.SetParent(transform);
        }

        public void DiscardHand()
        {
            _hand.Clear();
        }

        public Vector3 GetNextCardPosition() => transform.position + (new Vector3 { x = 0.25f, y = 0f, z = 0.15f } * _hand.Count);

        public IEnumerator Draw(Deck deck, int drawCount, Action<Player, Card> onSuccessfulDraw)
        {
            if (drawCount <= 0) { yield break; }

            onSuccessfulDraw += (Player player, Card card) => AddToHand(card);

            for (int i = 0; i < drawCount; i++)
            {
                yield return deck.Draw(this, onSuccessfulDraw, (Card card) => card.MoveTo(GetNextCardPosition(), 45.0f));
            }
        }

        public IEnumerator Deal(List<Player> players, Deck deck, Action<Player, Card> onSuccessfulDraw, Func<bool> continueDeal)
        {
            // Start dealing to the left of the dealer
            var n = players.IndexOf(this) + 1;
            Debug.Log($"Player {n} is dealing");

            // Get deck's card count since we can't alter the deck's contents while looping
            int count = deck.Cards.Count;

            onSuccessfulDraw += (Player player, Card card) => { count--; };

            while (count > 0 && (continueDeal != null ? continueDeal() : true))
            {
                n++;
                n %= players.Count;

                Debug.Log(count);
                yield return players[n].Draw(deck, 1, onSuccessfulDraw);
            }
        }
    }
}
