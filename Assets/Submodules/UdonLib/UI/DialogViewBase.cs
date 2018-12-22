using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using DG.Tweening;
using UdonLib.Async;

namespace UdonLib.UI
{
    /// <summary>
    /// ダイアログのビューベース
    /// </summary>
    public class DialogViewBase : UIMono
    {
        [SerializeField]
        private FadeUIGroup _fadeCanvas;

        [SerializeField]
        private Text _title;

        protected const float _transitionTime = 0.5f;    

        public virtual void Initialize()
        {
            _cachedRectTransform.localScale = Vector3.zero;
        }

        public void SetVisible(bool visible)
        {
            _fadeCanvas.SetVisible(visible);
        }

        public void Fade(bool visible)
        {
            _fadeCanvas.FadeGroup(visible);
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public virtual async UniTask OpenDialogAnimation()
        {
            await _cachedRectTransform.DOScale(Vector3.one, _transitionTime);
        }

        public virtual async UniTask CloseDialogAnimation()
        {
            await _cachedRectTransform.DOScale(Vector3.zero, _transitionTime);
        }
    }
}