using UniRx;
using UdonLib.Commons;
using UnityEngine;

public class OVRControllerRayModel : IRayHandler
{
    public Subject<RaycastHit> OnRaycastHitObject;

    public OVRControllerRayModel()
    {
        OnRaycastHitObject = new Subject<RaycastHit>();
    }

    public void OnRayHit(RaycastHit hit)
    {
        OnRaycastHitObject.OnNext(hit);
    }
}
