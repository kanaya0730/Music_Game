using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class NotesManager : MonoBehaviour
{
[Serializable]
public struct Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Notes[] notes;
}

[Serializable]
public struct Notes
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

    public int NoteNum => _noteNum;
    public List<int> LaneNum => _laneNum;
    public List<int> NoteType => _noteType;
    public List<float> NotesTime => _notesTime;
    public List<GameObject> NotesObj => _notesObj;

    private int _noteNum;

    [SerializeField]
    private List<int> _laneNum = new();
    
    [SerializeField]
    private List<int> _noteType = new();
    
    [SerializeField]
    private List<float> _notesTime = new();
    
    [SerializeField]
    private List<GameObject> _notesObj = new();
    
    [SerializeField]
    private GameObject _noteObj;
    
    [SerializeField] 
    private List<TextAsset> _songName = new();
    
    private const int MINUTE = 60;
    
    void Start()
    {
        _noteNum = 0;
        Load(_songName[GameManager.Instance.SongID]);
    }

    private void Load(TextAsset songName)
    {
        string inputString = songName.ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        
        _noteNum = inputJson.notes.Length;
        
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            //ノーツの生成位置を計算
            float interval = MINUTE / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = interval * inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            
            //ノーツの情報を追加
            _notesTime.Add(time);
            _laneNum.Add(inputJson.notes[i].block);
            _noteType.Add(inputJson.notes[i].type);

            float z = _notesTime[i] * GameManager.Instance.NotesSpeed;
            
            var obj = Instantiate(_noteObj);
            obj.transform.position = new Vector3(inputJson.notes[i].block * 0.8f, 0.02f, z);
            
            _notesObj.Add(obj);
        }
    }
}