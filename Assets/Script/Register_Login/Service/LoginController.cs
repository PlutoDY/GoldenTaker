using KKM32.Services;
using UnityEngine;
using Zenject;

namespace KKM32.Controller
{
    public class LoginController : MonoBehaviour
    {
        [Inject]
        private readonly ILoginService _iLoginService;

        public void TryRegister()
        {
            _iLoginService.Register();
        }
    }
}