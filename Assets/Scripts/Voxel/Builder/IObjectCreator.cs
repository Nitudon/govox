using UnityEngine;

public interface IObjectCreator
{
    void Create(Vector3 position, Transform root);
}
