using Assets.Scripts.Game.Extensions;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Behaviors
{
    /// <summary>
    /// Something that can be dealt cards
    /// </summary>
    public class Dealable : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards = new List<Card>();
        [SerializeField] private bool _revealCards = false;

        public List<Card> Cards { get => _cards; protected set => _cards = value; }

        public void DiscardCards() => _cards.Clear();

        public IEnumerator ReceiveCard(Card card)
        {
            _cards.Add(card);

            // yield return this.AwaitAllCoroutines(card.DoCardStuff(this.GetLastCardPosition(), transform.rotation, 45.0f, _revealCards));
            yield return this.AwaitAllCoroutines(card.DoCardStuff(this.GetCardPosition(_cards.Count - 1), transform.rotation, 45.0f, _revealCards));

            card.transform.SetParent(transform);
        }

        public IEnumerator ArrangeCards()
        {
            var coroutines = new List<Coroutine>();

            for (int i = 0; i < _cards.Count; i++)
            {
                // coroutines.AddRange(_cards[i].DoCardStuff(this.GetCardPositionByIndex(i), transform.rotation, 45.0f, _revealCards));
                coroutines.AddRange(_cards[i].DoCardStuff(this.GetCardPosition(i), transform.rotation, 45.0f, _revealCards));
            }

            yield return this.AwaitAllCoroutines(coroutines);
        }
    }
}
