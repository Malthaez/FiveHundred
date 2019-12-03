using MatchingGame.Enums;
using System;
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
        [SerializeField] private Image _cardBack;

        public CardSuitsEnum Suit { get => _cardSuit; set => _cardSuit = value; }
        public int Value { get => _value; set => _value = value; }
        public Sprite Art { get => _cardArt.sprite; set => _cardArt.sprite = value; }
        public Sprite Back { get => _cardBack.sprite; set => _cardBack.sprite = value; }

        //========

        protected bool _flipping = false;
        protected bool _flipped = false;
        protected bool _moving = false;
        private Coroutine _flipCoroutine;

        public bool Flipping => _flipping;
        public bool Flipped => _flipped;

        public bool Moving { get => _moving; set => _moving = value; }
        public Coroutine FlipCoroutine { get => _flipCoroutine; set => _flipCoroutine = value; }

        public delegate void OnFlip(Card card);
        public OnFlip OnFlipStart { get; set; }
        public OnFlip OnEachFlipFrame { get; set; }
        public OnFlip OnFlipEnd { get; set; }

        private void GetMouseInput()
        {
            /* Mouse Left */
            if (Input.GetMouseButtonUp(0)) { if (!_flipping && !_flipped) { FlipUp(); } }
            /* Mouse Right */
            if (Input.GetMouseButtonUp(1)) { if (!_flipping && _flipped) { FlipDown(); } }
            /* Mouse Middle */
            if (Input.GetMouseButtonUp(2)) { }
        }

        private void OnMouseOver() => GetMouseInput();

        public Coroutine FlipDown(float duration, float pause)
        {
            if (_flipCoroutine != null) { StopCoroutine(_flipCoroutine); }
            _flipCoroutine = StartCoroutine(IFlip(-180f, duration, pause));
            return _flipCoroutine;
        }

        public Coroutine FlipDown(float duration) => FlipDown(duration, 0.1f);

        public Coroutine FlipDown() => FlipDown(0.1f);

        public Coroutine FlipUp(float duration, float pause)
        {
            if (_flipCoroutine != null) { StopCoroutine(_flipCoroutine); }
            _flipCoroutine = StartCoroutine(IFlip(180f, duration, pause));
            return _flipCoroutine;
        }

        public Coroutine FlipUp(float duration) => FlipUp(duration, 0.1f);

        public Coroutine FlipUp() => FlipUp(0.1f);

        private IEnumerator IFlip(float rotation, float duration, float pause)
        {
            OnFlipStart?.Invoke(this);

            if (rotation != 0)
            {
                var totalDuration = duration * Math.Abs(rotation / 180f);
                var t = 0f;
                _flipping = true;

                while (true)
                {
                    OnEachFlipFrame?.Invoke(this);

                    float incrementalRotation = (rotation / totalDuration) * Time.deltaTime;

                    if (Math.Abs(t + incrementalRotation) > Math.Abs(rotation))
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
            }

            OnFlipEnd?.Invoke(this);
        }

        public IEnumerator MoveTo(Vector3 destinationPosition, float speed, Action onMoveEnd)
        {
            var startTime = Time.time;
            var startPosition = transform.position;
            var endPosition = destinationPosition;
            var totalDistance = Vector3.Distance(startPosition, endPosition);
            var t = 0f;

            _moving = true;

            if (totalDistance != 0)
            {
                while (t < 1.0f)
                {
                    var elapsedDistance = (Time.time - startTime) * speed;
                    t = Mathf.Clamp(elapsedDistance / totalDistance, 0f, 1.0f);

                    transform.position = Vector3.Lerp(startPosition, endPosition, t);

                    yield return null;
                }
            }

            _moving = false;

            onMoveEnd?.Invoke();
            // yield return awaitOnMoveEnd(this);
        }
    }
}
