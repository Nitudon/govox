using UnityEngine;
using UdonObservable.InputRx.Key;
using UniRx;

namespace Commons
{
    [RequireComponent(typeof(CharacterController))]
    public class TestMover : MonoBehaviour
    {
        private readonly KeyCode[] _controllKeys = {
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow
    };

        [SerializeField]
        private float _speed = 1.0f;

        private CharacterController _controller;

        // Use this for initialization
        void Start ()
        {
            _controller = GetComponent<CharacterController>();

            KeyObservable.GetKeyObservable(KeyCode.A).Subscribe(_ => Debug.Log(""));

            KeyObservable.MultiKeyActionRegister(_controllKeys,
                () => _controller.Move(Vector3.forward * _speed),
                () => _controller.Move(Vector3.back * _speed),
                () => _controller.Move(Vector3.left * _speed),
                () => _controller.Move(Vector3.right * _speed)
            ).AddTo(gameObject);
        }
    }
}
