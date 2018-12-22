using UnityEngine;

namespace UdonLib.Commons
{
    public class InputTouchManager : UdonBehaviourSingleton<InputTouchManager>
    {
        public void SetMultiTouchEnable(bool enable)
        {
            Input.multiTouchEnabled = enable;
        }
    }
}
