using System;
using System.Collections;
using System.Collections.Generic;
using NoteEditor.Utility;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class NotesMove : MonoBehaviour,IRemoved
{
    private void Start()
    {   
        this.FixedUpdateAsObservable()
            .Subscribe(_ => Move())
            .AddTo(this);
    }

    private void Move()
    {
        if (GameManager.Instance.IsPlayed)
            transform.position -= transform.forward * GameManager.Instance.NotesSpeed * Time.deltaTime;
    }

    public void Delete(bool value) => gameObject.SetActive(value);
}