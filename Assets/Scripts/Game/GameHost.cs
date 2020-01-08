using Assets.Scripts.Game.Managers;
using UnityEngine;

namespace Assets.Scripts.Game
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
