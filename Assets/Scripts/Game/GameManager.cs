using System;
using System.Collections;
using System.Collections.Generic;
using NoteEditor.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int SongID => _songID;
    public float NotesSpeed => _noteSpeed;

    public bool IsPlayed => _isPlayed;
    public float StartTime => _startTime;

    public int Score => _score;
    
    public int Perfect => _perfect;
    public int Great => _great;
    public int Bad => _bad;
    public int Miss => _miss;
    
    private int _songID;
    
    [SerializeField]
    private float _noteSpeed;

    [SerializeField]
    private bool _isPlayed;
    
    [SerializeField]
    private float _startTime;

    [SerializeField] 
    private int _score;
    
    [SerializeField]
    private int _perfect;
    
    [SerializeField]
    private int _great;
    
    [SerializeField]
    private int _bad;
    
    [SerializeField]
    private int _miss;
    
    [SerializeField] 
    private GameScorePresenter _gameScorePresenter;

    private void OnEnable()
    {
        _songID = PlayerPrefs.GetInt("SONG_ID");
    }

    private void Start()
    {
        _score = 0;
        
        this.UpdateAsObservable()
            .Subscribe(_ => SetScore())
            .AddTo(this);   
    }
    

    private void SetScore()
    {
        _score = _gameScorePresenter.GameScoreModel.Score.Value;
    }

    public void AddPerfect()
    {
        _perfect++;
        _gameScorePresenter.GameScoreModel.AddCombo();
    }
    
    public void AddGreat()
    {
        _great++;
        _gameScorePresenter.GameScoreModel.AddCombo();
    }
    
    public void AddBad()
    {
        _bad++;
    }
    
    public void AddMiss()
    {
        _miss++;
        _gameScorePresenter.GameScoreModel.DiscontinuedCombo();
    }
    
    public void SetPlayed(bool value)
    {
        _isPlayed = value;
    }

    public void SetStartTime()
    {
        _startTime = Time.time;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("SCORE", _score);
        
        PlayerPrefs.SetInt("PERFECT", _perfect);
        PlayerPrefs.SetInt("GREAT", _great);
        PlayerPrefs.SetInt("BAD", _bad);
        PlayerPrefs.SetInt("MISS", _miss);

        PlayerPrefs.SetInt("SONG_ID", _songID);
        PlayerPrefs.Save();
        
    }
}