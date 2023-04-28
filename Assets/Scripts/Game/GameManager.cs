using System;
using System.Collections;
using System.Collections.Generic;
using NoteEditor.Utility;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public float MaxScore => _maxScore;
    public float RatioScore => _ratioScore;
    
    public int SongID => _songID;
    public float NotesSpeed => _noteSpeed;

    public bool IsPlayed => _isPlayed;
    public float StartTime => _startTime;

    public int Combo => _combo;
    public int Score => _score;
    
    public int Perfect => _perfect;
    public int Great => _great;
    public int Bad => _bad;
    public int Miss => _miss;
    
    private float _maxScore;//new!!
    private float _ratioScore;//new!!

    private int _songID;
    
    [SerializeField]
    private float _noteSpeed;

    [SerializeField]
    private bool _isPlayed;
    
    [SerializeField]
    private float _startTime;

    [SerializeField]
    private int _combo;
    
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

    public void AddScore(int score)
    {
        _score += score;
    }

    public void AddPerfect()
    {
        _perfect++;
        AddCombo();
    }
    
    public void AddGreat()
    {
        _great++;
        AddCombo();
    }
    
    public void AddBad()
    {
        _bad++;
        DiscontinuedCombo();
    }
    
    public void AddMiss()
    {
        _miss++;
        DiscontinuedCombo();
    }

    private void AddCombo()
    {
        _combo++;
    }

    private void DiscontinuedCombo()
    {
        _combo = 0;
    }

    public void AddRatioScore(int num)
    {
        _ratioScore += num;
    }

    public void MaxSetResultScore()
    {
        _score = (int)Math.Round(1000000 * Math.Floor(_ratioScore / _maxScore * 1000000) / 1000000);
    }

    public void SetMaxScore(int noteNum , int num)
    {
        _maxScore = noteNum * num;
    }

    public void SetPlayed(bool value)
    {
        _isPlayed = value;
    }

    public void SetStartTime()
    {
        _startTime = Time.time;
    }
}