using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace KKM32.Services
{

    public interface IFirebaseService
    {
        void InitializeFirebases();
    }

    public class FirebaseService : IFirebaseService
    {
        private readonly IFirebaseCore _app;
        private readonly IAuthProvider _auth;
        private readonly IUserProvider _user;

        [Inject]
        public FirebaseService(IFirebaseCore _app, IAuthProvider _auth, IUserProvider _user)
        {
            this._app = _app;
            this._auth = _auth;
            this._user = _user;
        }

        public void InitializeFirebases()
        {

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(firebaseTask =>
            {
                var dependencyStatus = firebaseTask.Result;

                if(dependencyStatus == DependencyStatus.Available)
                {
                    _app.SetApp(FirebaseApp.DefaultInstance);
                    _auth.SetAuth(FirebaseAuth.DefaultInstance);

                    Debug.Log($"Completed Firebase Initialize");

                    _user.SetUser();
                }
                else
                {
                    Debug.LogError($"Faild Firebase Initialize");
                }
            });

        }
    }
}
