using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameScoreModel : MonoBehaviour
{
    public float MaxHp => _maxHp;

    public int MaxScore => _maxScore;
    public IReadOnlyReactiveProperty<float> Hp => _hp;
    private readonly FloatReactiveProperty _hp = new(1000);
    private readonly float _maxHp = 1000;

    public IReadOnlyReactiveProperty<int> Score => _score;
    private readonly IntReactiveProperty _score = new(0);
    private readonly int _maxScore = 0;
    
    public IReadOnlyReactiveProperty<int> Combo => _combo;
    private readonly IntReactiveProperty _combo = new(0);

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Miss(float damage)
    {
        _hp.Value -= damage;
    }
    
    public void AddCombo()
    {
        _combo.Value++;
    }
    
    public void DiscontinuedCombo()
    {
        _combo.Value = 0;
    }
    
    public void AddScore(int score)
    {
        _score.Value += score;
    }

    private void OnDestroy()
    {
        _hp.Dispose();
        _combo.Dispose();
        _score.Dispose();
    }
}
