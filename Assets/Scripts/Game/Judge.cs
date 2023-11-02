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
    private GameObject[] MessageObj; //プレイヤーに判定を伝えるゲームオブジェクト

    [SerializeField]
    private NotesGenerator notesGenerator; //スクリプト「notesGenerator」を入れる変数

    [SerializeField] 
    private GameScorePresenter _gameScorePresenter;

    [SerializeField] 
    private EffectManager _effectManager;

    [SerializeField]
    private MusicManager _musicManager;

    private const float LANE_WIDTH = 1.0f;　//レーンの太さ( = ノーツの太さ )
    private const int HALF_LANE_NUM = 4;

    void Start()
    {
        this.FixedUpdateAsObservable()
            .Subscribe(_ => MissCheck())
            .AddTo(this);
    }

    private void MissCheck()
    {
        if (!GameManager.Instance.IsPlayed) return;

        //本来ノーツをたたくべき時間から0.3秒たっても入力がなかった場合
        if (Time.time > notesGenerator.NotesTime[0] + 0.2f + GameManager.Instance.StartTime)
        {
            Message(3, notesGenerator.NotesLane[0]);
            DeleteData(0);
            Debug.Log("Miss");
            GameManager.Instance.AddMiss();
            _gameScorePresenter.GameScoreModel.Miss(40);
        }
    }

    public void Judging(int num)
    {
        if (!GameManager.Instance.IsPlayed) return;

        if (notesGenerator.NotesLane[0] == num) //押されたボタンはレーンの番号とあっているか？
            Judgement(GetABS(Time.time - (notesGenerator.NotesTime[0] + GameManager.Instance.StartTime)), 0, num);

        else if (notesGenerator.NotesLane[1] == num)
            Judgement(GetABS(Time.time - (notesGenerator.NotesTime[1] + GameManager.Instance.StartTime)), 1, num);
    }

    void Judgement(float timeLag, int numOffset, int num)
    {
        if (timeLag <= 0.1f) //本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            _musicManager.PlaySound();
            _effectManager.PlayTapEffect(num);
            Message(0, num);
            GameManager.Instance.AddPerfect();
            _gameScorePresenter.GameScoreModel.AddScore(1000);
            DeleteData(numOffset);
        }
        else if (timeLag <= 0.15f) //本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            _musicManager.PlaySound();
            _effectManager.PlayTapEffect(num);
            Message(1, num);
            GameManager.Instance.AddGreat();
            _gameScorePresenter.GameScoreModel.AddScore(500);
            DeleteData(numOffset);
        }
        else if (timeLag <= 0.2f) //本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
        {
            _musicManager.PlaySound();
            _effectManager.PlayTapEffect(num);
            Message(2, num);
            GameManager.Instance.AddBad();
            DeleteData(numOffset);
        }
    }

    float GetABS(float num) //引数の絶対値を返す関数
    {
        return num >= 0 ? num : -num;
    }

    void DeleteData(int numOffset) //すでにたたいたノーツを削除する関数
    {
        notesGenerator.NotesTime.RemoveAt(numOffset);
        notesGenerator.NotesLane.RemoveAt(numOffset);
        notesGenerator.NotesType.RemoveAt(numOffset);

        if (notesGenerator.NotesObj[numOffset].TryGetComponent(out IRemoved removed))
            removed.Delete(false);

        notesGenerator.NotesObj.RemoveAt(numOffset);
    }

    /// <summary>判定を表示する</summary>
    /// <param name="judge"></param>
    void Message(int judge, float num)
    {
        Instantiate(MessageObj[judge], new Vector3(-HALF_LANE_NUM * LANE_WIDTH + LANE_WIDTH * num + LANE_WIDTH / 2, 0.0f, 1.2f), Quaternion.Euler(45, 0, 0));
    }
}