using System;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

public class TestRayHandlableCube : UdonBehaviour, IRayHandler
{
    private RayCastMeshCollisionUseCase _collision;

    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _collision = new RayCastMeshCollisionUseCase(CachedTransform);

        _collision.HitDirection.Pairwise().Subscribe(dir => 
        {
            _material.DisableKeyword(ShaderUtility.GetKeywordForMeshDirection(dir.Previous));
            _material.EnableKeyword(ShaderUtility.GetKeywordForMeshDirection(dir.Current));
        }).AddTo(gameObject);
    }

    public void OnRayHit(RaycastHit hit)
    {
        _collision.OnRayHit(hit);
    }
}