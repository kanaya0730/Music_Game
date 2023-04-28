using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerCharacterData : MonoBehaviour
{
    
    async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        GetItem();
    }
    
    public void GetItem()
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
            {
                CatalogVersion = "Main",
                StoreId = "gold_store",
                ItemId = "Wizard",
                Price = 100,
                VirtualCurrency = "GD"
            }, 
            result =>
            {
                Debug.Log("アイテムの購入完了");
                CreatCharacter();
            },
            error => 
            { 
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    public void CreatCharacter()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
            {
                CatalogVersion = "Main",
                ItemId = "Wizard",
                CharacterName = "Wizard"
            }, 
            result =>
            {
                Debug.Log("キャラクター作成完了");
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
}
