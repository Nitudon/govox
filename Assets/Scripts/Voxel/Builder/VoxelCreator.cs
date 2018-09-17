using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using Zenject;

public class VoxelCreator : InitializableMono, IObjectCreator
{
    [SerializeField]
    private CreatableVoxel _baseVoxPrefab;
    
    [SerializeField]
    private Material _baseMaterial;
    private Material _material;

    public override void Initialize()
    {
        _material = new Material(_baseMaterial);
        _material.hideFlags = HideFlags.DontSave;
    }

    public void Create(Vector3 position, Transform parent)
    {
        var voxel = Instantiate(_baseVoxPrefab, parent);
        voxel.SetLocalPosition(position);
    }

    public void OnDestroy()
    {
        Destroy(_material);
    }

    public class Factory : PlaceholderFactory<VoxelCreator>{}
}
