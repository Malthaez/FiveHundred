using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingGame.Behaviors.Card;

namespace MatchingGame.Behaviors
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;

        public List<Card> Cards { get => _cards; private set => _cards = value; }

        public OnDraw OnDrawStart { get; set; }
        public OnDraw AtEndOfEachDrawFrame { get; set; }
        public OnDraw OnDrawEnd { get; set; }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            card.transform.parent = transform;
        }

        public void Shuffle()
        {
            int n = Cards.Count;

            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                Card value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }

        public IEnumerator Draw(int count)
        {
            foreach (var card in Cards)
            {
                card.OnDrawEnd = OnDrawEnd;
                yield return players[n].Draw(card);
                card.OnDrawEnd = null;

                AtEndOfEachDrawFrame?.Invoke(card);
            }

            Cards = null;

            OnDrawEnd?.Invoke(this);
        }

        public IEnumerator Deal(List<Player> players, int dealerIndex) => Deal(players, dealerIndex, null);

        public IEnumerator Deal(List<Player> players) => Deal(players, 0);
    }
}
