using Assets.Scripts.UI.Behaviors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Repositories
{
    public class MenuRepository : MonoBehaviour
    {
        [SerializeField] private Button _buttonPrefab; // Set in Unity Inspector
        [SerializeField] private Menu _menuPrefab; // Must be set in Unity Inspector

        void Awake()
        {
            if(_buttonPrefab == null || _menuPrefab == null)
            {
                Debug.LogWarning($"Not all prefabs are set in {GetType().ToString()}");
            }
        }

        public void Initialize() { }

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

            menu.SetTitleAndButtons(title, buttons);

            return menu;
        }
    }
}
