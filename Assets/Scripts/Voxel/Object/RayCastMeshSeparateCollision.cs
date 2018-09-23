using System;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

public class RayCastMeshSeparateCollision : IRayHandler, IDisposable
{
    private const uint MESH_COUNT = 6;

    private ReactiveProperty<DirectionXYZ> _hitDirection;
    public IReadOnlyReactiveProperty<DirectionXYZ> HitDirection => _hitDirection;

    private Transform _targetTransform;

    public RayCastMeshSeparateCollision(Transform transform)
    {
        _targetTransform = transform;
        _hitDirection = new ReactiveProperty<DirectionXYZ>();
    }

    public void OnRayHit(RaycastHit hit)
    {
        _hitDirection.Value = CheckDirectionalHitRayCast(hit);
    }

    public void OnRayExit()
    {
        _hitDirection.Value = DirectionXYZ.None;
    }

    private DirectionXYZ CheckDirectionalHitRayCast(RaycastHit hit)
    {
        for(int i = 0; i < MESH_COUNT; ++i)
        {
            Vector3 normalDir, meshCenter;
            int div2 = i / 2;
            int mod2 = i - div2 * 2;
            bool cross1, cross2;
            float halfScale = ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;

            switch (div2)
            {
                case 0:
                    normalDir = mod2 == 0 ? Vector3.up : Vector3.down;
                    meshCenter = _targetTransform.position + normalDir * halfScale;
                    cross1 = meshCenter.x - halfScale < hit.point.x && hit.point.x < meshCenter.x + halfScale;
                    cross2 = meshCenter.z - halfScale < hit.point.z && hit.point.z < meshCenter.z + halfScale;
                    break;
                case 1:
                    normalDir = mod2 == 0 ? Vector3.forward : Vector3.back;
                    meshCenter = _targetTransform.position + normalDir * halfScale;
                    cross1 = meshCenter.x - halfScale < hit.point.x && hit.point.x < meshCenter.x + halfScale;
                    cross2 = meshCenter.y - halfScale < hit.point.y && hit.point.y < meshCenter.y + halfScale;
                    break;
                case 2:
                    normalDir = mod2 == 0 ? Vector3.left : Vector3.right;
                    meshCenter = _targetTransform.position + normalDir * halfScale;
                    cross1 = meshCenter.y - halfScale < hit.point.y && hit.point.y < meshCenter.y + halfScale;
                    cross2 = meshCenter.z - halfScale < hit.point.z && hit.point.z < meshCenter.z + halfScale;
                    break;
                default:
                    InstantLog.StringLogError("Invalid Mesh Count");
                    return DirectionXYZ.None;
            }
            if(cross1 && cross2)
            {
                return (DirectionXYZ)i;
            }
        }

        InstantLog.StringLogError("Invalid RayCast Hit");
        return DirectionXYZ.None;
    }

    public void Dispose()
    {
        _hitDirection.Dispose();
    }
}
