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
        [SerializeField] private float _dealSpeed = 60.0f;

        public CardSuitsEnum Suit { get => _cardSuit; set => _cardSuit = value; }
        public int Value { get => _value; set => _value = value; }
        public Sprite Art { get => _cardArt.sprite; set => _cardArt.sprite = value; }

        //========

        protected bool _flipping = false;
        protected bool _flipped = false;
        protected bool _moving = false;

        public bool Flipping => _flipping;

        public bool Moving { get => _moving; set => _moving = value; }

        public delegate void OnFlip(Card card);
        public OnFlip OnFlipStart { get; set; }
        public OnFlip OnEachFlipFrame { get; set; }
        public OnFlip OnFlipEnd { get; set; }

        public delegate void OnDraw(Card card);
        public OnDraw OnDrawStart { get; set; }
        public OnDraw OnEachDrawFrame { get; set; }
        public OnDraw OnDrawEnd { get; set; }

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

        public IEnumerator Draw(Vector3 destinationPosition)
        {
            OnDrawStart?.Invoke(this);

            _moving = true;

            var startTime = Time.time;
            var startPosition = transform.position;
            var endPosition = destinationPosition;
            var totalDistance = Vector3.Distance(startPosition, endPosition);

            while (true)
            {
                OnEachDrawFrame?.Invoke(this);

                var elapsedDistance = (Time.time - startTime) * _dealSpeed;
                var t = Mathf.Clamp(elapsedDistance / totalDistance, 0f, 1.0f);

                transform.position = Vector3.Lerp(startPosition, endPosition, t);

                if (t >= 1.0f) { break; }

                yield return null;
            }

            _moving = false;

            OnDrawEnd?.Invoke(this);
        }
    }
}
