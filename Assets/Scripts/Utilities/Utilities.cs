using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Utilities
{
    public class Utilities : MonoBehaviour
    {
        public static IEnumerator AwaitAllCoroutines(IEnumerable<Coroutine> coroutines)
        {
            foreach (var coroutine in coroutines)
            {
                yield return coroutine;
            }
        }
    }
}
