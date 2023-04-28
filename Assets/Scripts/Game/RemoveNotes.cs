using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNotes : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("衝突した");
        if (other.gameObject.TryGetComponent(out IRemeved removed))
        {
            removed.Delete(false);
        }
    }
}
