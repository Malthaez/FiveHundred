using MatchingGame.Managers;
using UnityEngine;

namespace MatchingGame
{
    [RequireComponent(typeof(GameManager))]
    public class GameHost : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();

            _gameManager.Initialize();
        }
    }
}
