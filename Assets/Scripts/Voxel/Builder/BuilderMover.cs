using UdonLib.Commons;
using UnityEngine;

namespace govox
{
    public class BuilderMover : UdonBehaviour, IDeltaInputHandler
    {
        private static readonly float DELTA_MOVE_UNIT = 0.1f;

        [SerializeField]
        private Transform _builderRoot;

        public void OnInputDelta(Vector2 delta)
        {
            _builderRoot.localEulerAngles += Vector3.up * DELTA_MOVE_UNIT * (delta.x > 0 ? 1 : -1);
        }
    }
}