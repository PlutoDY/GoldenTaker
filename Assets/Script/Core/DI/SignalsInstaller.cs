using KKM32.Controller;
using KKM32.Signal;
using System;
using System.Linq.Expressions;
using Zenject;

namespace KKM32.Installer
{
    public class SignalInstaller : MonoInstaller
    {
        //TODO : 로그인이 완료되고, Text변환

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            bindingAllInterfaces();

            registerSignalHandlers();
        }

        private void bindingAllInterfaces()
        {
            bindingInterFace<LoginUIController>(true);
            bindingInterFace<LoginController>();
        }

        private void registerSignalHandlers()
        {
            registerSignalHandler<FirebaseInitializeCompleteSignal, LoginUIController>((x, y) => x.EnableLoginUI_Event(y));
            registerSignalHandler<ClickRegisterButtonSignal, LoginController>((x, y) => x.TryRegister());
            registerSignalHandler<LoginCompleteSignal, LoginUIController>((x, y) => x.SetTextCompletedLogin());
        }

        private void bindingInterFace<TController>(bool nonLazy = false)
        {
            var bindingInterface = Container.BindInterfacesAndSelfTo<TController>()
                .FromComponentInHierarchy()
                .AsSingle();

            if (nonLazy) bindingInterface.NonLazy();
        }

        private void registerSignalHandler<TSignal, TController>(Expression<Action<TController, TSignal>> expr)
        {
            Container.DeclareSignal<TSignal>().RequireSubscriber();

            var action = expr.Compile();

            Container.BindSignal<TSignal>().ToMethod(action).FromResolve(); ;
        }

/*       private void _registerFirebaseInitializeCompleteSignal()
        {
            Container.DeclareSignal<FirebaseInitializeCompleteSignal>().RequireSubscriber();

            Container.BindInterfacesAndSelfTo<LoginUIController>().
                FromComponentInHierarchy().
                AsSingle().NonLazy();

            Container.BindSignal<FirebaseInitializeCompleteSignal>()
                .ToMethod<LoginUIController>(x => x.EnableLoginUI_Event).FromResolve();
        }

        private void _registerClickRegisterButtonSignal()
        {
            Container.DeclareSignal<ClickRegisterButtonSignal>().RequireSubscriber();

            Container.BindInterfacesAndSelfTo<LoginController>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindSignal<ClickRegisterButtonSignal>().
                ToMethod<LoginController>(x => x.TryRegister).FromResolve();
        }*/
    }
}