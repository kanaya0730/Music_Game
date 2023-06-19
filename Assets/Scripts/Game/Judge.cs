using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UniRx;
using UniRx.Triggers;

public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField]
    private GameObject[] MessageObj;//プレイヤーに判定を伝えるゲームオブジェクト
    
    [SerializeField] 
    private NotesManager _notesManager;//スクリプト「_notesManager」を入れる変数

    [SerializeField]
    private GameScoreModel _gameScoreModel;

    [SerializeField] 
    private EffectManager _effectManager;

    [SerializeField] 
    private MusicManager _musicManager;

    void Start()
    {
        this.FixedUpdateAsObservable()
            .Subscribe(_ => MissCheck())
            .AddTo(this);
    }

    public void MissCheck()
    {
        if (GameManager.Instance.IsPlayed)
        {
            if (_notesManager.NotesTime != null)
            {
                if (Time.time > _notesManager.NotesTime[0] + 0.12f + GameManager.Instance.StartTime)//本来ノーツをたたくべき時間から0.18秒たっても入力がなかった場合
                {
                    Message(3);
                    DeleteData(0);
                    Debug.Log("Miss");
                    GameManager.Instance.AddMiss();
                    _gameScoreModel.Miss(40);
                }    
            }
        }
    }
    public void Judging(int num)
    {
        if (GameManager.Instance.IsPlayed)
        {
            if (_notesManager.LaneNum[0] == num)//押されたボタンはレーンの番号とあっているか？
            {
                Judgement(GetABS(Time.time - (_notesManager.NotesTime[0] + GameManager.Instance.StartTime)), 0, num);
            }
            else
            {
                if (_notesManager.LaneNum[1] == num)
                {
                    Judgement(GetABS(Time.time - (_notesManager.NotesTime[1] + GameManager.Instance.StartTime)), 1, num);
                }
            }
        }
    }
    void Judgement(float timeLag,int numOffset, int num)
    {
        if (timeLag <= 0.07)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            _musicManager.PlaySound();
            _effectManager.PlayTapEffect(num);
            Message(0);
            GameManager.Instance.AddPerfect();
            _gameScoreModel.AddScore(1000);
            DeleteData(numOffset);
        }
        else
        {
            if (timeLag <= 0.09)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
            {
                _musicManager.PlaySound();
                _effectManager.PlayTapEffect(num);
                Message(1);
                GameManager.Instance.AddGreat();
                _gameScoreModel.AddScore(500);
                DeleteData(numOffset);
            }
            else
            {
                if (timeLag <= 0.12)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
                {
                    _musicManager.PlaySound();
                    _effectManager.PlayTapEffect(num);
                    Message(2);
                    GameManager.Instance.AddBad();
                    DeleteData(numOffset);
                }
            }
        }
    }
    float GetABS(float num)//引数の絶対値を返す関数
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }
    void DeleteData(int numOffset)//すでにたたいたノーツを削除する関数
    {
        _notesManager.NotesTime.RemoveAt(numOffset);
        _notesManager.LaneNum.RemoveAt(numOffset);
        _notesManager.NoteType.RemoveAt(numOffset);
        
        if (_notesManager.NotesObj[numOffset].TryGetComponent(out IRemeved removed))
        {
            removed.Delete(false);
        }
        
        _notesManager.NotesObj.RemoveAt(numOffset);
    }

    /// <summary>判定を表示する</summary>
    /// <param name="judge"></param>
    void Message(int judge)
    {
        Instantiate(MessageObj[judge],new Vector3(_notesManager.LaneNum[0] * 0.8f, 0.0f, 1.2f),Quaternion.Euler(45,0,0));
    }
}