using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UdonLib.Commons;
using Zenject;
using govox;

public class OVRControllerRay : InitializableMono
{
    private const float RAY_LENGTH = 200f;

    [SerializeField]
    private OVRControllerRayVisualizer _rayVisualizer;

    [SerializeField]
    private bool _rayVisible;

    [Inject]
    private IRayHandler _rayHandler;

    private IVoxel _currentHitVoxel;
    public IVoxel CurrentHitVoxel => _currentHitVoxel;
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

        if(VoxelUtility.VoxelRayCast(CachedTransform.position, transform.TransformDirection(Vector3.forward), out var voxel, RAY_LENGTH))
        {
            rayPosition = voxel.HitPosition;
            _currentHitVoxel = voxel.VoxelInfo;
        }
        else if(_currentHitVoxel != null)
        {
            _currentHitVoxel.OnRayExit();
        }

        if (_rayVisible)
        {
            _rayVisualizer.DrawRay(rayPosition);
        }
    }
}
