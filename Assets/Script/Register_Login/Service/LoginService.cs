using Firebase.Auth;
using Google;
using UnityEngine;
using System.Threading.Tasks;
using Zenject;

namespace KKM32.Services {
    public interface ILoginService
    {
        void Register();
    }

    public class LoginService : ILoginService
    {
        private readonly IAuthProvider _auth;
        private readonly IUserProvider _user;



        [Inject]
        public LoginService(IAuthProvider _auth, IUserProvider _user)
        {
            this._auth = _auth;
            this._user = _user;
        }

        private Task<GoogleSignInUser> signIn;
        private TaskCompletionSource<FirebaseUser> signInCompleted;

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

                    Debug.Log("Google Sign In Task is Canceled");
                }
                else if (task.IsFaulted)
                {
                    signInCompleted.SetException(task.Exception);

                    Debug.LogError(task.Exception);
                }
                else
                {
                    Credential credential = GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)signIn).Result.IdToken, null);

                    _auth.Auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
                    {
                        if (authTask.IsCanceled)
                        {
                            signInCompleted.SetCanceled();

                            Debug.Log("Firebase Auth Login In was Canceld");
                        }
                        else if (authTask.IsFaulted)
                        {
                            signInCompleted.SetException(authTask.Exception);

                            Debug.LogError(task.Exception);
                        }
                        else
                        {
                            signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);

                            _user.SetUser();
                        }

                    });
                }
            });
        }
    }
}