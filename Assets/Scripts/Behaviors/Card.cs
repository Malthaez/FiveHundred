using MatchingGame.Enums;
using MatchingGame.Factories;
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

        [SerializeField] protected FaceDirection _faceDirection = FaceDirection.Down;
        protected bool _flipping = false;
        protected bool _moving = false;
        private Coroutine _flipCoroutine;

        public bool Flipping => _flipping;
        public FaceDirection FaceDirection => _faceDirection;

        public bool Moving { get => _moving; set => _moving = value; }

        private void GetMouseInput()
        {
            /* Mouse Left */
            if (Input.GetMouseButtonUp(0)) { if (!_flipping) { StartCoroutine(Flip(FaceDirection.Up, 0.1f)); } }
            /* Mouse Right */
            if (Input.GetMouseButtonUp(1)) { if (!_flipping) { StartCoroutine(Flip(FaceDirection.Down, 0.1f)); } }
            /* Mouse Middle */
            if (Input.GetMouseButtonUp(2)) { }
        }

        private void OnMouseOver() => GetMouseInput();

        //public Coroutine FlipDown(float duration)
        //{
        //    if(_flipping && _flipCoroutine != null)
        //    {
        //        StopCoroutine(_flipCoroutine);
        //        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, FaceDirectionFactory.GetFaceDirectionRotation(FaceDirection.Up).z);

        //        _faceDirection = FaceDirection.Up;
        //        _flipping = false;
        //        _flipCoroutine = null;
        //    }
        //    _flipCoroutine = StartCoroutine(IFlip(FaceDirection.Down, duration));
        //    return _flipCoroutine;
        //}

        //public Coroutine FlipDown() => FlipDown(0.1f);

        //public Coroutine FlipUp(float duration)
        //{
        //    if (_flipping && _flipCoroutine != null)
        //    {
        //        StopCoroutine(_flipCoroutine);
        //        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, FaceDirectionFactory.GetFaceDirectionRotation(FaceDirection.Down).z);

        //        _faceDirection = FaceDirection.Down;
        //        _flipping = false;
        //        _flipCoroutine = null;
        //    }
        //    _flipCoroutine = StartCoroutine(IFlip(FaceDirection.Up, duration));
        //    return _flipCoroutine;
        //}

        //public Coroutine FlipUp() => FlipUp(0.1f);

        public IEnumerator Flip(FaceDirection flipDirection, float duration)
        {
            if (_faceDirection == flipDirection) { yield break; }

            var rotation = flipDirection == FaceDirection.Up ? 180f : -180f;
            var totalDuration = duration * Math.Abs(rotation / 180f);
            var t = 0f;
            _flipping = true;
            while (_flipping)
            {
                var incrementalRotation = (rotation / totalDuration) * Time.deltaTime;

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
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, FaceDirectionFactory.GetFaceDirectionRotation(flipDirection).y, transform.eulerAngles.z);

            _faceDirection = flipDirection;
            _flipping = false;
        }

        public IEnumerator MoveTo(Vector3 destinationPosition, float speed)
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
        }
    }
}
