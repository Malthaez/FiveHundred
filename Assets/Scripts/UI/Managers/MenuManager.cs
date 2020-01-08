using Assets.Scripts.UI.Behaviors;
using Assets.Scripts.UI.Repositories;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MenuRepository _menuRepository;

        public void Initialize(MenuRepository menuRepository)
        {
            _menuRepository = menuRepository;

            _menuRepository.Initialize();
        }

        public Button GetButton<T>(T value, IEnumerable<Action<T>> actions) where T : Enum
        {
            var button = _menuRepository.CreateButton(value.ToString());

            foreach (var action in actions)
            {
                button.onClick.AddListener(() => action(value));
            }

            return button;
        }

        public Button GetButton<T>(T value, Action<T> action) where T : Enum => GetButton(value, new List<Action<T>>() { action });

        public List<Button> GetButtons<T>(IEnumerable<T> values, IEnumerable<Action<T>> actions) where T : Enum
        {
            var buttons = new List<Button>();

            foreach (var value in values)
            {
                buttons.Add(GetButton(value, actions));
            }

            return buttons;
        }

        public List<Button> GetButtons<T>(IEnumerable<T> values, Action<T> action) where T : Enum => GetButtons(values, new List<Action<T>>() { action });

        public Menu GetMenu(string title, IEnumerable<Button> buttons) => _menuRepository.CreateMenu(title, buttons);
    }
}
