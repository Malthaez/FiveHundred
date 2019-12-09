using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class CoroutineWithData<T>
    {
        private Coroutine _coroutine;
        private T _result;
        private IEnumerator _target;

        public Coroutine Coroutine => _coroutine;
        public T Result => _result;

        public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
        {
            _target = target;
            _coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while(_target.MoveNext())
            {
                _result = (T)_target.Current;
                yield return _result;
            }
        }
    }
}
