using Assets.Scripts.UI.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Mediators
{
    public class MenuMediator : MonoBehaviour
    {
        [SerializeField] private Stack<Menu> _menuStack;

        public void Initialize() => _menuStack = new Stack<Menu>();

        public void RegisterMenus(Stack<Menu> menuStack) => _menuStack = menuStack;

        public void ClearMenuStack() => _menuStack.Clear();

        public Menu CloseMenu() => _menuStack.Pop();

        public void OpenMenu(Menu menu) => _menuStack.Push(menu);
    }
}
