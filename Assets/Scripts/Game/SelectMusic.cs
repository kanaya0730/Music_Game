using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusic : MonoBehaviour
{
    public int ID => _id;
    
    private int _id;

    public void Select(int num)
    {
        _id = num;
        SaveID();
    }


    private void SaveID()
    {
        PlayerPrefs.SetInt("SONG_ID", _id);
        PlayerPrefs.Save();
    }
}
