using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.IO;


public class FirstLogin : MonoBehaviour
{
    [SerializeField]
    [Header("パスワード")]
    private string _customKey;
    
    private bool _shouldCreateAccount;


    [SerializeField]
    private LoginKeyData _loginKeyData;
    
    void Start()
    {
        //ログイン
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }
    
    void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
    }
    
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";
    
    /// <summary>カスタムID生成</summary>
    /// <returns>生成したID</returns>
    private string GenerateCustomID()
    {
        int idLength = 32;//IDの長さ
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();
    
        for (int i = 0; i < idLength; i++)
        {
            stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
        }

        return stringBuilder.ToString();
    }
    
    
    //ログイン成功
    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        Debug.Log("ログイン成功");
        Debug.Log("アカウントが存在するかどうか確認");
        _shouldCreateAccount = result.NewlyCreated;
        if (!_shouldCreateAccount)
        {
            LoginKey loginKey = JsonSaveManager<LoginKey>.Load(_customKey);

            if (loginKey == null)
            {
                Debug.Log("アカウントが存在しないのでパスワードとキーを作成");
                loginKey = new LoginKey()
                {
                    _loginKeyData = GenerateCustomID(),
                    _playFabID = result.PlayFabId
                };
            }
            else
            {
                Debug.Log("既にこのパスワードのキーが存在する");
                _loginKeyData.SetValue(loginKey);
            }
        }
        else
        {
            Debug.Log("アカウントが存在するので正しいパスワードを入力して下さい");
        }
    }

    /// <summary>アプリケーション終了時に呼び出す</summary>
    private void OnApplicationQuit() 
    {
        OverWriteSaveData();
    }
    
    //保存
    public void OverWriteSaveData()
    {
        if (_shouldCreateAccount)
        {
            LoginKey loginKey = new LoginKey()
            {
                _loginKeyData = _loginKeyData.CustomID,
                _playFabID = _loginKeyData.PlayFabID
            };

            JsonSaveManager<LoginKey>.Save(loginKey, _customKey);
        }
    }

    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
    }

}
