using UnityEngine.UI;

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

}