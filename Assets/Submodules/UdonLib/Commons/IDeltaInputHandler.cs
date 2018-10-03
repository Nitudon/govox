using UnityEngine;

namespace UdonLib.Commons
{
    public interface IDeltaInputHandler
    {
         void OnInputDelta(Vector2 delta);
    }
}