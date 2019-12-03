using MatchingGame.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Managers
{
    public class FiveHundredGameManager : MonoBehaviour
    {
        public void Initialize() { }

        public IEnumerator ReturnCardsToDeck(IEnumerable<Player> players, Deck deck)
        {
            List<Card> cards = new List<Card>();

            foreach (var player in players)
            {
                foreach (var card in player.Hand)
                {
                    cards.Add(card);
                }

                player.DiscardHand();
            }

            return deck.ReturnCards(cards);
        }

        public IEnumerator StartFiveHundredGame(List<Player> players, Deck deck)
        {
            Player dealer = null;

            yield return deck.Shuffle();
            yield return players[0].Deal(players, deck, (Player player, Card card) => dealer = Rules.CheckForJack(card) ? player : dealer, () => dealer == null);
            Debug.Log(dealer);
            yield return ReturnCardsToDeck(players, deck);
            yield return deck.Shuffle();
            yield return dealer.Deal(players, deck, null, null);
            Debug.Log("Done!");
        }
    }
}
