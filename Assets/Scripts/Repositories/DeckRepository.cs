using MatchingGame.Behaviors;
using UnityEngine;

namespace MatchingGame.Repositories
{
    public class DeckRepository : MonoBehaviour
    {
        [SerializeField] private Deck _deckPrefab;

        public void Initialize() { }

        public Deck CreateDeckPrefab() => Instantiate(_deckPrefab);
    }
}
