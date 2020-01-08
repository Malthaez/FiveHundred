using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Behaviors
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected Text _titleText;
        [SerializeField] protected GameObject _buttonsPanel;
        [SerializeField] protected List<Button> _buttons;

        public void SetTitleAndButtons(string title, IEnumerable<Button> buttons)
        {
            _titleText.text = title;

            _buttons = buttons.ToList();

            foreach(var button in _buttons)
            {
                button.transform.SetParent(_buttonsPanel.transform);
            }
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
