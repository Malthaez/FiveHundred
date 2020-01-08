using Assets.Scripts.Game.Factories;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Behaviors
{
    [RequireComponent(typeof(Collider))]
    public class CardAvatar : MonoBehaviour
    {
        [SerializeField] protected FaceDirection _faceDirection = FaceDirection.Down;
        protected bool _flipping = false;

        public FaceDirection FaceDirection => _faceDirection;
        public bool Flipping => _flipping;

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

            _faceDirection = flipDirection;
            _flipping = false;
        }
    }
}
