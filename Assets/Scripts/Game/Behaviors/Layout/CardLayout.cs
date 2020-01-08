using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Behaviors.Layout
{
    public abstract class CardLayout
    {
        [SerializeField] protected List<Transform> _layoutElements;

        public CardLayout(List<Transform> layoutElements) => _layoutElements = layoutElements;

        public abstract void Refresh();
    }
}
