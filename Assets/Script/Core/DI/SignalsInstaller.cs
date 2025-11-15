using KKM32.Controller;
using KKM32.Signal;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace KKM32.Installer
{
    public class SignalInstaller : MonoInstaller
    {
        //TODO : 로그인이 완료되고, Text변환

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            registerSignalHandler<FirebaseInitializeCompleteSignal, LoginUIController>(action: (x, y) => x.EnableLoginUI_Event(y), true);
            registerSignalHandler<ClickRegisterButtonSignal, LoginController>(action: (x, y) => x.TryRegister(y));
        }

        private void registerSignalHandler<TSignal, TController>(
            Action<TController, TSignal> action,
            bool nonLazyController = false)
        {
            Container.DeclareSignal<TSignal>().RequireSubscriber();

            var controllerBinding = Container.BindInterfacesAndSelfTo<TController>()
                                                .FromComponentInHierarchy().AsSingle();

            if (nonLazyController) controllerBinding.NonLazy();

            Container.BindSignal<TSignal>().ToMethod(action).FromResolve();

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