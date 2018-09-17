using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<OVRInputHandler>().AsCached();
        Container.Bind<IRayHandler>().To<OVRControllerRayModel>().AsCached();
    }
}
