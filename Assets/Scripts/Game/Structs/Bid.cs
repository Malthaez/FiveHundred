using Assets.Scripts.Game.Enums;
using UnityEngine;

namespace Assets.Scripts.Game.Structs
{
    public struct Bid
    {
        [SerializeField] private CardSuitsEnum? _suit;
        [SerializeField] private BidsEnum? _value;

        public CardSuitsEnum? Suit { get => _suit; set => _suit = value; }

        public BidsEnum? Value { get => _value; set => _value = value; }
    }
}
