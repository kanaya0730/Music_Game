using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

[Serializable]
public class MonsterMaster
{
    public int id;
    public string name;
    public float hp;
    public float power;
    public string type;
    public int gold;
    
    public enum Type
    {
        fire,
        water,
        wood,
        shine,
        dark,
    }
    

}
public class MonsterData : MonoBehaviour
{
    
    async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        GetTitleData();
    }

    public void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result =>
            {
                var monsterMaster =
                    PlayFab.Json.PlayFabSimpleJson.DeserializeObject<List<MonsterMaster>>(result.Data["MonsterData"]);
                foreach (var monster in monsterMaster)
                {
                    Debug.Log($"{monster.id}");
                    Debug.Log($"{monster.name}");
                    Debug.Log($"{monster.hp}");
                    Debug.Log($"{monster.power}");
                    Debug.Log($"{monster.type}");
                    Debug.Log($"{monster.gold}");
                }
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
}
