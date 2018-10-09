using System;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using Zenject;
using govox;

[RequireComponent(typeof(MeshRenderer))]
public class CreatableVoxel : UdonBehaviour, IVoxel
{
    private RayCastMeshCollisionUseCase _meshRayCastUseCase;

    public void Initialize()
    {
        _meshRayCastUseCase = new RayCastMeshCollisionUseCase(CachedTransform);
    }

    public void OnRayHit(RaycastHit hit)
    {
        _meshRayCastUseCase.OnRayHit(hit);
    }

    public void OnRayExit()
    {
        _meshRayCastUseCase.OnRayExit();
    }

    public VoxelHitInfo GetHitInfo(RaycastHit hit)
    {
        var direction = _meshRayCastUseCase.HitDirection.Value;
        return new VoxelHitInfo(this, hit.point, VoxelUtility.DirectionalCenterPoint(CachedTransform.position, direction), direction);
    }

    public void SetMaterial()
    {
    }
}