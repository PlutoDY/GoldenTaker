using KKM32.Signal;
using UnityEngine;
using Zenject;

namespace KKM32.Controller
{
    public class LoginUIController : MonoBehaviour
    {

        [SerializeField]
        private LoginUIView _loginUIView;

        public void Start()
        {
            intializeLoginUIView();
        }

        private void intializeLoginUIView()
        {
            if (_loginUIView == null)
                gameObject.TryGetComponent<LoginUIView>(out _loginUIView);
        }

        public void EnableLoginUI_Event(FirebaseInitializeCompleteSignal _firebaseInitializeCompleteSignal)
        {
            if (_firebaseInitializeCompleteSignal.IsFirstLogin)
            {
                uiSetRegister();
            }
        }

        private void uiSetRegister()
        {
            Debug.Log($"들었다 병신아");
            enableRegisterUI();
        }

        private void enableRegisterUI()
        {
            _loginUIView.RegisterUIPanel.SetActive(true);
        }

        private void disableRegisterUI()
        {
            _loginUIView.RegisterUIPanel.SetActive(false);
        }


        #region Button Click Event

        [Inject]
        private readonly SignalBus _onClickRegisterButton;

        public void RegisterButtonClick()
        {
            _onClickRegisterButton.Fire(new ClickRegisterButton());
            disableRegisterUI();
        }

        #endregion
    }
}