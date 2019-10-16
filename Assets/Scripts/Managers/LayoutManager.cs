using MatchingGame.Behaviors.Layout;
using UnityEngine;

namespace MatchingGame.Managers
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
