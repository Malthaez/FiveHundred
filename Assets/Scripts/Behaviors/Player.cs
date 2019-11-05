using MatchingGame.Enums;
using MatchingGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingGame.Utilities.Utilities;

namespace MatchingGame.Behaviors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SeatPositionEnum _seat;
        [SerializeField] private List<Card> _hand;

        public SeatPositionEnum Seat { get => _seat; set => _seat = value; }

        //public OnDraw OnDrawSuccess;
        //public OnDraw OnFailedDraw;

        public void AddToHand(Card card)
        {
            _hand.Add(card);
            card.transform.SetParent(transform);
        }

        public void DiscardHand()
        {
            _hand.Clear();
        }

        // Belongs in a rules class
        public IEnumerator CheckForJack(Card card) { yield return card.Value == (int)CardValuesEnum.Jack ? new WaitForSeconds(0.5f) : null; }

        public Vector3 GetNextCardPosition() => transform.position + (new Vector3 { x = 0.25f, y = 0f, z = 0.15f } * _hand.Count);

        public IEnumerator Draw(Deck deck, int count, CoroutineAction<Card> actions)
        {
            if (count <= 0) { yield break; }

            for (int i = 0; i < count; i++)
            {
                Card card = null;

                deck.Draw((Card _card) => { card = _card; });

                card.OnMoveEnd = AddToHand;
                yield return card.MoveTo(GetNextCardPosition(), 45.0f);
                card.OnMoveEnd = null;

                if (card != null)
                {
                    actions.onSuccess(card);
                    //yield return AwaitAllCoroutines(actions.yieldOnSuccess);
                }
                else
                {
                    actions.onFailure(card);
                    //yield return AwaitAllCoroutines(actions.yieldOnFailure);
                }
            }
        }

        public IEnumerator Deal(List<Player> players, Deck deck)
        {
            // Start dealing to the left of the dealer
            var n = players.IndexOf(this) + 1;
            Debug.Log($"Player {n} is dealing");

            // Get deck's card count since we can't alter the deck's contents while looping
            int count = deck.Cards.Count;

            var drawActions = new CoroutineAction<Card>
            {
                onSuccess = (Card card) => { count--; }
            };

            while (count > 0)
            {
                n++;
                n %= players.Count;

                Debug.Log(count);
                yield return players[n].Draw(deck, 1, drawActions);
            }
        }

        public IEnumerator DealUntilFirstJack(List<Player> players, Deck deck)
        {
            // players.ForEach((player) => player.OnDrawSuccess += player.CheckForJack);
            yield return Deal(players, deck);
        }
    }
}
