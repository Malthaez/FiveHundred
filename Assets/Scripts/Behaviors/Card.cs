using MatchingGame.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Vector3 _eulerRotation;

        public CardSuitsEnum Suit { get => _cardSuit; set => _cardSuit = value; }
        public int Value { get => _value; set => _value = value; }
        public Sprite Art { get => _cardArt.sprite; set => _cardArt.sprite = value; }
        public Sprite Back { get => _cardBack.sprite; set => _cardBack.sprite = value; }
        public Vector3 EulerRotation { get => _eulerRotation; set => _eulerRotation = value; }

        //========

        protected bool _moving = false;
        protected bool _rotating = false;

        public FaceDirection FaceDirection => GetComponentInChildren<CardAvatar>().FaceDirection;
        public bool Flipping => GetComponentInChildren<CardAvatar>().Flipping;
        public bool Moving { get => _moving; set => _moving = value; }
        public bool Rotating => _rotating;


        public IEnumerator Flip(FaceDirection flipDirection, float duration) => GetComponentInChildren<CardAvatar>().Flip(flipDirection, duration);

        private IEnumerator MoveTo(Vector3 destinationPosition, float speed)
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

        //public IEnumerator RotateTo(float finalRotation, float duration)
        //{
        //    var initialRotation = transform.rotation.eulerAngles.z;

        //    if (initialRotation == finalRotation) { yield break; }

        //    var rotation = finalRotation - initialRotation;
        //    var t = 0f;

        //    _rotating = true;

        //    while (_rotating)
        //    {
        //        var incrementalRotation = (rotation / duration) * Time.deltaTime;

        //        if (Math.Abs(t + incrementalRotation) > Math.Abs(rotation))
        //        {
        //            transform.Rotate(new Vector3 { y = rotation - t });
        //            break;
        //        }
        //        else
        //        {
        //            t += incrementalRotation;
        //            transform.Rotate(new Vector3 { y = incrementalRotation });
        //            yield return null;
        //        }
        //    }

        //    _rotating = false;
        //}

        private IEnumerator RotateTo(Quaternion finalRotation, float duration)
        {
            var finalEulers = finalRotation.eulerAngles;

            if (transform.rotation == finalRotation)
            {
                Debug.Log($"Initial Rotation: {_eulerRotation}; Final Rotation: {finalEulers}");
                yield break;
            }

            var speed = (finalEulers.z - _eulerRotation.z) / duration;
            var condition = (speed > 0) ? new Func<Vector3, bool>((Vector3 eulers) => eulers.z < finalEulers.z) : new Func<Vector3, bool>((Vector3 eulers) => eulers.z > finalEulers.z);
            _rotating = true;

            while (condition(_eulerRotation))
            {
                _eulerRotation.z += speed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(_eulerRotation);
                yield return null;
            }
            transform.rotation = finalRotation;

            _rotating = false;
        }

        public List<Coroutine> DoCardStuff(Vector3 destPosition, Quaternion destRotation, float speed, bool revealCard)
        {
            var coroutines = new List<Coroutine>();
            var duration = Vector3.Distance(transform.position, destPosition) / speed;

            coroutines.Add(StartCoroutine(MoveTo(destPosition, speed)));
            coroutines.Add(StartCoroutine(RotateTo(destRotation, duration)));
            coroutines.Add(StartCoroutine(Flip(revealCard ? FaceDirection.Up : FaceDirection.Down, duration)));

            return coroutines;
        }

        public List<Coroutine> DoCardStuff(Vector3 destPosition, Quaternion destRotation, float speed) => DoCardStuff(destPosition, destRotation, speed, false);
    }
}
