using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Enums;
using Assets.Scripts.Game.FiveHundredGame;
using Assets.Scripts.UI.Behaviors;
using Assets.Scripts.UI.Managers;
using System.Collections.Generic;
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

        // TODO: Finish GetButtons for buttons that are built from strings with void Actions (or possibly do another generic)
        public List<Menu> CreateBidsMenu(Player player)
        {
            List<Menu> bidsMenus = new List<Menu>();

            var suitsButtons = new List<Button>();
            var valuesButtons = new List<Button>();

            suitsButtons = _menuManager.GetButtons(FiveHundredGameRules.FiveHundredSuits, (CardSuitsEnum suit) => player.bid.Suit = suit);
            suitsButtons.Add(_menuManager.GetButton("Ace No Face"));
            valuesButtons = _menuManager.GetButtons(FiveHundredGameRules.FiveHundredTricks, (BidsEnum value) => player.bid.Value = value);
            valuesButtons.Add(_menuManager.GetButton("Back"));

            bidsMenus.Add(_menuManager.GetMenu("Suit", suitsButtons));
            bidsMenus.Add(_menuManager.GetMenu("Tricks", valuesButtons));

            return bidsMenus;
        }
    }
}
