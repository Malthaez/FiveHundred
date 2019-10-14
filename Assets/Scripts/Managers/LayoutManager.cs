using Assets.Scripts.Behaviors.Layout;
using UnityEngine;

namespace MatchingGame.Managers
{
    public class LayoutManager : MonoBehaviour
    {
        private Layout _layout;

        public void Initialize() { }

        public void SetLayout(Layout layout)
        {
            _layout = layout;

            layout.Refresh();
        }
    }
}
