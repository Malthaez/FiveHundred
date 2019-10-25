using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;
        [SerializeField] private int _drawIndex;

        public delegate void OnDraw();
        public OnDraw OnSuccessfulDraw;
        public OnDraw OnFailedDraw;

        public List<Card> Cards { get => _cards; private set => _cards = value; }
        public int DrawIndex => _drawIndex;

        public void AddCard(Card card)
        {
            _cards.Add(card);
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

        public Card Draw()
        {
            Card card = null;

            if (_drawIndex < _cards.Count)
            {
                OnSuccessfulDraw();
                card = _cards[_drawIndex];
                _drawIndex++;
            }
            else
            {
                OnFailedDraw();
            }

            return card;
        }
    }
}
