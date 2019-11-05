using System;
using System.Collections;
using System.Collections.Generic;

namespace MatchingGame.Utilities
{
    public struct CoroutineAction<T>
    {
        public Action<T> onSuccess;
        public Action<T> onFailure;
        public IEnumerable<IEnumerator> yieldOnSuccess;
        public IEnumerable<IEnumerator> yieldOnFailure;
    }
}
