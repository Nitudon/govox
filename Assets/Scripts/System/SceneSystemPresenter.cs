using UnityEngine;
using UdonLib.Commons;

public class SceneSystemPresenter : UdonBehaviour
{
    [SerializeField]
    private InitializableMono[] _initializers;

    protected override void Start()
    {
        base.Start();
        _initializers.ForEach(x => x.Initialize());
    }
}
