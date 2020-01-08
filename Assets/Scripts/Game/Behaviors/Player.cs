using Assets.Scripts.Game.Enums;
using Assets.Scripts.Game.Interfaces;
using Assets.Scripts.Game.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Behaviors
{
    /// <summary>
    /// A Dealable that can take actions in a game
    /// </summary>
    public class Player : Dealable, IDealer
    {
        [SerializeField] private SeatPositionEnum _seat;
        [SerializeField] public Bid bid;

        public SeatPositionEnum Seat { get => _seat; set => _seat = value; }

        //public Bid Bid { get => _bid; set => _bid = value; }

        public void DiscardHand() => DiscardCards();

        public IEnumerator DealTo(Dealable dealable, List<Card> cards, Action<Dealable, Card> onSuccessfulDeal)
        {
            foreach (var card in cards)
            {
                yield return dealable.ReceiveCard(card);
                onSuccessfulDeal?.Invoke(dealable, card);
            }
        }

        public IEnumerator Deal(List<Dealable> dealables, Deck deck, int[] dealCount, Action<Dealable, Card> onSuccessfulDeal, Func<bool> continueDeal, Func<Dealable, bool> skipDealable)
        {
            // Start dealing to the left of the dealer
            var dealIndex = dealables.IndexOf(this) + 1;
            var dealCountIndex = 0;
            Debug.Log($"Player {dealIndex} is dealing");

            var cards = new List<Card>();

            while (deck.DrawIndex < deck.Cards.Count && (continueDeal != null ? continueDeal() : true))
            {
                dealIndex++;
                dealIndex %= dealables.Count;

                // Check to see if we skip this dealable
                if (skipDealable(dealables[dealIndex])) { continue; }

                cards = deck.Take(dealCount[dealCountIndex]);
                yield return DealTo(dealables[dealIndex], cards, onSuccessfulDeal);

                if (dealIndex == dealables.IndexOf(this))
                {
                    dealCountIndex++;
                    dealCountIndex %= dealCount.Length;
                }
            }
        }

        public IEnumerator Deal(List<Dealable> dealables, Deck deck, int dealCount, Action<Dealable, Card> onSuccessfulDeal, Func<bool> continueDeal, Func<Dealable, bool> skipDealable) => Deal(dealables, deck, new[] { dealCount }, onSuccessfulDeal, continueDeal, skipDealable);
    }
}
