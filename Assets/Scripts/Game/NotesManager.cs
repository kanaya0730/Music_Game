using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Notes[] notes;
}

[Serializable]
public class Notes
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    public int NoteNum => _noteNum;
    
    public List<int> LaneNum => _laneNum;
    public List<int> NoteType => _noteType;
    public List<float> NotesTime => _notesTime;
    public List<GameObject> NotesObj => _notesObj;

    private int _noteNum;

    public List<int> _laneNum = new List<int>();
    public List<int> _noteType = new List<int>();
    public List<float> _notesTime = new List<float>();
    public List<GameObject> _notesObj = new List<GameObject>();
    
    [SerializeField]
    private GameObject _noteObj;

    [SerializeField] 
    private GameManager _gameManager;

    [SerializeField] 
    private List<TextAsset> _songName = new();
    
    private const int MINUTE = 60;
    void OnEnable()
    {
        _noteNum = 0;
        Load(_songName[0]);
    }

    private void Load(TextAsset songName)
    {
        string inputString = songName.ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        
        _noteNum = inputJson.notes.Length;
        _gameManager.SetMaxScore(_noteNum, 5);

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float interval = MINUTE / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = interval * inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            _notesTime.Add(time);
            _laneNum.Add(inputJson.notes[i].block);
            _noteType.Add(inputJson.notes[i].type);

            float z = _notesTime[i] * _gameManager.NotesSpeed;
            _notesObj.Add(Instantiate(_noteObj, new Vector3(inputJson.notes[i].block * 0.5f, 0.02f, z), Quaternion.identity));
        }
    }
}