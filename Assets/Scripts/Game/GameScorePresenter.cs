using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

public class GameScorePresenter : MonoBehaviour
{
    public GameScoreModel GameScoreModel { get; private set; } = new ();

    [SerializeField] 
    private GameScoreView _gameScoreView;

    private void Start()
    {
        GameScoreModel.Hp
            .Subscribe(x => _gameScoreView.SetHpValue(x))
            .AddTo(this);

        GameScoreModel.Score
            .Subscribe(x => _gameScoreView.SetScoreValue(x))
            .AddTo(this);

        GameScoreModel.Combo
            .Subscribe(x => _gameScoreView.SetComboValue(x))
            .AddTo(this);
    }
}