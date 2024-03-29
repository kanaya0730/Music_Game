using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Template.Constant;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button[] _buuton;

    [SerializeField] private Button _homeButton;

    public void Start()
    {
        if (_homeButton == null)
        {
            foreach (var selectButton in _buuton)
            {
                selectButton
                    .onClick
                    .AsObservable()
                    .Subscribe(_ => SceneChangeEffect.Instance.LoadScene(SceneName.GAMESCENE))
                    .AddTo(this);
            }   
        }

        _homeButton
            .onClick
            .AsObservable()
            .Subscribe(_ => SceneChangeEffect.Instance.LoadScene(SceneName.HOMESCENE))
            .AddTo(this);
    }
}