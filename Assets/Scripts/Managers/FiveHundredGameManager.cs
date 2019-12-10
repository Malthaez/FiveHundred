using MatchingGame.Behaviors;
using MatchingGame.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
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
            var dealables = new List<Dealable>();
            foreach (var player in players)
            {
                dealables.Add(player);
            }
            deck.RemoveCards(new[] { CardValuesEnum.Two, CardValuesEnum.Three });

            Action<Dealable, Card> dealCallback = null;
            dealCallback += (Dealable dealable, Card card) => dealer = Rules.CheckForJack(card) ? dealable as Player : dealer;
            dealCallback += (Dealable dealable, Card card) =>
            {
                if (dealable as Player != null && ((Player)dealable).Seat == SeatPositionEnum.Bottom)
                {
                    card.FlipUp();
                };
                StartCoroutine(dealable.ArrangeCards(dealable.transform.position + new Vector3((dealable.Cards.Count / 2f) * 0.25f, 0f, 0f)));
            };

            yield return deck.Shuffle();
            yield return players[0].Deal(dealables, deck, 1, dealCallback, () => dealer == null);
            Debug.Log(dealer);
            yield return ReturnCardsToDeck(dealables, deck);
            yield return deck.Shuffle();
            dealables.Add(kitty);

            dealCallback = null;
            dealCallback += (Dealable dealable, Card card) =>
            {
                if (dealable as Player != null && ((Player)dealable).Seat == SeatPositionEnum.Bottom)
                {
                    card.FlipUp();
                };
                StartCoroutine(dealable.ArrangeCards(dealable.transform.position + new Vector3((dealable.Cards.Count / 2f) * 0.25f, 0f, 0f)));
            };

            yield return dealer.Deal(dealables, deck, 3, dealCallback, null);
            Debug.Log("Done!");
        }
    }
}
