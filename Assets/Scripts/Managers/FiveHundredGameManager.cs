using MatchingGame.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Managers
{
    public class FiveHundredGameManager : MonoBehaviour
    {
        public void Initialize() { }

        public IEnumerator StartFiveHundredGame(List<Player> players, Deck deck)
        {
            deck.Shuffle();
            Player dealer = null;

            yield return players[0].Deal(players, deck, (Player player, Card card) => dealer = Rules.CheckForJack(card) ? player : dealer, () => dealer == null);
            // yield return deck.ReturnCards();
            // yield return dealer.Deal(players, deck);
            Debug.Log("Done!");
            Debug.Log(dealer);
        }
    }
}
