using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Utilities
{
    public static class CoroutineUtilities
    {
        public delegate Coroutine GameAction<T>(T val);
        public delegate Coroutine GameAction<T1, T2>(T1 val1, T2 val2);
        public delegate Coroutine GameAction<T1, T2, T3>(T1 val1, T2 val2, T3 val3);

        public static IEnumerable<Coroutine> StartAllCoroutines(this MonoBehaviour gameObject, IEnumerable<IEnumerator> iEnumerators)
        {
            var coroutines = new List<Coroutine>();

            foreach (var iEnumerator in iEnumerators)
            {
                coroutines.Add(gameObject.StartCoroutine(iEnumerator));
            }

            return coroutines;
        }

        public static IEnumerator AwaitAllCoroutines(this MonoBehaviour gameObject, IEnumerable<Coroutine> coroutines)
        {
            foreach (var coroutine in coroutines)
            {
                yield return coroutine;
            }
        }

        public static IEnumerator AwaitAllGameActions<T>(this MonoBehaviour gameObject, IEnumerable<GameAction<T>> gameActions, T val)
        {
            if (gameActions != null)
            {
                foreach (var gameAction in gameActions)
                {
                    yield return gameAction?.Invoke(val);
                }
            }
        }

        public static IEnumerator AwaitAllGameActions<T1, T2>(this MonoBehaviour gameObject, IEnumerable<GameAction<T1, T2>> gameActions, T1 val1, T2 val2)
        {
            if (gameActions != null)
            {
                foreach (var gameAction in gameActions)
                {
                    yield return gameAction?.Invoke(val1, val2);
                }
            }
        }

        public static IEnumerator AwaitAllGameActions<T1, T2, T3>(this MonoBehaviour gameObject, IEnumerable<GameAction<T1, T2, T3>> gameActions, T1 val1, T2 val2, T3 val3)
        {
            if (gameActions != null)
            {
                foreach (var gameAction in gameActions)
                {
                    yield return gameAction?.Invoke(val1, val2, val3);
                }
            }
        }
    }
}
