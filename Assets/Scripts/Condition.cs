using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Condition : MonoBehaviour
{
    [SerializeField]
    private byte _player = 0b0000_0000;
    
    [SerializeField]
    private byte _poison = 0b0000_0001;
    
    [SerializeField]
    private byte _paralysis = 0b0000_0010;
    
    [SerializeField]
    private byte _sleep = 0b0000_0100;
    
    [SerializeField]
    private byte _silence = 0b0000_1000;

    [SerializeField] 
    private byte _moudoku = 0b0001_0000;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_player >= _poison)
            {
                _player |= _moudoku;
                Debug.Log($"poison => condition{Convert.ToString(_player, 2/*二進数で表示*/).PadLeft(4/*四桁で*/,'0'/*0を補う*/)}");
            }
            else
            {
                _player |= _poison;
            }

            Debug.Log($"poison => condition{Convert.ToString(_player, 2/*二進数で表示*/).PadLeft(4/*四桁で*/,'0'/*0を補う*/)}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _player |= _paralysis;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            _player |= _sleep;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            _player |= _silence;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }

        //直す
        if (Input.GetKeyDown(KeyCode.F))
        {  
            _player &= (byte)~_poison;
            _player &= (byte)~_moudoku;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            _player &= (byte)~_paralysis;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            _player &= (byte)~_sleep;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _player &= (byte)~_silence;
            Debug.Log($"poison => condition{Convert.ToString(_player, 2).PadLeft(4,'0')}");
        }

    }
}
