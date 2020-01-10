using Assets.Scripts.UI.Behaviors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Repositories
{
    public class MenuRepository : MonoBehaviour
    {
        /// <summary>
        /// Must be set in Unity Inspector
        /// </summary>
        [SerializeField] private Button _buttonPrefab;

        /// <summary>
        /// Must be set in Unity Inspector
        /// </summary>
        [SerializeField] private Menu _menuPrefab;

        public void Initialize()
        {
            if (_buttonPrefab == null || _menuPrefab == null)
            {
                Debug.LogWarning($"Not all prefabs are set in {GetType().ToString()}");
            }
        }

        public Button CreateButton(string name)
        {
            var button = Instantiate(_buttonPrefab);
            var buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = name;
            return button;
        }

        public Menu CreateMenu(string title, IEnumerable<Button> buttons)
        {
            var menu = Instantiate(_menuPrefab);
            menu.Title = title;
            menu.Buttons = buttons;
            return menu;
        }
    }
}
