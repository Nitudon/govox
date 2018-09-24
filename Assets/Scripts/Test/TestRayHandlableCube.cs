using UnityEngine;
using UdonLib.Commons;
using UniRx;

public class TestRayHandlableCube : UdonBehaviour, IRayHandler
{
    private RayCastMeshSeparateCollision _collision;

    private void Start()
    {
        _collision = new RayCastMeshSeparateCollision(CachedTransform);

        _collision.HitDirection.Subscribe(dir => Debug.Log(dir)).AddTo(gameObject);
    }

    public void OnRayHit(RaycastHit hit)
    {
        _collision.OnRayHit(hit);
    }
}