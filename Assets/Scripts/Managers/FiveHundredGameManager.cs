using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchingGame.Managers
{
    public class FiveHundredGameManager : MonoBehaviour
    {
        public void Initialize() { }

        public IEnumerator ReturnCardsToDeck(IEnumerable<Dealable> dealable, Deck deck)
        {
            List<Card> cards = new List<Card>();

            foreach (var player in dealable)
            {
                foreach (var card in player.Cards)
                {
                    cards.Add(card);
                }

                player.DiscardCards();
            }

            return deck.ReturnCards(cards);
        }

        public IEnumerator StartFiveHundredGame(List<Player> players, Deck deck, Dealable kitty)
        {
            Player dealer = null;
            var dealables = players.Cast<Dealable>().ToList();
            deck.RemoveCardsByValues(new[] { CardValuesEnum.Two, CardValuesEnum.Three });
            deck.RemoveCard(deck.Cards.First(card => (CardValuesEnum)card.Value == CardValuesEnum.Joker));

            Action<Dealable, Card> dealCallback = null;
            dealCallback += (Dealable dealable, Card card) => dealer = Rules.CheckForJack(card) ? dealable as Player : dealer;
            dealCallback += (Dealable dealable, Card card) =>
            {
                if (dealable as Player != null && ((Player)dealable).Seat == SeatPositionEnum.Bottom)
                {
                    StartCoroutine(card.Flip(FaceDirection.Up, 0.1f));
                };
                StartCoroutine(dealable.ArrangeCards(dealable.transform.position + new Vector3((dealable.Cards.Count / 2f) * 0.25f, 0f, 0f)));
            };

            yield return deck.Shuffle();
            yield return players[0].Deal(dealables, deck, 1, dealCallback, () => dealer == null, (Dealable dealable) => dealable.name == "Kitty");
            Debug.Log(dealer);
            yield return ReturnCardsToDeck(dealables, deck);
            yield return deck.Shuffle();
            dealables.Add(kitty);

            dealCallback = null;
            dealCallback += (Dealable dealable, Card card) =>
            {
                if (dealable as Player != null && ((Player)dealable).Seat == SeatPositionEnum.Bottom)
                {
                    StartCoroutine(card.Flip(FaceDirection.Up, 0.1f));
                };
                StartCoroutine(dealable.ArrangeCards(dealable.transform.position + new Vector3((dealable.Cards.Count / 2f) * 0.25f, 0f, 0f)));
            };

            yield return dealer.Deal(GetDealables(dealables, dealer), deck, new[] { 3, 2 }, dealCallback, null, (Dealable dealable) => dealable.name == "Kitty" && dealable.Cards.Count >= 5);
            Debug.Log("Done!");
        }

        private List<Dealable> GetDealables(List<Dealable> dealTargets, IDealer dealer)
        {
            // Figure out which target is the Kitty
            var kitty = dealTargets.Where(targets => targets.name == "Kitty");

            // Create new list excluding Kitty for now because Kitty gets dealt to last
            var newList = dealTargets.Except(kitty).ToList();

            // Start dealing to the Player to the left of the dealer
            var dealOffset = newList.IndexOf(dealer as Player) + 1;
            dealOffset = dealOffset < newList.Count ? dealOffset : 0;

            // Build final list starting from first deal target
            var finalList = newList.GetRange(dealOffset, newList.Count - dealOffset);
            finalList.AddRange(newList.GetRange(0, dealOffset));
            finalList.AddRange(kitty);

            return finalList;
        }
    }
}
