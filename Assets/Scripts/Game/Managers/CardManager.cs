using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using Assets.Scripts.Game.Mediators;
using Assets.Scripts.Game.Repositories;
using Assets.Scripts.Game.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    [RequireComponent(typeof(CardMediator))]
    [RequireComponent(typeof(MemoryCardRepository))]
    [RequireComponent(typeof(PlayingCardRepository))]
    [RequireComponent(typeof(DeckRepository))]
    public class CardManager : MonoBehaviour
    {
        private CardMediator _cardMediator;
        private MemoryCardRepository _cardRepository;
        private PlayingCardRepository _playingCardRepository;
        private DeckRepository _deckRepository;

        public void Initialize(ScoreManager scoreManager)
        {
            _cardMediator = GetComponent<CardMediator>();
            _cardRepository = GetComponent<MemoryCardRepository>();
            _playingCardRepository = GetComponent<PlayingCardRepository>();
            _deckRepository = GetComponent<DeckRepository>();

            _cardMediator.Initialize(scoreManager);
            _cardRepository.Initialize(CardServiceHost.CreateCardService());
            _playingCardRepository.Initialize(CardServiceHost.CreateCardService());
            _deckRepository.Initialize();
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

        public CaptionCard CreateCard(CardSuitsEnum cardValue) => _cardRepository.CreateMemoryCardPrefab(cardValue);

        public Card CreatePlayingCard(CardSuitsEnum cardSuit, CardValuesEnum cardValue) => _playingCardRepository.CreatePlayingCardPrefab(cardSuit, cardValue);

        public Deck GetPlayingCardDeck()
        {
            var deck = _deckRepository.CreateDeckPrefab();

            var cards= new List<Card>();

            HashSet<CardSuitsEnum> suits = new HashSet<CardSuitsEnum>
            {
                CardSuitsEnum.Club,
                CardSuitsEnum.Spade,
                CardSuitsEnum.Diamond,
                CardSuitsEnum.Heart,
            };

            HashSet<CardValuesEnum> values = new HashSet<CardValuesEnum>
            {
                CardValuesEnum.Ace,
                CardValuesEnum.Two,
                CardValuesEnum.Three,
                CardValuesEnum.Four,
                CardValuesEnum.Five,
                CardValuesEnum.Six,
                CardValuesEnum.Seven,
                CardValuesEnum.Eight,
                CardValuesEnum.Nine,
                CardValuesEnum.Ten,
                CardValuesEnum.Jack,
                CardValuesEnum.Queen,
                CardValuesEnum.King
            };

            foreach (var suit in suits)
            {
                foreach (var value in values)
                {
                    deck.AddCard(CreatePlayingCard(suit, value));
                }
            }

            deck.AddCard(CreatePlayingCard(CardSuitsEnum.Joker, CardValuesEnum.Joker));
            deck.AddCard(CreatePlayingCard(CardSuitsEnum.Joker, CardValuesEnum.Joker));

            return deck;
        }
    }
}
