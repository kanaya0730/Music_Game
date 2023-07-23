using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using DG.Tweening;
using Template.Constant;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    
    [SerializeField]
    private List<VideoClip> _videoClip = new();

    void Start()
    {
        _videoPlayer = Camera.main.gameObject.AddComponent<VideoPlayer>();
        
        _videoPlayer.playOnAwake = false;
        _videoPlayer.isLooping = false;
        _videoPlayer.source = VideoSource.VideoClip;
        _videoPlayer.clip = _videoClip[GameManager.Instance.SongID];
        
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
    }
 
    public void PlayVideo()
    {
        _videoPlayer.loopPointReached += LoopPointReached;
        _videoPlayer.Play();
    }

    public void PauseVideo()
    {
        _videoPlayer.Pause();
    }
    
    public void UnPauseVideo()
    {
        if(_videoPlayer.isPaused)
            _videoPlayer.Play();
    }
    
    private async void LoopPointReached(VideoPlayer vp)
    {
        // 動画再生完了時の処理
        Debug.Log("曲が終わった");
        GameManager.Instance.SaveScore();
        //クリア演出
        await UniTask.Delay(TimeSpan.FromSeconds(5f)); //n秒間待つ
        Debug.Log("シーン切り替え");
        SceneChangeEffect.Instance.LoadScene(SceneName.RESULTSCENE);
    }
    
}
