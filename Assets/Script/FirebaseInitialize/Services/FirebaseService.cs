using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using UnityEngine;

namespace KKM32.Services
{

    public interface IFirebaseService
    {
        void InitializeFirebases();
    }

    public class FirebaseService : IFirebaseService
    {
        public Action<FirebaseAuth> OnCompletedInitializeFirebase;

        private FirebaseApp _app;
        private FirebaseAuth _auth;
        private FirebaseUser _user;

        public FirebaseApp App { get { return _app; } }
        public FirebaseAuth Auth { get { return _auth; } }
        public FirebaseUser User { get { return _user; } }

        public FirebaseService(LoginService loginService)
        {
            loginService.OnCompletedLogin -= SetFirebaseUser;
            loginService.OnCompletedLogin += SetFirebaseUser;
        }

        public void InitializeFirebases()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(firebaseTask =>
            {
                var dpendecnyStatus = firebaseTask.Result;

                if (dpendecnyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    _auth = FirebaseAuth.DefaultInstance;

                    Debug.Log($"Firebase Initialize Scussess");

                    OnCompletedInitializeFirebase.Invoke(_auth);
                }
                else
                {
                    Debug.Log($"Firebase Initialize Faild");
                }

            });
        }

        public void SetFirebaseUser()
        {
            if (_auth.CurrentUser == null) return;

            _user = _auth.CurrentUser;
        }
    }
}
