using KKM32.Signal;
using KKM32.UI.Login;
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
            enableRegisterUI();
            setInteractableRegisterButton(true);
        }

        private void enableRegisterUI()
        {
            _loginUIView.RegisterUIPanel.SetActive(true);
        }

        private void disableRegisterUI()
        {
            _loginUIView.RegisterUIPanel.SetActive(false);
        }

        private void setInteractableRegisterButton(bool isActive)
        {
            _loginUIView.RegisterButton.interactable = isActive;
        }


        #region Button Click Event

        [Inject]
        private readonly SignalBus _signalBus;

        public void RegisterButtonClick()
        {
            _signalBus.Fire(new ClickRegisterButtonSignal());
            disableRegisterUI();
        }

        #endregion
        public void SetTextCompletedLogin()
        {
            _loginUIView.SetText("LoginComplete");
        }
    }
}