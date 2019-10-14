using Assets.Scripts.Enums;
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
            card.transform.position = transform.position;
            card.transform.SetParent(transform);

            _hand.Add(card);
        }
    }
}
