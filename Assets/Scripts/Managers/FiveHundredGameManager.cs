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
            yield return players[0].DealUntilFirstJack(players, deck);
            yield return deck.ReturnCards();
            Debug.Log("Done!");
        }
    }
}
