using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Mediators;
using MatchingGame.Repositories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Managers
{
    [RequireComponent(typeof(FiveHundredGameMediator))]
    public class FiveHundredGameManager : MonoBehaviour
    {
        private FiveHundredGameMediator _fiveHundredGameMediator;
        private PlayingCardRepository _playingCardRepository;

        public void Initialize(List<Player> players)
        {
            _fiveHundredGameMediator = GetComponent<FiveHundredGameMediator>();

            _fiveHundredGameMediator.Initialize(players);
        }

        public bool Register(Deck deck) => _fiveHundredGameMediator.Register(deck);

        public bool Register(List<Player> players) => _fiveHundredGameMediator.Register(players);

        public IEnumerator FiveHundredGame(Deck deck)
        {
            deck.Shuffle();
            yield return _fiveHundredGameMediator.Deal((int)CardValuesEnum.Jack);
        }
    }
}