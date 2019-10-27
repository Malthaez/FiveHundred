using MatchingGame.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingGame.Behaviors.Deck;

namespace MatchingGame.Behaviors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SeatPositionEnum _seat;
        [SerializeField] private List<Card> _hand;

        public SeatPositionEnum Seat { get => _seat; set => _seat = value; }

        public OnDraw OnDrawStart;
        public OnDraw OnEachDrawFrame;
        public OnDraw OnDrawEnd;

        public void AddToHand(Card card)
        {
            _hand.Add(card);
            card.transform.SetParent(transform);
        }

        public Vector3 GetNextCardPosition() => transform.position + (new Vector3 { x = 0.25f, y = 0f, z = 0.15f } * _hand.Count);

        public IEnumerator Draw(Deck deck, int count)
        {
            OnDrawStart?.Invoke();

            if (count <= 0) { yield break; }

            for (int i = 0; i < count; i++)
            {
                OnEachDrawFrame?.Invoke();

                var card = deck.Draw();

                if (card == null)
                {
                    Debug.Log("Can't draw. There are no more cards.");
                    yield break;
                }

                card.OnMoveEnd = AddToHand;
                yield return card.MoveTo(GetNextCardPosition(), 45.0f);
                card.OnMoveEnd = null;
            }

            OnDrawEnd?.Invoke();
        }

        public IEnumerator Deal(List<Player> players, Deck deck, int? stopCardValue)
        {
            // Start dealing to the left of the dealer
            int n = players.IndexOf(this) + 1;

            Debug.Log($"Player {n} is dealing");

            yield return deck.Deal(players, n, null);
        }
    }
}
