using MatchingGame.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Mappers
{
    public static class CardMapper
    {
        public static List<Transform> ToTransforms(this List<Card> cards)
        {
            var cardTransforms = new List<Transform>();

            foreach (var card in cards) { cardTransforms.Add(card.transform); }

            return cardTransforms;
        }
    }
}
