using MatchingGame.Enums;
using MatchingGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public class Player : Dealable
    {
        [SerializeField] private SeatPositionEnum _seat;

        public SeatPositionEnum Seat { get => _seat; set => _seat = value; }

        public IEnumerator AddToHand(Card card)
        {
            Cards.Add(card);

            var startPosition = transform.position + new Vector3((Cards.Count / 2f) * 0.25f, 0f, 0f);

            yield return card.MoveTo(GetNextCardPosition(Cards.Count - 1, startPosition), 45.0f, () => { if (_seat == SeatPositionEnum.Bottom) { card.FlipUp(); }; StartCoroutine(ArrangeHand(startPosition)); });
            card.transform.SetParent(transform);
        }

        public void DiscardHand() => DiscardCards();

        public IEnumerator ArrangeHand(Vector3 startPosition)
        {
            var coroutines = new List<Coroutine>();

            for (int i = 0; i < Cards.Count; i++)
            {
                coroutines.Add(StartCoroutine(Cards[i].MoveTo(GetNextCardPosition(i, startPosition), 40f, null)));
            }

            yield return this.AwaitAllCoroutines(coroutines);
        }

        public Vector3 GetNextCardPosition(int positionInHand, Vector3 startPosition) => startPosition + (new Vector3(-0.25f, 0f, 0.15f) * positionInHand);

        public Vector3 GetNextCardPosition(int positionInHand) => GetNextCardPosition(positionInHand, transform.position);

        public IEnumerator Draw(Deck deck, int drawCount, Action<Player, Card> onSuccessfulDraw)
        {
            if (drawCount <= 0) { yield break; }

            for (int i = 0; i < drawCount; i++)
            {
                yield return deck.Draw(this, onSuccessfulDraw, (Card card) => AddToHand(card));
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

                // Debug.Log(count);
                yield return players[n].Draw(deck, 1, onSuccessfulDraw);
            }
        }
    }
}
