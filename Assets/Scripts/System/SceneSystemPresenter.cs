using System.Tasks.
using UnityEngine;

namespace UdonLib.Commons
{
    public class SceneSystemPresenter : UdonBehaviour
    {
        [SerializeField]
        private InitializableMono[] _initializers;

        [SerializeField]
        private ncInitializableMono[] _asyncInitializers;

        protected override async void Start()
        {
            base.Start();
            _initializers.ForEach(x => x.Initialize());
            await 
        }
    }
}
