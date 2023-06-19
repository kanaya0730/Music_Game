using System.Collections;
using System.Collections.Generic;
using NoteEditor.Utility;
using UnityEngine;
using UnityEngine.Serialization;

public class JsonSave : MonoBehaviour
{
    [SerializeField]
    List<string> _savePath = new();
    
    private void Start()
    {
        
        for (int i = 0; i < _savePath.Count; i++)
        {
            
            ResultData resultData = JsonSaveManager<ResultData>.Load(_savePath[i]);

            if (resultData == null)
            {
                resultData = new()
                {
                    _score = 0,
                    _perfect = 0,
                    _great = 0,
                    _bad = 0,
                    _miss = 0,
                    _songID = i,
                };
            }
            
            ScoreData.Instance.SetValue(resultData);
        }
        
    }

    public void OverWriteSave(int id)
    {
        ResultData resultData = new()
        {
            _score = ScoreData.Instance.Score,
            _perfect = ScoreData.Instance.Perfect,
            _great = ScoreData.Instance.Great,
            _bad = ScoreData.Instance.Bad,
            _miss = ScoreData.Instance.Miss,
            _songID = ScoreData.Instance.SongID,
        };
        
        JsonSaveManager<ResultData>.Save(resultData, _savePath[id]);
    }
}
