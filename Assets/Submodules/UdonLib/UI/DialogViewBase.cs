using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            _cachedRectTransform.DOScale(Vector3.one, _transitionTime);
        }

        private async Task OpenDialog()
        {

        }
    }
}