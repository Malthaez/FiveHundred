using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Behaviors
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected Text _titleText;
        [SerializeField] protected GameObject _buttonsPanel;
        [SerializeField] protected List<Button> _buttons;

        public string Title { get => _titleText.text; set => _titleText.text = value; }

        public IEnumerable<Button> Buttons
        {
            get => _buttons;
            set
            {
                _buttons = new List<Button>(value);
                foreach (var button in _buttons) { button.transform.SetParent(_buttonsPanel.transform); }
            }
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
