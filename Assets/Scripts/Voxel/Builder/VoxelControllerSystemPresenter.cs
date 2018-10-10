using UniRx;
using UnityEngine;
using UdonLib.Commons;

namespace govox
{
    public class VoxelControllerSystemPresenter : InitializableMono
    {
        [SerializeField]
        private OVRInputHandler _eventPresenter;

        [SerializeField]
        private OVRControllerRayHandler _rayPresenter;

        [SerializeField]
        private OVRInputTouchPanelHandler _touchPanelHandler;

        [SerializeField]
        private VoxelCreator _creationUseCase;

        public void Initialize()
        {
            _eventPresenter.Initialize();
            _rayPresenter.Initialize();
            _touchPanelHandler.Initialize();
        }

        private void Bind()
        {
            _eventPresenter
                .OnOVRInputGetDown(OVRInput.Button.PrimaryIndexTrigger)
                .Subscribe(_ => _creationUseCase.Create())
                .AddTo(gameObject);
        }
    }
}