using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    [SerializeField]
    private List<AudioClip> _music = new();
    
    [SerializeField] 
    private VideoManager _videoManager;
    
    [SerializeField]
    private AudioClip _hitSound;

    [SerializeField]
    private Text _countText;

    [SerializeField]
    private float _setTime;
    
    private float _time;
    
    void Start()
    {
        _time = _setTime;

        _audioSource ??= GetComponent<AudioSource>();   
        
        GameManager.Instance.SetPlayed(false);
        
        StartGame();

        this.UpdateAsObservable()
            .Subscribe(_ => TimeCount())
            .AddTo(this);
    }
    
    //開始待ち
    private async void StartGame()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_setTime));
        _audioSource.PlayOneShot(_music[GameManager.Instance.SongID]);
        _audioSource.volume = 0.7f;
        GameManager.Instance.SetPlayed(true);
        GameManager.Instance.SetStartTime();
        _videoManager.PlayVideo();
        _countText.enabled = false;
    }

    /// <summary>SE</summary>
    public void PlaySound() => _audioSource.PlayOneShot(_hitSound);

    private void TimeCount()
    {
        _countText.text = _time.ToString("f0");
        _time -= Time.deltaTime;
    }
}