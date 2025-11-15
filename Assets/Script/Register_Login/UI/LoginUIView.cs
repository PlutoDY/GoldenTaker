using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KKM32.UI.Login {
    public class LoginUIView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _registerUIPanel;

        public GameObject RegisterUIPanel { get { return _registerUIPanel; } }

        [SerializeField]
        private Button _registerButton;

        public Button RegisterButton { get { return _registerButton; } }

        #region Test
        [SerializeField]
        private TMP_Text _statusText;

        public TMP_Text StatusText { get { return _statusText; } }

        #endregion

    }
}