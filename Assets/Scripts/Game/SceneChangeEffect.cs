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
        _backGround = Instantiate(_backGround);
        _backGround.transform.position = transform.position;
        _backGround.transform.SetParent(_canvasCenter);


        _effect = Instantiate(_effect);
        _effect.transform.position = transform.position;
        _effect.transform.SetParent(_backGround.transform);

        DontDestroyOnLoad(this.gameObject);
        _canvasCenter.gameObject.SetActive(false);
    }

    private async UniTask ZoomIn()
    {
        _canvasCenter.gameObject.SetActive(true);
        await _effect.transform.DOScale(new Vector3(10,10,10), _zoomTime);
        ZoomOut();
    }

    private async void ZoomOut()
    {
        await _effect.transform.DOScale(new Vector3(0,0,0), _zoomTime);
        _canvasCenter.gameObject.SetActive(false);
    }
    
    public async void LoadScene(string name)
    {
        await ZoomIn();
        SceneManager.LoadScene(name);
    }
}
