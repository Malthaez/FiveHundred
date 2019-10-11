using MatchingGame.Behaviors;
using MatchingGame.Enums;
using MatchingGame.Mediators;
using MatchingGame.Repositories;
using MatchingGame.Services;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Managers
{
    [RequireComponent(typeof(CardMediator))]
    [RequireComponent(typeof(MemoryCardRepository))]
    [RequireComponent(typeof(PlayingCardRepository))]
    public class CardManager : MonoBehaviour
    {
        private CardMediator _cardMediator;
        private MemoryCardRepository _cardRepository;
        private PlayingCardRepository _playingCardRepository;

        public void Initialize(ScoreManager scoreManager)
        {
            _cardMediator = GetComponent<CardMediator>();
            _cardRepository = GetComponent<MemoryCardRepository>();
            _playingCardRepository = GetComponent<PlayingCardRepository>();

            _cardMediator.Initialize(scoreManager);
            _cardRepository.Initialize(CardServiceHost.CreateCardService());
            _playingCardRepository.Initialize(CardServiceHost.CreateCardService());
        }

        public bool Register(Card card) => _cardMediator.Register(card);

        public bool Register(IEnumerable<Card> cards)
        {
            bool isRegistered = true;

            foreach (var card in cards)
            {
                isRegistered = isRegistered && Register(card);
            }

            return isRegistered;
        }

        public MemoryCard CreateCard(CardValuesEnum cardValue) => _cardRepository.CreateCardPrefab(cardValue);

        public Card CreatePlayingCard(CardValuesEnum cardValue) => _playingCardRepository.CreatePlayingCardPrefab(cardValue);

        public List<Card> GetCardPairs(int cardPairsCount)
        {
            var cards = new List<Card>();

            for (int i = 0; i < cardPairsCount; i++)
            {
                var cardValue = (CardValuesEnum)(i + 1 - (((int)CardValuesEnum.MAX - 1) * (i / ((int)CardValuesEnum.MAX - 1))));
                //var cardValue = (CardValuesEnum)Random.Range(1, (int)CardValuesEnum.MAX);

                //cards.Add(CreateCard(cardValue));
                //cards.Add(CreateCard(cardValue));
                cards.Add(CreatePlayingCard(cardValue));
                cards.Add(CreatePlayingCard(cardValue));
            }

            return cards;
        }

        public void Shuffle(List<Card> cards)
        {
            int n = cards.Count;

            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }
    }
}
