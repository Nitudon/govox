using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UdonLib.Commons {

    public class FlashGroup:UdonBehaviour{

        [SerializeField]
        private CanvasGroup group;
        [SerializeField]
        private float duration = 1.0f;
        [SerializeField]
        private Ease easeType = Ease.Linear;
        [SerializeField]
        private float MinAlpha = 0f;
        [SerializeField]
        private float MaxAlpha = 1f;


        private Tweener _tweener;

        public void StartFlash()
        {
            group.alpha = MaxAlpha;
            _tweener = group.DOFade(MinAlpha, duration)
                .SetEase(easeType)
                .SetLoops(-1,LoopType.Yoyo);
        }

        public void StopFlash()
        {
            _tweener.Kill();
        }
    }
}
