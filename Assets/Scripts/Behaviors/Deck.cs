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

        public void Shuffle()
        {
            int n = _cards.Count;

            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                Card value = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = value;
            }
        }

        private List<Coroutine> GetReturnCardCoroutines(IEnumerable<Card> cards)
        {
            var coroutines = new List<Coroutine>();

            foreach (var card in cards)
            {
                card.OnMoveEnd = (Card _card) => card.transform.parent = transform;
                coroutines.Add(StartCoroutine(card.MoveTo(transform.position, 60.0f)));
                card.OnMoveEnd = null;
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
