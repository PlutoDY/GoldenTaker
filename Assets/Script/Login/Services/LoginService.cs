using Firebase.Auth;
using Google;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace KKM32.Services {
    public interface ILoginService
    {
        void Login();
    }

    public class LoginService : ILoginService
    {
        public Action OnCompletedLogin;

        private Task<GoogleSignInUser> signIn;
        private TaskCompletionSource<FirebaseUser> signInCompleted;
        private FirebaseAuth _auth;

        public LoginService(FirebaseService firebaseService)
        {
            firebaseService.OnCompletedInitializeFirebase -= SetterFirebaseAuth;
            firebaseService.OnCompletedInitializeFirebase += SetterFirebaseAuth;
        }

        public void SetterFirebaseAuth(FirebaseAuth auth) { _auth = auth; Login(); }

        public void Login()
        {
            Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)signIn).Result.IdToken, null);
            _auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
            {
                if (authTask.IsCanceled)
                {
                    signInCompleted.SetCanceled();
                }
                else if (authTask.IsFaulted)
                {
                    signInCompleted.SetException(authTask.Exception);
                }
                else
                {
                    signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);

                    OnCompletedLogin.Invoke();
                }
            });
        }

        public void Register()
        {
            Google.GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
                RequestEmail = true,
                WebClientId = "67072481159-5pll9osahuhldheom60cajs92s90rtn0.apps.googleusercontent.com"
            };

            signIn = Google.GoogleSignIn.DefaultInstance.SignIn();

            signInCompleted = new TaskCompletionSource<FirebaseUser>();

            signIn.ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    signInCompleted.SetCanceled();
                }
                else if (task.IsFaulted)
                {
                    signInCompleted.SetException(task.Exception);
                }
                else
                {
                    Login();
                }
            });
        }
    }
}