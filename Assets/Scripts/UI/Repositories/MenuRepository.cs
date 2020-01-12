using Assets.Scripts.UI.Behaviors;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Repositories
{
    public class MenuRepository : MonoBehaviour
    {
        /// <summary>
        /// Default parent for new Menus
        /// </summary>
        [SerializeField] private Canvas _canvas;

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
            if (_canvas == null || _buttonPrefab == null || _menuPrefab == null)
            {
                Debug.LogWarning($"Not all prefabs are set in {GetType().ToString()}");
            }
        }

        public Button CreateButton(string label)
        {
            var button = Instantiate(_buttonPrefab);
            var buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = label;
            return button;
        }

        public Menu CreateMenu(string title)
        {
            var menu = Instantiate(_menuPrefab);
            menu.transform.parent = _canvas.transform;
            menu.transform.localPosition = Vector3.zero;
            menu.Title = title;
            menu.Hide();
            return menu;
        }

        public Coroutine GetMenuCoroutine(IEnumerator menuCoroutine) => StartCoroutine(menuCoroutine);
    }
}
