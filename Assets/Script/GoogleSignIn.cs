using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

using Google;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

public class GoogleSignIn : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text statusText;

    [SerializeField]
    private Button registerButton;

    FirebaseApp app;
    FirebaseAuth auth;
    FirebaseUser user;

/*    private void firebaseInitialize() {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(fireTask =>
        {
            var dependencyStatus = fireTask.Result;


            if(dependencyStatus == DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;

                statusText.text = "FirebaseInitialzie Complete";

                registerButton.interactable = true;
            }
            else
            {
                statusText.text = "FirebaseInitalize Faild";
            }
        });
    }*/

    public void googleSignIn()
    {
        Google.GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            RequestEmail = true,
            WebClientId = "67072481159-5pll9osahuhldheom60cajs92s90rtn0.apps.googleusercontent.com"
        };

        Task<GoogleSignInUser> signIn = Google.GoogleSignIn.DefaultInstance.SignIn();

        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();
        signIn.ContinueWith(task => {
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
                Login(signIn, signInCompleted);
            }
        });

    }

    private void Login(Task<GoogleSignInUser> signIn, TaskCompletionSource<FirebaseUser> signInCompleted)
    {
        if (app == null || auth == null) return;

        string text;

        Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)signIn).Result.IdToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
        {
            if (authTask.IsCanceled)
            {
                signInCompleted.SetCanceled();

                text = "Firebase Auth Sign In was Canceled";
            }
            else if (authTask.IsFaulted)
            {
                signInCompleted.SetException(authTask.Exception);

                text = "Firebase Auth Sign In was Faild " + authTask.Exception.ToString();
            }
            else
            {
                signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);

                user = auth.CurrentUser;

                if (user == null)
                {
                    text = "User is Null";
                }
                else 
                {
                    text = $"Name = {user.DisplayName}\nE-mail = \n{user.Email}";
                }

                registerButton.gameObject.SetActive(false);
            }

            statusText.text = text;
        });


    }

}
