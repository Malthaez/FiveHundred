using UnityEngine.UI;

namespace Assets.Scripts.Game.Behaviors
{
    public class CaptionCard : Card
    {
        public Text _cardText;

        public string CardText { get => _cardText.text; set => _cardText.text = value; }
    }
}
