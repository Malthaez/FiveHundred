using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;

        public List<Card> Cards { get => _cards; private set => _cards = value; }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            card.transform.parent = transform;
        }

        public void Shuffle()
        {
            int n = _cards.Count;

            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                Card value = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = value;
            }
        }

        public Card Draw(int count)
        {
            var card = _cards[0];

            _cards.Remove(card);

            return card;
        }

        public Card Draw() => Draw(1);
    }
}
