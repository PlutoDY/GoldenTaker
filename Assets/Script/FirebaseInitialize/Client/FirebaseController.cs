using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using KKM32.Services;
using UnityEngine;
using Zenject;

namespace KKM32.Controller {
    public class FirebaseController : MonoBehaviour
    {
        [Inject]
        private readonly IFirebaseService _iFirebaseService;

        public void Start()
        {
            InitializeFirebase();
        }

        public void InitializeFirebase()
        {
            _iFirebaseService.InitializeFirebases();
        }

    }
}
