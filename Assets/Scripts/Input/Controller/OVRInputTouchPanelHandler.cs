using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UdonLib.Commons;

public class OVRInputTouchPanelHandler : InitializableMono
{
    private IObservable<Vector2> _updateTouchSubscription;
    public IObservable<Vector2> OVRTouchAxisObservable => _updateTouchSubscription; 

    public override void Initialize()
    {
        _updateTouchSubscription =
            this.FixedUpdateAsObservable()
                .Where(_ => OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
                .Select(_ => OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad));
    }
}