using System;
using UnityEngine;
using DG.Tweening;

namespace UdonLib.UI
{
    /// <summary>
    /// FadeするUIパーツ
    /// </summary>
    public class FadeUIGroup : UIMono
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private float _fadeDuration = 0.3f;

        public void FadeGroup(bool visible, Action onCompleted = null)
        {
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(visible ? 1f : 0f, _fadeDuration).OnComplete(() => onCompleted?.Invoke());
        }

        public void SetVisible(bool visible)
        {
            _canvasGroup.DOKill();
            _canvasGroup.alpha = visible ? 1f : 0f;
        }

        public void SetRayCastTarget(bool active)
        {
            _canvasGroup.blocksRaycasts = active;
        }
    }
}