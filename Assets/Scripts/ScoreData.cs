using System;
using System.Collections;
using System.Collections.Generic;
using NoteEditor.Utility;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


[Serializable]
public class ResultData
{
    public int _score;
    public int _perfect;
    public int _great;
    public int _bad;
    public int _miss;
    
    public int _songID;
}

public class ScoreData : SingletonMonoBehaviour<ScoreData>
{
    public int Score => _score;
    
    public int Perfect => _perfect;
    public int Great => _great;
    public int Bad => _bad;
    public int Miss => _miss;
    
    public int SongID => _songID;
    
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
    
    private int _songID;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetValue(ResultData resultData)
    {
        _score = resultData._score;
        
        _perfect = resultData._perfect;
        _great = resultData._great;
        _bad = resultData._bad;
        _miss = resultData._miss;

        _songID = resultData._songID;
    }
}
