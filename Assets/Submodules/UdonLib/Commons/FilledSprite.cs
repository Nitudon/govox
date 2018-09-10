using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UdonLib.Commons;

namespace UdonLib.Commons {

    public class FilledSprite : Image {

        protected override void Awake()
        {
            base.Awake();
            fillMethod = FillMethod.Horizontal;
        }

        public void MaxFill()
        {
            fillAmount = 1.0f;
        }

        public void MinFill()
        {
            fillAmount = 0.0f;
        }

        public void SetFill(float rate)
        {
            fillAmount = rate;
        }

        public void PlusFill(float rate)
        {
            fillAmount += rate;
        }

        public void MinusFill(float rate)
        {
            fillAmount -= rate;
        }

        public void MultiFill(float rate)
        {
            fillAmount *= rate;
        }

        public void DivFill(float rate)
        {
            fillAmount /= rate;
        }

    }
}
