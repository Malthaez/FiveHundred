using MatchingGame.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingGame.Behaviors
{
    [RequireComponent(typeof(Collider))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardSuitsEnum _cardSuit;
        [SerializeField] private int _value;
        [SerializeField] private Image _cardArt;

        public CardSuitsEnum Suit { get => _cardSuit; set => _cardSuit = value; }
        public int Value { get => _value; set => _value = value; }
        public Sprite Art { get => _cardArt.sprite; set => _cardArt.sprite = value; }

        //========

        protected bool _flipping = false;
        protected bool _flipped = false;

        public bool Flipping => _flipping;

        public delegate void OnFlip(Card selectable);
        public OnFlip OnFlipStart { get; set; }
        public OnFlip OnEachFlipFrame { get; set; }
        public OnFlip OnFlipEnd { get; set; }

        private void GetMouseInput()
        {
            /* Mouse Left */
            if (Input.GetMouseButtonUp(0)) { if (!_flipping && !_flipped) { StartCoroutine(Flip(180f, 0.25f, 0.25f)); } }
            /* Mouse Right */
            if (Input.GetMouseButtonUp(1)) { if (!_flipping && _flipped) { StartCoroutine(Flip(180f, 0.25f, 0.25f)); } }
            /* Mouse Middle */
            if (Input.GetMouseButtonUp(2)) { }
        }

        private void OnMouseOver() => GetMouseInput();

        public IEnumerator Flip(float rotation, float duration, float pause)
        {
            OnFlipStart?.Invoke(this);

            _flipping = true;
            float t = 0f;

            while (true)
            {
                OnEachFlipFrame?.Invoke(this);

                float incrementalRotation = (rotation / duration) * Time.deltaTime;

                if (t + incrementalRotation > rotation)
                {
                    transform.Rotate(new Vector3 { z = rotation - t });
                    break;
                }
                else
                {
                    t += incrementalRotation;
                    transform.Rotate(new Vector3 { z = incrementalRotation });
                    yield return null;
                }
            }

            yield return new WaitForSeconds(pause);

            _flipped = !_flipped;
            _flipping = false;

            OnFlipEnd?.Invoke(this);
        }
    }
}
