using KKM32.Controller;
using KKM32.Services;
using Zenject;

namespace KKM32.Installer
{
    public class FirebaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseProvider>().AsSingle();
        }
    }
}