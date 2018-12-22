using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

namespace UdonLib.Commons
{
    public class TapDetector : UdonBehaviour, IPointerUpHandler, IPointerDownHandler, IDisposable
    {
        private float _pressedTime;
        public float PressedTime => _pressedTime;

        private Vector2 _tapPosition;
        public Vector2 TapPosition => _tapPosition;

        public Action onTapCallback;
        public Action onTapEndCallback;

        private IDisposable _pressedUpdateDisposable;

        public void OnPointerDown(PointerEventData eventData)
        {
            _tapPosition = eventData.pressPosition;
            SetPressObserver(true);
            onTapCallback?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onTapEndCallback?.Invoke();
            SetPressObserver(false);
        }

        public void Dispose()
        {
            _pressedUpdateDisposable?.Dispose();
        }

        protected override void OnDisable()
        {
            Dispose();
        }

        protected virtual void OnDestroy()
        {
            Dispose();
        }

        private void SetPressObserver(bool enable)
        {
            if(enable)
            {
                _pressedTime = 0f;
                _pressedUpdateDisposable = Observable.FromMicroCoroutine(() => PressUpdateEnumerator()).Subscribe().AddTo(gameObject);
            }
            else
            {
                _pressedUpdateDisposable?.Dispose();
            }
        }

        private IEnumerator PressUpdateEnumerator()
        {
            _pressedTime += Time.deltaTime;
            yield return null;
        }
    }
}
