using MatchingGame.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors
{
    /// <summary>
    /// Something that can be dealt cards
    /// </summary>
    public class Dealable : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards = new List<Card>();

        public List<Card> Cards { get => _cards; protected set => _cards = value; }

        public void DiscardCards() => _cards.Clear();

        public IEnumerator ReceiveCard(Card card)
        {
            var coroutines = new List<Coroutine>();
            var cardMoveSpeed = 45.0f;

            _cards.Add(card);

            var lastCardPosition = this.GetLastCardPosition();
            var duration = Vector3.Distance(card.transform.position, lastCardPosition) / cardMoveSpeed;

            coroutines.Add(StartCoroutine(card.MoveTo(lastCardPosition, cardMoveSpeed)));
            coroutines.Add(StartCoroutine(card.RotateTo(transform.rotation.eulerAngles.z, duration)));

            yield return this.AwaitAllCoroutines(coroutines);

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
