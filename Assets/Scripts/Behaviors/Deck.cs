using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingGame.Utilities.CoroutineUtilities;

namespace MatchingGame.Behaviors
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;
        [SerializeField] private int _drawIndex;

        public List<Card> Cards { get => _cards; private set => _cards = value; }
        public int DrawIndex => _drawIndex;

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.transform.parent = transform;
        }

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

            yield return AwaitAllCoroutines(coroutines);
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

            foreach(var movement in movements)
            {
                yield return card.MoveTo(transform.position + movement, 10.0f, null);
            }
        }

        private List<Coroutine> GetReturnCardCoroutines(IEnumerable<Card> cards)
        {
            var coroutines = new List<Coroutine>();

            foreach (var card in cards)
            {
                coroutines.Add(StartCoroutine(card.MoveTo(transform.position, 60.0f, () => { card.transform.parent = transform; if (card.Flipped || card.Flipping) { card.FlipDown(); } })));
            }

            return coroutines;
        }

        public IEnumerator ReturnCards(IEnumerable<Card> cards)
        {
            yield return AwaitAllCoroutines(GetReturnCardCoroutines(cards));
            _drawIndex = 0;
        }

        public IEnumerator Draw(Player player, Action<Player, Card> onSuccessfulDraw, Func<Card, IEnumerator> awaitOnSuccessfulDraw)
        {
            Card card = null;
            Action<Player, Card> callback = null;
            Func<Card, IEnumerator> awaitCallback = null;

            if (_drawIndex < _cards.Count)
            {
                card = _cards[_drawIndex];
                callback = onSuccessfulDraw;
                awaitCallback = awaitOnSuccessfulDraw;
                _drawIndex++;
            }

            callback(player, card);
            yield return awaitCallback.Invoke(card);
        }
    }
}
