using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

public class TestRayHit : UdonBehaviour 
{
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
            var rayHandler = hit.collider.GetComponent<IRayHandler>();

            if (rayHandler != null)
            {
                rayHandler.OnRayHit(hit);
            }
        }
    }
}
