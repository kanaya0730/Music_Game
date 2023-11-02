using System;
using System.Collections.Generic;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
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
    public Note[] notes;
}
[Serializable]
public struct Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

    public int NoteNum => _noteNum;
    public List<int> NotesLane => _notesLane;
    public List<int> NotesType => _notesType;
    public List<float> NotesTime => _notesTime;
    public List<GameObject> NotesObj => _notesObj;

    private int _noteNum;

    [SerializeField]
    private List<int> _notesLane = new();
    
    [SerializeField]
    private List<int> _notesType = new();
    
    [SerializeField]
    private List<float> _notesTime = new();
    
    [SerializeField]
    private List<GameObject> _notesObj = new();
    
    [SerializeField]
    private GameObject _noteObj;

    [SerializeField]
    private List<GameObject> _longNotesObj = new();

    [SerializeField]
    private GameObject _longNoteObj;

    [SerializeField] 
    private List<float> _longNotesTime;

    [SerializeField]
    private List<int> _longNotesLane;

    [SerializeField] 
    private List<TextAsset> _songName = new();
    
    private const int MINUTE = 60;

    private const float LANE_WIDTH = 1.0f;
    private const int HALF_LANE_NUM = 4;


    void Start()
    {
        Load(_songName[GameManager.Instance.SongID]);
    }

    private void Load(TextAsset songName)
    {
        _noteNum = 0;
        
        var inputString = songName.ToString();
        var inputJson = JsonUtility.FromJson<Data>(inputString);
        
        _noteNum = inputJson.notes.Length;
        Debug.Log(_noteNum);

        for(int k = 0; k < _noteNum; k++)
        {
            //ノーツの生成位置を計算
            var interval = MINUTE / (inputJson.BPM * (float)inputJson.notes[k].LPB);
            var beatSec = interval * inputJson.notes[k].LPB;
            var time = (beatSec * inputJson.notes[k].num / inputJson.notes[k].LPB) + inputJson.offset * 0.01f;

            //ノーツの情報を追加
            _notesTime.Add(time);
            _notesLane.Add(inputJson.notes[k].block);
            _notesType.Add(inputJson.notes[k].type);

            if (_notesType[k] == 1)
            {
                var obj = Instantiate(_noteObj);
                var z = _notesTime[k] * GameManager.Instance.NotesSpeed;

                obj.transform.position = new Vector3(-HALF_LANE_NUM * LANE_WIDTH + LANE_WIDTH * inputJson.notes[k].block + LANE_WIDTH / 2, 0.02f, z);
                _notesObj.Add(obj);
            }

            if (_notesType[k] == 2)
            {
                //ロングノーツの生成位置を計算
                var interval_0 = MINUTE / (inputJson.BPM * (float)inputJson.notes[k].LPB);
                var beatSec_0 = interval_0 * inputJson.notes[k].LPB;
                var time_0 = (beatSec_0 * inputJson.notes[k].num / inputJson.notes[k].LPB) + inputJson.offset * 0.01f;

                //ロングノーツの情報を追加
                _longNotesTime.Add(time_0);
                _longNotesLane.Add(inputJson.notes[k].block);

                for (int j = 0; j < inputJson.notes[k].notes.Length; j++)
                {
                    //ノーツの生成位置を計算
                    var interval1 = MINUTE / (inputJson.BPM * (float)inputJson.notes[k].notes[j].LPB);
                    var beatSec1 = interval1 * inputJson.notes[k].notes[j].LPB;
                    var time1 = (beatSec1 * inputJson.notes[k].notes[j].num / inputJson.notes[k].notes[j].LPB) + inputJson.offset * 0.01f;

                    _longNotesTime.Add(time1);
                    _longNotesLane.Add(inputJson.notes[k].notes[j].block);
                }

            }
        }
        for (int n = 0; n < _longNotesLane.Count; n++)
        {
            LongNotes(_longNotesLane[n], _longNotesLane[n + 1], _longNotesTime[n] * GameManager.Instance.NotesSpeed, _longNotesTime[n + 1] * GameManager.Instance.NotesSpeed);
        }
    }

    private void LongNotes(int startLane, int endLane, float starTime, float endTime)
    {
        //位置座標を求める。
        var startPos = new Vector3(-HALF_LANE_NUM * LANE_WIDTH + LANE_WIDTH * startLane + LANE_WIDTH / 2, 0.02f, starTime);
        var endPos = new Vector3(-HALF_LANE_NUM * LANE_WIDTH + LANE_WIDTH * endLane + LANE_WIDTH / 2, 0.02f, endTime);

        GameObject testNotes = Instantiate(_longNoteObj);
        testNotes.AddComponent<MeshFilter>();
        testNotes.AddComponent<MeshRenderer>();

        //ロングノーツ生成
        Generate(startPos, endPos, testNotes);
    }

    void Generate(Vector3 sPos, Vector3 ePos, GameObject notesObj)
    {
        Mesh mesh = new Mesh();
        notesObj.GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[4];

        vertices[0] = sPos + new Vector3(-LANE_WIDTH / 2, 0, 0);//始点の左端
        vertices[1] = sPos + new Vector3(LANE_WIDTH / 2, 0, 0); //始点の右端
        vertices[2] = ePos + new Vector3(-LANE_WIDTH / 2, 0, 0); //終点の左端
        vertices[3] = ePos + new Vector3(LANE_WIDTH / 2, 0, 0); //終点の右端

        var triangles = new int[6] { 0, 2, 1, 3, 1, 2 };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        var sObj = Instantiate(_noteObj);
        sObj.transform.position = sPos;
        _longNotesObj.Add(sObj);

        var eObj = Instantiate(_noteObj);
        eObj.transform.position = ePos;
        _longNotesObj.Add(eObj);
    }
}