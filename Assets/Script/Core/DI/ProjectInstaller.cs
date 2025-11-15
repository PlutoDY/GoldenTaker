using KKM32.Services;
using Zenject;


namespace KKM32.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFirebaseService>().To<FirebaseService>().AsSingle();
            Container.Bind<ILoginService>().To<LoginService>().AsSingle();
        }
    }
}
