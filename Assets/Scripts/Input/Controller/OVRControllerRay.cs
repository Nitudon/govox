using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UdonLib.Commons;
using Zenject;

public class OVRControllerRay : InitializableMono
{
    private const float RAY_LENGTH = 200f;

    [SerializeField]
    private OVRControllerRayVisualizer _rayVisualizer;

    [SerializeField]
    private bool _rayVisible;

    [Inject]
    private IRayHandler _rayHandler;

    private IRayTriggerHandler _currentRayTriggerHandler;
    private bool _isValid;

    public override void Initialize()
    {
        _isValid = true;

        this
            .FixedUpdateAsObservable()
            .Where(_ => _isValid)
            .Subscribe(_ => CheckRaycastHit())
            .AddTo(gameObject);
    }

    public void SetValid(bool valid)
    {
        _isValid = valid;
    }

    private void CheckRaycastHit()
    {
        var rayPosition = CachedTransform.position + Vector3.forward * RAY_LENGTH;

        if(Physics.Raycast(CachedTransform.position, transform.TransformDirection(Vector3.forward), out var hit, RAY_LENGTH))
        {
            //_rayHandler.OnRayHit(hit);
            rayPosition = hit.transform.position;

            var handler = hit.collider.gameObject.GetComponent<TestRayHandlableCube>();
            if(handler != null)
            {
                _currentRayTriggerHandler = handler as IRayTriggerHandler;
                handler.OnRayHit(hit);
            }
            else if(_currentRayTriggerHandler != null)
            {
                _currentRayTriggerHandler.OnRayExit();
            }
        }

        if (_rayVisible)
        {
            _rayVisualizer.DrawRay(rayPosition);
        }
    }
}
