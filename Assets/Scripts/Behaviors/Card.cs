using MatchingGame.Enums;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingGame.Behaviors
{
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

        protected bool _moving = false;
        protected bool _rotating = false;

        public FaceDirection FaceDirection => GetComponentInChildren<CardAvatar>().FaceDirection;
        public bool Flipping => GetComponentInChildren<CardAvatar>().Flipping;
        public bool Moving { get => _moving; set => _moving = value; }
        public bool Rotating => _rotating;

        public IEnumerator Flip(FaceDirection flipDirection, float duration) => GetComponentInChildren<CardAvatar>().Flip(flipDirection, duration);

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

        public IEnumerator RotateTo(float finalRotation, float duration)
        {
            var initialRotation = transform.rotation.eulerAngles.z;

            if (initialRotation == finalRotation) { yield break; }

            var rotation = finalRotation - initialRotation;
            var t = 0f;

            _rotating = true;

            while (_rotating)
            {
                var incrementalRotation = (rotation / duration) * Time.deltaTime;

                if (Math.Abs(t + incrementalRotation) > Math.Abs(rotation))
                {
                    transform.Rotate(new Vector3 { y = rotation - t });
                    break;
                }
                else
                {
                    t += incrementalRotation;
                    transform.Rotate(new Vector3 { y = incrementalRotation });
                    yield return null;
                }
            }

            _rotating = false;
        }
    }
}
