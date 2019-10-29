using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;
        [SerializeField] private int _drawIndex;

        public delegate IEnumerator OnDraw(Card card);
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

        public IEnumerator Draw()
        {
            Card card = null;
            var drawCallback = OnFailedDraw;

            if (_drawIndex < _cards.Count)
            {
                card = _cards[_drawIndex];
                drawCallback = OnSuccessfulDraw;
                _drawIndex++;
            }

            yield return drawCallback(card);
        }

        public IEnumerator Deal(List<Player> players, int dealerIndex)
        {
            // Start dealing to the left of the dealer
            int n = dealerIndex + 1;

            // Get deck's card count since we can't alter the deck's contents while looping
            int count = _cards.Count;

            while (count > 0)
            {
                n = n >= players.Count - 1 ? 0 : n + 1;

                Debug.Log(count);
                OnSuccessfulDraw += (Card card) => { count--; return null; };
                yield return players[n].Draw(this, 1);
                OnSuccessfulDraw = null;
            }
        }
    }
}
