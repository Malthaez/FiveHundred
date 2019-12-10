using MatchingGame.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public class Dealable : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards = new List<Card>();

        public List<Card> Cards { get => _cards; protected set => _cards = value; }

        public void DiscardCards() => _cards.Clear();

        public IEnumerator ReceiveCard(Card card)
        {
            _cards.Add(card);
            yield return card.MoveTo(this.GetLastCardPosition(), 45.0f);
            card.transform.SetParent(transform);
        }

        public IEnumerator ArrangeCards(Vector3 startPosition)
        {
            var coroutines = new List<Coroutine>();

            for (int i = 0; i < _cards.Count; i++)
            {
                coroutines.Add(StartCoroutine(_cards[i].MoveTo(this.GetCardPositionByIndex(i), 40f)));
            }

            yield return this.AwaitAllCoroutines(coroutines);
        }
    }
}
