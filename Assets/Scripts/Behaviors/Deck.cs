using MatchingGame.Enums;
using MatchingGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
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

        public void RemoveCards(IEnumerable<CardValuesEnum> cardValues)
        {
            var cards = new List<Card>();

            foreach (var card in _cards)
            {
                if (card.Value == (int)CardValuesEnum.Two || card.Value == (int)CardValuesEnum.Three)
                {
                    cards.Add(card);
                }
            }

            RemoveCards(cards);
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
            yield return this.AwaitAllCoroutines(GetReturnCardCoroutines(cards));
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

        // TODO: Possibly don't use these Deal methods, but we'll see.

        public IEnumerator Deal(Dealable dealable, Action<Dealable, Card> onSuccessfulDeal, Func<Card, IEnumerator> awaitOnSuccessfulDeal)
        {
            Card card = null;
            Action<Dealable, Card> callback = null;
            Func<Card, IEnumerator> awaitCallback = null;

            if (_drawIndex < _cards.Count)
            {
                card = _cards[_drawIndex];
                callback = onSuccessfulDeal;
                awaitCallback = awaitOnSuccessfulDeal;
                _drawIndex++;
            }

            callback(dealable, card);
            yield return awaitCallback.Invoke(card);
        }

        public IEnumerator DealRound(IEnumerable<Dealable> dealables, int cardsToDeal, Action<Dealable, Card> onSuccessfulDeal, Func<Card, IEnumerator> awaitOnSuccessfulDeal)
        {
            Card card = null;
            Action<Dealable, Card> callback = null;
            Func<Card, IEnumerator> awaitCallback = null;

            if (_drawIndex < _cards.Count)
            {
                card = _cards[_drawIndex];
                callback = onSuccessfulDeal;
                awaitCallback = awaitOnSuccessfulDeal;
                _drawIndex++;
            }

            callback(dealable, card);
            yield return awaitCallback.Invoke(card);
        }

        public IEnumerator DealFiveHundred(Player dealer, List<Dealable> dealables, Action<Dealable, Card> onSuccessfulDeal, Func<bool> continueDeal)
        {
            // Start dealing to the left of the dealer
            var n = dealables.IndexOf(dealer) + 1;
            Debug.Log($"Player {n} is dealing");

            // Get deck's card count since we can't alter the deck's contents while looping
            int count = Cards.Count;
            int drawCount = 1;

            onSuccessfulDeal += (Dealable dealable, Card card) => { count -= drawCount; };

            while (count > 0 && (continueDeal != null ? continueDeal() : true))
            {
                n++;
                n %= dealables.Count;

                drawCount = 2 + (((n / dealables.Count) + 1) % 2);

                // Debug.Log(count);
                yield return DealRound(dealables, drawCount, onSuccessfulDeal);
            }
        }
    }
}
