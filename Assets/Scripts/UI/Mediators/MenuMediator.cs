using Assets.Scripts.UI.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void ClearMenuStack() => _menuStack.Clear();

        public void CloseAllMenus()
        {
            foreach (var menu in _menuStack)
            {
                menu.Hide();
            }
            ClearMenuStack();
        }

        public void CloseMenu()
        {
            var menu = _menuStack.Pop();
            menu.Hide();
            if (_menuStack.Count > 0) { _menuStack.Peek().Show(); }
        }

        public void OpenMenu(Menu menu)
        {
            if (_menuStack.Count > 0) { _menuStack.Peek().Hide(); }
            _menuStack.Push(menu);
            menu.Show();
        }

        public IEnumerator AwaitMenu(Menu menu)
        {
            OpenMenu(menu);

            while (_menuStack.Count > 0)
            {
                yield return null;
            }

            Debug.Log("Menu Complete");
        }
    }
}
