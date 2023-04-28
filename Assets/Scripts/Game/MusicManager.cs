using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MusicManager : MonoBehaviour
{
    AudioSource _audioSource;
    
    [SerializeField]
    private List<AudioClip> _music = new();

    [SerializeField] 
    private GameManager _gameManager;

    [SerializeField] 
    private VideoManager _videoManager;
    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
        _gameManager.SetPlayed(false);

        this.UpdateAsObservable()
            .Subscribe(_ => WaitStart())
            .AddTo(this);
    }
    
    //開始待ち
    void WaitStart()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !_gameManager.IsPlayed )
        {
            _audioSource.PlayOneShot(_music[0]);
            _gameManager.SetPlayed(true);
            _gameManager.SetStartTime();
            _videoManager.VPControl();
        }
    }
}