using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UdonLib.UI
{
    public class HitRectArea : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            vh.Clear();
        }
    }

    #region Editor Script
    [CustomEditor(typeof(HitRectArea))]
    public class HitRectAreaEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            HitRectArea rect = target as HitRectArea;

            rect.raycastTarget = EditorGUILayout.Toggle("RayCast Target", rect.raycastTarget);
        }
    }
    #endregion

}