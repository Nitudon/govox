using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using govox;

public class TestRayHit : UdonBehaviour 
{
    [SerializeField]
    private Transform _objectRoot;

    [SerializeField]
    private VoxelCreator _voxelCreator;

    private const float RAY_LENGTH = 200f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckRaycastHit();
        }
    }

    private void CheckRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var rayHit = Physics.Raycast(ray, out var hit, RAY_LENGTH);

        if(rayHit)
        {
            var rayHandler = hit.collider.GetComponent<IVoxel>();

            if (rayHandler != null)
            {
                rayHandler.OnRayHit(hit);
                //_voxelCreator.Create(_objectRoot);
            }
        }
    }
}
