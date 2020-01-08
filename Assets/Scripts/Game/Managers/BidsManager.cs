using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using Assets.Scripts.Game.UI;
using Assets.Scripts.UI.Behaviors;
using Assets.Scripts.UI.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Managers
{
    public class BidsManager : MonoBehaviour
    {
        [SerializeField] private MenuManager _menuManager;

        private readonly HashSet<CardSuitsEnum> _fiveHundredSuits = new HashSet<CardSuitsEnum>
            {
                CardSuitsEnum.Club,
                CardSuitsEnum.Spade,
                CardSuitsEnum.Diamond,
                CardSuitsEnum.Heart,
                CardSuitsEnum.NoTrump
            };

        private readonly HashSet<BidsEnum> _fiveHundredTricks = new HashSet<BidsEnum>
            {
                BidsEnum.Inkle,
                BidsEnum.Seven,
                BidsEnum.Eight,
                BidsEnum.Nine,
                BidsEnum.Ten
            };

        public void Initialize(MenuManager menuManager)
        {
            _menuManager = menuManager;
        }


        // TODO: Finish GetButtons for buttons that are built from strings with void Actions (or possibly do another generic)
        public Menu CreateBidsMenu(Player player)
        {
            Menu bidsMenu = null;

            Button aceNoFaceButton = null;
            var suitsButtons = new List<Button>();
            var valuesButtons = new List<Button>();
            Button backButton = null;


            suitsButtons = _menuManager.GetButtons(_fiveHundredSuits, (CardSuitsEnum suit) => player.bid.Suit = suit);
            valuesButtons = _menuManager.GetButtons(_fiveHundredTricks, (BidsEnum value) => player.bid.Value = value);

            // bidsMenu = _menuRepository.CreateMenu("Suit", suitsButtons);
            bidsMenu = _menuManager.GetMenu("Tricks", valuesButtons);

            return bidsMenu;
        }

        public void PlaceBid(CardSuitsEnum? bidSuit, BidsEnum? bidCount)
        {

        }
    }
}
