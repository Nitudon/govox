using System;
using UniRx;
using UdonLib.Commons;
using UnityEngine;

public class OVRControllerRayModel : IRayHandler, IDisposable
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

    public void Dispose()
    {
        OnRaycastHitObject.Dispose();
    }
}
