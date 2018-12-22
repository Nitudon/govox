using UnityEngine;
using UdonLib.Commons;

namespace UdonLib.UI
{
    /// <summary>
    /// Canvas上のパーツのBase
    /// </summary>
    public class UIMono : UdonBehaviour
    {
        protected RectTransform _cachedRectTransform;
        public RectTransform RectTransform 
        {
            get
            {
                if(_cachedRectTransform == null)
                {
                    _cachedRectTransform = GetComponent<RectTransform>();
                }
                return _cachedRectTransform;
            }
        }

        public void ForceUpdate()
        {
            _cachedRectTransform?.ForceUpdateRectTransforms();
        }

        protected virtual void OnDestroy()
        {
            _cachedRectTransform = null;
        }
    }
}