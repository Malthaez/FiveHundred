using UnityEngine.UI;

namespace MatchingGame.Behaviors
{
    public class CaptionCard : Card
    {
        public Text _cardText;

        public string CardText { get => _cardText.text; set => _cardText.text = value; }
    }
}
