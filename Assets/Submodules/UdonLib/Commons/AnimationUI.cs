using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UdonLib.Commons {

    public class AnimationUIText : Text{

        private Sequence TextAnimation;

        private Action OnAnimationStarted;
        private Action OnAnimationEnded;

        public float Duration
        {
            get
            {
                if(TextAnimation == null)
                {
                    return 0;
                }
                else
                {
                    return TextAnimation.Duration();
                }
            }
        }

        public void SetOnAnimationStarted(Action act)
        {
            OnAnimationStarted += act;
        }

        public void SetOnAnimationEnded(Action act)
        {
            OnAnimationEnded += act;
        }

        public void SetAnimation(Sequence seq)
        {
            TextAnimation = seq;
        }

        public void Play()
        {
            if(TextAnimation == null)
            {
                InstantLog.StringLogError("Animation isn't set");
                return;
            }

            TextAnimation
                .OnStart(() =>
                {
                    if (OnAnimationStarted != null)
                    {
                        OnAnimationStarted();
                    }
                })
                .OnComplete(() =>
                {
                    if (OnAnimationEnded != null)
                    {
                        OnAnimationEnded();
                    }
                });

            TextAnimation.Play();

        }

    }

}