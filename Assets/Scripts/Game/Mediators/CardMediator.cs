using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Mediators
{
    public class CardMediator : MonoBehaviour
    {
        ScoreManager _scoreManager;

        public Transform _cardContainer;

        public List<Card> _cards;
        public List<Card> _selectedCards;

        public List<Card> Cards { get => _cards; set => _cards = value; }
        public List<Card> SelectedCards { get => _selectedCards; set => _selectedCards = value; }

        public void Initialize(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }

        public bool Register(Card card)
        {
            bool isRegistered = false;

            try
            {
                if (card == null) { return false; }
                Cards.Add(card);
                //card.OnFlipEnd = ResolveSelect;
                card.transform.SetParent(_cardContainer);
                isRegistered = true;
            }
            catch { }

            return isRegistered;
        }

        public bool UnRegister(Card card)
        {
            bool isRegistered = false;

            try
            {
                if (Cards.Contains(card)) { Cards.Remove(card); }
                //card.OnFlipEnd = null;
                isRegistered = true;
            }
            catch { }

            return isRegistered;
        }

        public void UnRegister(IEnumerable<Card> selectables) { foreach (var selectable in selectables) { UnRegister(selectable); } }

        public void Select(Card card)
        {
            if (!SelectedCards.Contains(card))
            {
                SelectedCards.Add(card);

                MatchValidation(SelectedCards);
            }
            else { }
        }

        public void Deselect(Card card)
        {
            if (SelectedCards.Contains(card))
            {
                SelectedCards.Remove(card);
            }
            else { }
        }

        public void ResolveSelect(Card card)
        {
            if (SelectedCards.Contains(card))
            {
                Deselect(card);
            }
            else
            {
                Select(card);
            }
        }

        public void MatchSuccess(IEnumerable<Card> cards)
        {
            UnRegister(SelectedCards);
            foreach (var card in cards) { card.gameObject.SetActive(false); }
            SelectedCards = new List<Card>();
        }

        public void MatchFailure(IEnumerable<Card> cards)
        {
            _scoreManager.AddStrike();
            StartFlipCards(cards);
        }

        public void MatchValidation(IEnumerable<Card> cards)
        {
            if (!Rules.ValidateCardCount(2, cards) || !Rules.CardSuitsAreSet(cards)) { return; }

            if (Rules.CardSuitsMatch(cards) && Rules.CardValuesMatch(cards))
            { MatchSuccess(cards); }
            else
            { MatchFailure(cards); }

            SelectedCards = new List<Card>();
        }

        public IEnumerator FlipCards(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                //card.OnFlipEnd = null;
                yield return card.Flip(card.FaceDirection == FaceDirection.Up ? FaceDirection.Down : FaceDirection.Up, 0.25f);
                //card.OnFlipEnd = ResolveSelect;
            }
        }

        public void StartFlipCards(IEnumerable<Card> cards) => StartCoroutine(FlipCards(cards));
    }
}
