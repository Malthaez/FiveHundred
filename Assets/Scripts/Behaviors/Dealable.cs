using MatchingGame.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    public abstract class Dealable : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;

        public List<Card> Cards { get => _cards; protected set => _cards = value; }

        public void DiscardCards()
        {
            _cards.Clear();
        }

        public IEnumerator ArrangeCards(Vector3 startPosition)
        {
            var coroutines = new List<Coroutine>();

            for (int i = 0; i < _cards.Count; i++)
            {
                coroutines.Add(StartCoroutine(_cards[i].MoveTo(this.GetNextCardPosition(i, startPosition), 40f, null)));
            }

            yield return this.AwaitAllCoroutines(coroutines);
        }
    }
}
