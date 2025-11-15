using System;

namespace KKM32.Signal
{
    [Serializable]
    public class FirebaseInitializeCompleteSignal
    {
        public bool IsFirstLogin = false;
    }

    public class ClickRegisterButtonSignal {}

    public class LoginCompleteSignal {}
}