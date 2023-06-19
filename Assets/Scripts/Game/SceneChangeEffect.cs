using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NoteEditor.Utility;
using UnityEngine.SceneManagement;

public class SceneChangeEffect : SingletonMonoBehaviour<SceneChangeEffect>
{
    [SerializeField] 
    private Canvas _canvas;
    
    [SerializeField] 
    private Image _backGround;

    [SerializeField] 
    private Image _effect;

    [SerializeField]
    private float _zoomTime;

    [SerializeField] 
    private Transform _canvasCenter;
    
    public void Start()
    {
        _backGround = Instantiate (_backGround, transform.position, Quaternion.identity);
        _backGround.transform.parent = _canvasCenter;

        
        _effect = Instantiate (_effect, transform.position, Quaternion.identity);
        _effect.transform.parent = _backGround.transform;

        DontDestroyOnLoad(this.gameObject);
        _canvasCenter.gameObject.SetActive(false);
    }

    private async void ZoomIn()
    {
        _canvasCenter.gameObject.SetActive(true);
        _effect.gameObject.transform.DOScale(new Vector3(10,10,10), _zoomTime);
        await UniTask.Delay(TimeSpan.FromSeconds(_zoomTime));
        ZoomOut();
    }

    private async void ZoomOut()
    {
        _effect.gameObject.transform.DOScale(new Vector3(0,0,0), _zoomTime);
        await UniTask.Delay(TimeSpan.FromSeconds(_zoomTime));
        _canvasCenter.gameObject.SetActive(false);
    }
    
    public async void LoadScene(string name)
    {
        ZoomIn();
        await UniTask.Delay(TimeSpan.FromSeconds(_zoomTime));
        SceneManager.LoadScene(name);
    }
}
