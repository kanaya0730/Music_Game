using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

public class GameScorePresenter : MonoBehaviour
{
    [SerializeField] 
    private GameScoreModel _gameScoreModel;
    
    [SerializeField]
    private GameScoreView _gameScoreView;

    private void Start()
    {
        _gameScoreModel.Hp
            .Subscribe(x =>
            {
                _gameScoreView.SetHpValue(x);
            })
            .AddTo(this);

        _gameScoreModel.Score
            .Subscribe(x =>
            {
                _gameScoreView.SetScoreValue(x);
            })
            .AddTo(this);

        _gameScoreModel.Combo
            .Subscribe(x =>
            {
                _gameScoreView.SetComboValue(x);
            })
            .AddTo(this);
    }
}
