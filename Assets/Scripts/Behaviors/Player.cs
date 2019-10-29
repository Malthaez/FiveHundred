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

        public OnDraw OnDrawSuccess;
        public OnDraw OnFailedDraw;

        public void AddToHand(Card card)
        {
            _hand.Add(card);
            card.transform.SetParent(transform);
        }

        // Belongs in a rules class
        public IEnumerator CheckForJack(Card card) { yield return card.Value == (int)CardValuesEnum.Jack ? new WaitForSeconds(5f) : null; }

        public Vector3 GetNextCardPosition() => transform.position + (new Vector3 { x = 0.25f, y = 0f, z = 0.15f } * _hand.Count);

        public IEnumerator Draw(Deck deck, int count)
        {
            if (count <= 0) { yield break; }

            for (int i = 0; i < count; i++)
            {
                Card card = null;

                deck.OnSuccessfulDraw += (Card _card) => { card = _card; return null; };

                yield return deck.Draw();

                card.OnMoveEnd = AddToHand;
                yield return card.MoveTo(GetNextCardPosition(), 45.0f);
                card.OnMoveEnd = null;

                if (card != null)
                {
                    yield return OnDrawSuccess(card);
                }
                else
                {
                    yield return OnFailedDraw(card);
                }
            }
        }

        public IEnumerator Deal(List<Player> players, Deck deck)
        {
            // Start dealing to the left of the dealer
            int n = players.IndexOf(this) + 1;
            Debug.Log($"Player {n} is dealing");

            yield return deck.Deal(players, n);
        }

        public IEnumerator DealUntilFirstJack(List<Player> players, Deck deck)
        {
            players.ForEach((player) => player.OnDrawSuccess = player.CheckForJack);
            yield return Deal(players, deck);
        }
    }
}
