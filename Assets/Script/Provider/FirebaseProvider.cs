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
    private readonly SignalBus _signalBus;

    [Inject]
    public FirebaseProvider(SignalBus _signalBus)
    {
        this._signalBus = _signalBus;
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
            onCompleteInitializeFirebase();
        }
        else
        {
            User = Auth.CurrentUser;

            notifyLoginComplete();
        }
    }

    private void onCompleteInitializeFirebase()
    {
        _signalBus.Fire(new FirebaseInitializeCompleteSignal { IsFirstLogin = true });
    }

    private void notifyLoginComplete()
    {
        _signalBus.Fire(new LoginCompleteSignal());
    }
}
