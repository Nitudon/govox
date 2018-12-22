using System;
using UnityEngine;
using UnityEngine.UI;
using UdonLib.Commons;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UdonLib.UI
{
    /// <summary>
    /// 汎用インタラクション付きボタン
    /// </summary>
    public class CommonButtonBase : UIMono
    {
        [Header("Button Graphics")]
        [SerializeField]
        protected Graphic _hitArea;

        [SerializeField]
        protected Image _buttonImage;

        [Header("Button Options")]
        [SerializeField]
        protected bool _buttonEnable = true;

        [SerializeField]
        private bool _colorInteractionEnable = true;

        [SerializeField]
        private bool _scaleInteractionEnable = true;

        [SerializeField]
        private float _clickDuration = 0.3f;

        [Header("Button Interaction")]
        [SerializeField]
        private Color _disableColor = Color.gray * 1.2f;

        [SerializeField]
        private Color _pressedColor = Color.gray * 1.5f;

        [SerializeField]
        private float _scaleDelta = -0.08f;

        [SerializeField]
        private float _scaleDuration = 0.1f;

        [SerializeField]
        private TapDetector _tapDetector;

        public Action onClickedCallback;

        protected Color _baseColor = Color.white;
        protected Vector3 _baseScale = Vector3.one;

        protected override void Awake()
        {
            _baseColor = _buttonImage.color;
            _baseScale = _buttonImage.rectTransform.lossyScale;

            _tapDetector.onTapCallback += () => SetInteraction(true);
            _tapDetector.onTapEndCallback += () =>
            {
                SetInteraction(false);
                if(_tapDetector.PressedTime < _clickDuration)
                {
                    onClickedCallback?.Invoke();
                }
            };
        }

        public virtual void SetEnable(bool enable)
        {
            _buttonImage.color = enable ? _baseColor : _disableColor * _baseColor;
            _tapDetector.enabled = _tapDetector.enabled;
        }

        protected virtual void PlayScaling(bool isPress)
        {
            _buttonImage.DOKill();

            var endScale = isPress ? _baseScale + _scaleDelta * Vector3.one : _baseScale; 
            _buttonImage.rectTransform.DOScale(endScale, _scaleDuration);
        }

        protected virtual void SetPressedColor(bool isPress)
        {
            _buttonImage.color = isPress ? _pressedColor * _baseColor : _baseColor;
        }

        protected virtual void SetInteraction(bool isPress)
        {
            if(_colorInteractionEnable)
            {
                SetPressedColor(isPress);
            }
            if(_scaleInteractionEnable)
            {
                PlayScaling(isPress);
            }
        }

        #region Editor Script

        public void Reset()
        {
            var rect = GetComponentInChildren<HitRectArea>(true);
            var image = GetComponentInChildren<Image>(true);
            var tapDetector = GetComponentInChildren<TapDetector>(true);
            if(rect != null)
            {
                _hitArea = rect as Graphic;
            }
            else
            {
                _hitArea = image as Graphic;
            }

            _buttonImage = image;
            _tapDetector = tapDetector;
        }

        #endregion
    }
}
