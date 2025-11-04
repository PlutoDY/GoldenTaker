using KKM32.Services;
using KKM32.Signal;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace KKM32.Controller
{
    public class LoginController : MonoBehaviour
    {
        [Inject]
        private readonly IUserProvider _user;

        [Inject]
        private readonly ILoginService _iLoginService;


        // 시그널 버스 Get
        public void TryRegister(ClickRegisterButton _clickRegisterButton)
        {
            _iLoginService.Register();
        }
    }
}