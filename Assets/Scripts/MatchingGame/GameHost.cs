using MatchingGame.Managers;
using UnityEngine;

namespace MatchingGame
{
    [RequireComponent(typeof(GameManager))]
    public class GameHost : MonoBehaviour
    {
        public GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();

            _gameManager.Initialize();
        }
    }
}
