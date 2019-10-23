using MatchingGame.Enums;
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

        public void AddToHand(Card card)
        {
            _hand.Add(card);
            card.transform.SetParent(transform);
        }

        public Vector3 GetNextCardPosition()
        {
            return transform.position + (new Vector3 { x = 0.25f, y = 0f, z = 0.15f } * _hand.Count);
        }

        public IEnumerator Draw(Deck deck)
        {
            deck.OnDrawEnd = AddToHand;
            yield return deck.Draw(GetNextCardPosition());
        }
    }
}
