using KKM32.Services;
using KKM32.Signal;
using KKM32.Util.CustomAtrribute;
using UnityEngine;
using Zenject;

namespace KKM32.Controller
{
    [BindingLifetime(BindingLifetime.Lazy)]
    public class LoginController : MonoBehaviour
    {
        [Inject]
        private readonly ILoginService _iLoginService;


        [ListenToSignal(typeof(ClickRegisterButtonSignal))]
        public void TryRegister()
        {
            _iLoginService.Register();
        }
    }
}