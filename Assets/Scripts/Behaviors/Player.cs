using MatchingGame.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingGame.Utilities.CoroutineUtilities;

namespace MatchingGame.Behaviors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SeatPositionEnum _seat;
        [SerializeField] private List<Card> _hand;

        public SeatPositionEnum Seat { get => _seat; set => _seat = value; }
        public List<Card> Hand => _hand;

        public IEnumerator AddToHand(Card card)
        {
            _hand.Add(card);

            var startPosition = transform.position + new Vector3((_hand.Count / 2f) * 0.25f, 0f, 0f);

            yield return card.MoveTo(GetNextCardPosition(_hand.Count - 1, startPosition), 45.0f, () => { if (_seat == SeatPositionEnum.Bottom) { card.FlipUp(); }; StartCoroutine(ArrangeHand(startPosition)); });
            card.transform.SetParent(transform);
        }

        public void DiscardHand()
        {
            _hand.Clear();
        }

        public IEnumerator ArrangeHand(Vector3 startPosition)
        {
            var coroutines = new List<Coroutine>();

            for (int i = 0; i < _hand.Count; i++)
            {
                coroutines.Add(StartCoroutine(_hand[i].MoveTo(GetNextCardPosition(i, startPosition), 40f, null)));
            }

            yield return AwaitAllCoroutines(coroutines);
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
