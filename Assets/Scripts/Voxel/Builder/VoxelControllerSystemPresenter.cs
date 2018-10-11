using UniRx;
using UnityEngine;
using UdonLib.Commons;
using Zenject;

namespace govox
{
    public class VoxelControllerSystemPresenter : InitializableMono
    {
        [Inject]
        private VoxelBuilderModel _model;

        [SerializeField]
        private OVRInputHandler _eventPresenter;

        [SerializeField]
        private OVRControllerRayHandler _rayPresenter;

        [SerializeField]
        private OVRInputTouchPanelHandler _touchPanelHandler;

        [SerializeField]
        private VoxelCreator _creationUseCase;

        [SerializeField]
        private BuilderMover _movingUseCase;

        public void Initialize()
        {
            _eventPresenter.Initialize();
            _rayPresenter.Initialize();
            _touchPanelHandler.Initialize();
        }

        private void Bind()
        {
            _rayPresenter
                .;

            _eventPresenter
                .OnOVRInputGetDown(OVRInput.Button.PrimaryIndexTrigger)
                .Where(_ => _model.IsHit)
                .Subscribe(_ => _creationUseCase.Create(_model.RayHitInfo.MeshCenter))
                .AddTo(gameObject);

            _touchPanelHandler
                .OVRTouchAxisObservable
                .Subscribe(_movingUseCase.OnInputDelta)
                .AddTo(gameObject);
        }
    }
}