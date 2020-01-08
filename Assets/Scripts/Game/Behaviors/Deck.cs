using Assets.Scripts.Game.Enums;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game.Behaviors
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;
        [SerializeField] private List<Card> _removedCards;
        [SerializeField] private int _drawIndex;

        public List<Card> Cards { get => _cards; private set => _cards = value; }
        public int DrawIndex => _drawIndex;

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.transform.parent = transform;
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            card.gameObject.SetActive(false);
            _removedCards.Add(card);
        }

        public void RemoveCards(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                RemoveCard(card);
            }
        }

        public void RemoveCardsByValues(IEnumerable<CardValuesEnum> cardValues) => RemoveCards(_cards.Where(card => cardValues.Any(value => card.Value == (int)value)).ToList());

        public IEnumerator Shuffle()
        {
            List<Coroutine> coroutines = new List<Coroutine>();
            int n = _cards.Count;

            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                Card value = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = value;
                coroutines.Add(StartCoroutine(MoveCardInDeck(_cards[k], k)));
            }

            yield return this.AwaitAllCoroutines(coroutines);
        }

        public IEnumerator MoveCardInDeck(Card card, int newIndex)
        {
            int direction = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;

            var movements = new[]
            {
                new Vector3(0.00f, 1.00f * direction, 0.00f), // Out
                new Vector3(0.00f, 1.00f * direction, 0.15f * newIndex), // Up-Down
                new Vector3(0.00f, 0.00f, 0.15f * newIndex) // In
            };

            foreach (var movement in movements)
            {
                var coroutines = card.DoCardStuff(transform.position + movement, transform.rotation, 10.0f);
                yield return this.AwaitAllCoroutines(coroutines);
            }
        }

        private List<Coroutine> GetReturnCardCoroutines(IEnumerable<Card> cards)
        {
            var coroutines = new List<Coroutine>();

            foreach (var card in cards)
            {
                coroutines.AddRange(card.DoCardStuff(transform.position, transform.rotation, 60.0f));
                //coroutines.Add(StartCoroutine(card.Flip(FaceDirection.Down, 0.1f)));
                card.transform.parent = transform;
            }

            return coroutines;
        }

        public IEnumerator ReturnCards(IEnumerable<Card> cards)
        {
            yield return this.AwaitAllCoroutines(GetReturnCardCoroutines(cards));
            _drawIndex = 0;
        }

        public Card Take()
        {
            Debug.Log(_cards.Count - _drawIndex);
            Card card = null;

            if (_drawIndex < _cards.Count)
            {
                card = _cards[_drawIndex];
                _drawIndex++;
            }

            return card;
        }

        public List<Card> Take(int count)
        {
            var cards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                var card = Take();
                if (card == null) { break; }
                cards.Add(card);
            }

            return cards;
        }
    }
}
