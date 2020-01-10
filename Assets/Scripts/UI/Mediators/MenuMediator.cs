using Assets.Scripts.UI.Behaviors;
using System.Collections.Generic;

namespace Assets.Scripts.UI.Mediators
{
    public class MenuMediator
    {
        private Stack<Menu> _menuStack;

        public MenuMediator()
        {
            _menuStack = new Stack<Menu>();
        }

        public void RegisterMenus(IEnumerable<Menu> menuStack) => _menuStack = new Stack<Menu>(menuStack);

        public void ClearMenuStack() => _menuStack.Clear();

        public void CloseMenu()
        {
            var menu = _menuStack.Pop();
            menu.gameObject.SetActive(false);
            if (_menuStack.Count > 0) { _menuStack.Peek().gameObject.SetActive(true); }
        }

        public void OpenMenu(Menu menu)
        {
            if (_menuStack.Count > 0) { _menuStack.Peek().gameObject.SetActive(false); }
            _menuStack.Push(menu);
            menu.gameObject.SetActive(true);
        }
    }
}
