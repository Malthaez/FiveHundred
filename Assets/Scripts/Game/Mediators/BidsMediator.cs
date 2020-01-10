using Assets.Scripts.Game.Behaviors;
using Assets.Scripts.Game.Managers;
using Assets.Scripts.UI.Mediators;
using UnityEngine;

namespace Assets.Scripts.Game.Mediators
{
    public class BidsMediator
    {
        private readonly BidsManager _bidsManager;
        private readonly MenuMediator _menuMediator;

        public BidsMediator(BidsManager bidsManager, MenuMediator menuMediator)
        {
            _bidsManager = bidsManager;
            _menuMediator = menuMediator;
        }

        public void Test(Player player, GameObject canvas)
        {
            var menus = _bidsManager.CreateBidsMenu(player);
            foreach (var menu in menus) { menu.gameObject.transform.parent = canvas.transform; }

            _menuMediator.OpenMenu(menus[0]);
        }
    }
}
