using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private List<VideoClip> _videoClip = new();
    
    [SerializeField]
    private GameObject _screen;
    
    void Start()
    {
        var videoPlayer = _screen.AddComponent<VideoPlayer>();
        
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = _videoClip[0];
        
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
    }
 
    public void VPControl()
    {
        var videoPlayer = _screen.GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }
}
