using UnityEngine;
using UdonLib.Commons;

public class OVRControllerRayVisualizer : UdonBehaviour
{
    [SerializeField]
    private Transform _anchor;
    
    [SerializeField]
    private LineRenderer _lineRenderer;

    public void DrawRay(Vector3 pos)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _anchor.position);
        _lineRenderer.SetPosition(1, pos);
    }
}