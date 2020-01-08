using Assets.Scripts.Game.Behaviors.Layout;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    public class LayoutManager : MonoBehaviour
    {
        private CardLayout _layout;

        public void Initialize() { }

        public void SetLayout(CardLayout layout)
        {
            _layout = layout;

            layout.Refresh();
        }
    }
}
