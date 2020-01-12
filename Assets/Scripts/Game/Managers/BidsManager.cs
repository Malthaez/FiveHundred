using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using Assets.Scripts.Game.FiveHundredGame;
using Assets.Scripts.UI.Behaviors;
using Assets.Scripts.UI.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Managers
{
    public class BidsManager
    {
        private readonly MenuManager _menuManager;

        public BidsManager(MenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        private Menu CreateTricksMenu(Player player)
        {
            var tricksMenu = _menuManager.GetMenu("Tricks");
            var tricksButtonActions = new List<Action<BidsEnum>>
            {
                (BidsEnum tricks) => player.bid.Value = tricks,
                (BidsEnum _) => _menuManager.CloseAllMenus(),
                (BidsEnum _) => Debug.Log($"Player Bids: {player.bid.Value} {player.bid.Suit}")
            };
            var tricksButtons = new List<Button>();
            tricksButtons = _menuManager.GetButtons(FiveHundredGameRules.FiveHundredTricks, tricksButtonActions);
            tricksButtons.Add(_menuManager.GetButton("Back", (string _) => _menuManager.CloseMenu()));
            tricksMenu.Buttons = tricksButtons;

            return tricksMenu;
        }

        private Menu CreateSuitsMenu(Player player, Menu tricksMenu)
        {
            var suitsMenu = _menuManager.GetMenu("Suit");

            var suitsButtonActions = new List<Action<CardSuitsEnum>>
            {
                (CardSuitsEnum suit) => player.bid.Suit = suit,
                (CardSuitsEnum _) => _menuManager.OpenMenu(tricksMenu)
            };
            var suitsButtons = new List<Button>();
            suitsButtons = _menuManager.GetButtons(FiveHundredGameRules.FiveHundredSuits, suitsButtonActions);
            suitsButtons.Add(_menuManager.GetButton("No Ace/No Face", (string _) => _menuManager.CloseAllMenus()));
            suitsMenu.Buttons = suitsButtons;

            return suitsMenu;
        }

        public Menu CreateMenu(Player player)
        {
            var tricksMenu = CreateTricksMenu(player);
            var suitsMenu = CreateSuitsMenu(player, tricksMenu);
            return suitsMenu;
        }
    }
}
