using UnityEngine;

public class LoginUIView : MonoBehaviour
{
    [SerializeField]
    private GameObject _registerUIPanel;

    public GameObject RegisterUIPanel { get { return _registerUIPanel; } }
}
