using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RemoveJudgeText : MonoBehaviour
{
    public async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        gameObject.SetActive(false);
    }
}
