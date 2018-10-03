using System.Collections.Generic;
using UnityEngine.Events;

namespace UdonLib.Common
{
    /// <summary>
    /// オブジェクトのプーリングを行うやつ
    /// </summary>
    public class ObjectPoolBase<T> where T : new()
    {
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly UnityAction<T> _actionOnGet;
        private readonly UnityAction<T> _actionOnRelease;

        public int CountAll { get; private set; }

        public int CountInactive
        {
            get { return _stack.Count; }
        }

        public ObjectPoolBase(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
        {
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
        }

        public T Rent()
        {
            T element;
            if (_stack.Count == 0)
            {
                element = new T();
                CountAll++;
            }
            else
            {
                element = _stack.Pop();
            }

            if (_actionOnGet != null)
                _actionOnGet(element);
            return element;
        }

        public void Release(T element)
        {
            if (_stack.Count > 0 && ReferenceEquals(_stack.Peek(), element))
            {
                UnityEngine.Debug.LogError(
                    "Internal error. Trying to destroy object that is already released to pool.");
            }

            if (_actionOnRelease != null)
            {
                _actionOnRelease(element);
            }

            _stack.Push(element);
        }
    }
}