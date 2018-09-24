using System;
using UnityEngine;
using UniRx;
using UdonLib.Commons;
using Zenject;

[RequireComponent(typeof(MeshRenderer))]
public class CreatableVoxel : UdonBehaviour, IRayTriggerHandler
{
    private IRayTriggerHandler _meshRayCastUseCase;

    public void Initialize()
    {
        _meshRayCastUseCase = new RayCastMeshCollisionUseCase(CachedTransform);
    }

    public void OnRayExit(RaycastHit hit)
    {
        _meshRayCastUseCase.OnRayHit(hit);
    }

    public void OnRayHit(RaycastHit hit)
    {
        _meshRayCastUseCase.OnRayExit(hit);
    }

    public void SetMaterial()
    {
    }
}