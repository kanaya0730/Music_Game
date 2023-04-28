using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class LoginKey
{
    public string _loginKeyData;

    public string _playFabID;
}

public class LoginKeyData : MonoBehaviour
{
    public string CustomID => _customID;
    public string PlayFabID => _playFabID;
    
    [SerializeField]
    private string _customID;

    [SerializeField]
    private string _playFabID;
    
    public void SetValue(LoginKey Keydata)
    {
        _customID = Keydata._loginKeyData;
        _playFabID = Keydata._playFabID;
    }
}
