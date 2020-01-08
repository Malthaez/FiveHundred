using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Behaviors
{
    public class Scoreboard : MonoBehaviour
    {
        public int _score;
        public int _strikes = 0;

        public int Strikes { get => _strikes; set => _strikes = value; }

        public Text _scoreText;
        public List<Image> _strikeImages;

        public void AddPoints(int points)
        {
            _score += points;
            _scoreText.text = $"Score: {_score}";
        }

        public void AddStrike()
        {
            if(_strikes++ >= _strikeImages.Count) { return; }
            _strikeImages[_strikes - 1].gameObject.SetActive(true);
        }
    }
}
