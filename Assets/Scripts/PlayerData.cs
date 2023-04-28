using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    [Header("名前")]
    private string _name;
    
    [SerializeField] 
    [Header("ランク")]
    private int _rank;
    
    [SerializeField]
    [Header("経験値")]
    private int _exp;
    
    async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        SetUserData();
    }

    public void SetUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>
                {
                    {"Name", _name},
                    {"Exp", _exp.ToString()} ,
                    {"Rank" , _rank.ToString()}
                }
            }, 
            result =>
            {
                Debug.Log("プレイヤーデータの更新完了");
            }, 
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });
    }
}
