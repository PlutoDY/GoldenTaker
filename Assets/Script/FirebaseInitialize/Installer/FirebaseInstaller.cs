using KKM32.Controller;
using KKM32.Services;
using UnityEngine;
using Zenject;

public class FirebaseInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IFirebaseService>().To<FirebaseService>().AsSingle();
    }
}