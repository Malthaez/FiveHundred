using Assets.Scripts.Game.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.UI
{
    public class BidsMenu : MonoBehaviour
    {
        private BidsEnum? _bidCount = null;
        private CardSuitsEnum? _bidSuit = null;

        // Menu Objects

        [SerializeField] private GameObject _bidsMenu;

        [SerializeField] private GameObject _bidCountButtonContainer;
        [SerializeField] private GameObject _bidSuitButtonContainer;

        // Bid Count Buttons

        [SerializeField] private List<Button> _bidCountButtons;

        //[SerializeField] private Button _aceNoFaceButton;
        //[SerializeField] private Button _inkleButton;
        //[SerializeField] private Button _sevenButton;
        //[SerializeField] private Button _eightButton;
        //[SerializeField] private Button _nineButton;
        //[SerializeField] private Button _tenButton;

        // Bid Suit Buttons

        [SerializeField] private List<Button> _bidSuitButtons;

        //[SerializeField] private Button _spadesButton;
        //[SerializeField] private Button _clubsButton;
        //[SerializeField] private Button _diamondsButton;
        //[SerializeField] private Button _heartsButton;
        //[SerializeField] private Button _noTrumpButton;

        // Other Buttons

        [SerializeField] private Button _backButton;

        private void Awake()
        {
            // Bid Count Buttons

            _bidCountButtons = _bidCountButtonContainer.GetComponentsInChildren<Button>().ToList();

            //_aceNoFaceButton.onClick.AddListener(() => ShowBidSuitButtons(BidsEnum.AceNoFace));
            //_inkleButton.onClick.AddListener(() => ShowBidSuitButtons(BidsEnum.Inkle));
            //_sevenButton.onClick.AddListener(() => ShowBidSuitButtons(BidsEnum.Seven));
            //_eightButton.onClick.AddListener(() => ShowBidSuitButtons(BidsEnum.Eight));
            //_nineButton.onClick.AddListener(() => ShowBidSuitButtons(BidsEnum.Nine));
            //_tenButton.onClick.AddListener(() => ShowBidSuitButtons(BidsEnum.Ten));

            // Bid Suit Buttons

            _bidSuitButtons = _bidSuitButtonContainer.GetComponentsInChildren<Button>().ToList();

            //_spadesButton.onClick.AddListener(() => ReturnBid(CardSuitsEnum.Spade));
            //_clubsButton.onClick.AddListener(() => ReturnBid(CardSuitsEnum.Club));
            //_diamondsButton.onClick.AddListener(() => ReturnBid(CardSuitsEnum.Diamond));
            //_heartsButton.onClick.AddListener(() => ReturnBid(CardSuitsEnum.Heart));
            //_noTrumpButton.onClick.AddListener(() => ReturnBid(CardSuitsEnum.NoTrump));

            // Other Buttons

            _backButton.onClick.AddListener(ShowBidSuitButtons);
        }

        private void ShowBidSuitButtons(BidsEnum? bidCount)
        {
            _bidCount = bidCount;

            _bidSuitButtonContainer.SetActive(true);
            _bidCountButtonContainer.SetActive(false);
        }

        private void ShowBidSuitButtons() => ShowBidSuitButtons(null);

        private void ShowBidCountButtons(CardSuitsEnum? bidSuit)
        {
            _bidSuit = bidSuit;

            _bidCountButtonContainer.SetActive(true);
            _bidSuitButtonContainer.SetActive(false);
        }

        private void ShowBidCountButtons() => ShowBidCountButtons(null);

        private void ReturnBid(CardSuitsEnum bidSuit)
        {
            _bidSuit = bidSuit;

            Debug.Log($"Bid is: {_bidCount.ToString()} {_bidSuit.ToString()}s");

            _bidsMenu.SetActive(false);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
