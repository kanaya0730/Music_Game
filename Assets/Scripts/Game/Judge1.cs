using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UniRx;
using UniRx.Triggers;

public class Judge1 : MonoBehaviour
{
    //変数の宣言
    [SerializeField]
    private GameObject[] MessageObj;//プレイヤーに判定を伝えるゲームオブジェクト
    
    [SerializeField] 
    private NotesManager _notesManager;//スクリプト「_notesManager」を入れる変数

    [SerializeField] 
    private GameManager _gameManager;
    
    [SerializeField] 
    TextMeshProUGUI comboText;//new!!
    [SerializeField]
    TextMeshProUGUI scoreText;//new!!
    
    [SerializeField]
    AudioClip _hitSound;

    AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        this.UpdateAsObservable()
            .Subscribe(_ => Judging())
            .AddTo(this);
    }
    void Judging()
    { 
        if (_gameManager.IsPlayed)
        {
            if (Input.GetKeyDown(KeyCode.D))//〇キーが押されたとき
            {
                if (_notesManager._laneNum[0] == 0)//押されたボタンはレーンの番号とあっているか？
                {
                    Judgement(GetABS(Time.time - (_notesManager._notesTime[0] + _gameManager.StartTime)), 0);
                }
                else
                {
                    if (_notesManager._laneNum[1] == 0)
                    {
                        Judgement(GetABS(Time.time - (_notesManager._notesTime[1] + _gameManager.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_notesManager._laneNum[0] == 1)
                {
                    Judgement(GetABS(Time.time - (_notesManager._notesTime[0] + _gameManager.StartTime)),0);
                }
                else
                {
                    if (_notesManager._laneNum[1] == 1)
                    {
                        Judgement(GetABS(Time.time - (_notesManager._notesTime[1] + _gameManager.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (_notesManager._laneNum[0] == 2)
                {
                    Judgement(GetABS(Time.time - (_notesManager._notesTime[0] + _gameManager.StartTime)),0);
                }
                else
                {
                    if (_notesManager._laneNum[1] == 2)
                    {
                        Judgement(GetABS(Time.time - (_notesManager._notesTime[1] + _gameManager.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (_notesManager._laneNum[0] == 3)
                {
                    Judgement(GetABS(Time.time - (_notesManager._notesTime[0] + _gameManager.StartTime)),0);
                }
                else
                {
                    if (_notesManager._laneNum[1] == 3)
                    {
                        Judgement(GetABS(Time.time - (_notesManager._notesTime[1] + _gameManager.StartTime)), 1);
                    }
                }
            }

            if (Time.time > _notesManager._notesTime[0] + 0.2f + _gameManager.StartTime)//本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
            {
                Message(3);
                DeleteData(0);
                Debug.Log("Miss");
                _gameManager.AddMiss();
                //ミス
            }
        }
    }
    void Judgement(float timeLag,int numOffset)
    {
        _audioSource.PlayOneShot(_hitSound);
        if (timeLag <= 0.05)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            Debug.Log("Perfect");
            Message(0);
            _gameManager.AddPerfect();
            _gameManager.AddRatioScore(5);
            DeleteData(numOffset);
        }
        else
        {
            if (timeLag <= 0.08)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
            {
                Debug.Log("Great");
                Message(1);
                _gameManager.AddGreat();
                _gameManager.AddRatioScore(3);
                DeleteData(numOffset);
            }
            else
            {
                if (timeLag <= 0.10)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
                {
                    Debug.Log("Bad");
                    Message(2);
                    _gameManager.AddBad();
                    _gameManager.AddRatioScore(1);
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
        _notesManager._notesTime.RemoveAt(numOffset);
        _notesManager._laneNum.RemoveAt(numOffset);
        _notesManager._noteType.RemoveAt(numOffset); 
        _gameManager.MaxSetResultScore();
        comboText.text = _gameManager.Combo.ToString();//new!!
        scoreText.text = _gameManager.Score.ToString();//new!!
    }

    void Message(int judge)//判定を表示する
    {
        Instantiate(MessageObj[judge],new Vector3(_notesManager._laneNum[0]-1.5f,0.76f,0.9f),Quaternion.Euler(45,0,0));
    }
}