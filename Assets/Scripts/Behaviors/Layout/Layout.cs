using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Layout
{
    public abstract class Layout
    {
        [SerializeField] protected List<Transform> _layoutElements;

        public Layout(List<Transform> layoutElements) => _layoutElements = layoutElements;

        public abstract void Refresh();
    }
}
