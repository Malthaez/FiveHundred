using Assets.Scripts.Game.Behaviors;
using UnityEngine;

namespace Assets.Scripts.Game.Repositories
{
    public class DeckRepository : MonoBehaviour
    {
        [SerializeField] private Deck _deckPrefab;

        public void Initialize() { }

        public Deck CreateDeckPrefab() => Instantiate(_deckPrefab);
    }
}
