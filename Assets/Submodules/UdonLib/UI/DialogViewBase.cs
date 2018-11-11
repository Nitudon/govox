using System;
using UnityEngine;
using UdonLib.Commons;
using DG.Tweening;

namespace UdonLib.UI
{
    /// <summary>
    /// ダイアログのビューベース
    /// </summary>
    public class DialogViewBase : UIMono
    {
        protected const float _transitionTime = 0.5f;    

        public virtual void Initialize()
        {
            _cachedRectTransform.localScale = Vector3.zero;
        }

        public virtual void OpenDialogAnimation(Action onComplete = null)
        {
            _cachedRectTransform.DOScale(Vector3.one, _transitionTime).OnComplete(() => onComplete?.Invoke());
        }

        public virtual void CloseDialogAnimation(Action onComplete = null)
        {
            _cachedRectTransform.DOScale(Vector3.zero, _transitionTime).OnComplete(() => onComplete?.Invoke());
        }
    }
}