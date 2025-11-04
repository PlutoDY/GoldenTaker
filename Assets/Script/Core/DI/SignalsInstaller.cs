using KKM32.Controller;
using KKM32.Signal;
using UnityEngine;
using Zenject;

namespace KKM32.Installer
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<FirebaseInitializeCompleteSignal>().RequireSubscriber();

            Container.BindInterfacesAndSelfTo<LoginUIController>().
                FromComponentInHierarchy().
                AsSingle().NonLazy();

            Container.BindSignal<FirebaseInitializeCompleteSignal>()
                .ToMethod<LoginUIController>(x => x.EnableLoginUI_Event).FromResolve();

            Container.DeclareSignal<ClickRegisterButton>();

            Container.BindSignal<ClickRegisterButton>().
                ToMethod<LoginController>(x => x.TryRegister).FromResolve();

        }
    }
}