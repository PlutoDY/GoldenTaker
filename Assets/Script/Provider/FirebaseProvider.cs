using Firebase;
using Firebase.Auth;
using KKM32.Signal;
using UnityEngine;
using Zenject;

public interface IFirebaseCore
{
    FirebaseApp App { get; }

    void SetApp(FirebaseApp app);
}

public interface IAuthProvider
{
    FirebaseAuth Auth { get; }

    void SetAuth(FirebaseAuth auth);
}

public interface IUserProvider
{
    FirebaseUser User { get; }

    void SetUser();
}

public class FirebaseProvider : IFirebaseCore, IAuthProvider, IUserProvider
{
    private readonly SignalBus _firebaseInitializeCompleteSignalBus;

    [Inject]
    public FirebaseProvider(SignalBus _firebaseInitializeCompleteSignalBus)
    {
        this._firebaseInitializeCompleteSignalBus = _firebaseInitializeCompleteSignalBus;
    }

    public FirebaseApp App { get; private set; }

    public FirebaseAuth Auth { get; private set; }

    public FirebaseUser User { get; private set; }


    public void SetApp(FirebaseApp app) { App = app; }

    public void SetAuth(FirebaseAuth auth) { Auth = auth; }

    public void SetUser()
    {
        if (Auth.CurrentUser == null)
        {
            OnCompleteInitializeFirebase();
        }
        else
        {
            User = Auth.CurrentUser;
        }
    }

    public void OnCompleteInitializeFirebase()
    {
        _firebaseInitializeCompleteSignalBus.Fire(new FirebaseInitializeCompleteSignal { IsFirstLogin = true });
    }
}
