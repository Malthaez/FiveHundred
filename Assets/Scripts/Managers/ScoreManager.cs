using MatchingGame.Behaviors;
using UnityEngine;

namespace MatchingGame.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public Scoreboard _scoreboard;

        private int _pairsRemaining;

        public void Initialize(int matchingPairs)
        {
            _pairsRemaining = matchingPairs;
        }

        public void AddStrike()
        {
            _scoreboard.AddStrike();
            if (CheckForLoss()) { Debug.Log("You Lose!"); }
        }

        public bool CheckForWin() => _scoreboard._score >= _pairsRemaining;

        public bool CheckForLoss() => _scoreboard.Strikes >= _scoreboard._strikeImages.Count;
    }
}
