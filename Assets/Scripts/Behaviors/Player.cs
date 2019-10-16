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

            card.transform.position = transform.position + new Vector3 { x = 0.25f, y = 0f, z = 0.15f } * _hand.Count;

            _hand.Add(card);
        }
    }
}
