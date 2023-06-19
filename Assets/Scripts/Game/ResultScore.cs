using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _songNameText;
    
    [SerializeField]
    [Header("パーフェクト、グレイト、グッド、バッド、ミス")]
    private Text[] _judgementText;

    [SerializeField] 
    private List<String> _songName = new();
    
    private void Start()
    {
        _scoreText.text = PlayerPrefs.GetInt("SCORE").ToString();
        _songNameText.text = _songName[PlayerPrefs.GetInt("SONG_ID")];
        
        _judgementText[0].text = PlayerPrefs.GetInt("PERFECT").ToString();
        _judgementText[1].text = PlayerPrefs.GetInt("GREAT").ToString();
        _judgementText[2].text = PlayerPrefs.GetInt("BAD").ToString();
        _judgementText[3].text = PlayerPrefs.GetInt("MISS").ToString();
        
        // _scoreText.text = ScoreData.Instance.Score.ToString();
        // _songNameText.text = ScoreData.Instance.SongID.ToString();
        //
        // _judgementText[0].text = ScoreData.Instance.Perfect.ToString();
        // _judgementText[1].text = ScoreData.Instance.Great.ToString();
        // _judgementText[2].text = ScoreData.Instance.Bad.ToString();
        // _judgementText[3].text = ScoreData.Instance.Miss.ToString();
    }
}
