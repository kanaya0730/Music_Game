using System;
using System.Collections;
using System.Collections.Generic;
using NoteEditor.Utility;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using NoteEditor.Utility;
public class NotesMove : MonoBehaviour,IRemeved
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
        {
            transform.position -= transform.forward * Time.deltaTime * GameManager.Instance.NotesSpeed;
        }
    }

    public void Delete(bool value)
    {
        gameObject.SetActive(value);
    }
}