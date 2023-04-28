using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;

public class LightSetting : MonoBehaviour
{
    [SerializeField]
    [Header("スピード")]
    private float _speed = 3;
    
    [SerializeField] 
    private int _num = 0;
    
    private Renderer _rend;
    
    private float _alfa = 0;
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }
    void Update()
    {

        if (!(_rend.material.color.a <= 0))
        {
            _rend.material.color = new Color(_rend.material.color.r, _rend.material.color.r, _rend.material.color.r, _alfa);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            ColorChange(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ColorChange(1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ColorChange(2);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ColorChange(3);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ColorChange(4);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ColorChange(5);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ColorChange(6);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ColorChange(7);
        }
        _alfa -= _speed * Time.deltaTime;
    }

    public void ColorChange(int num)
    {
        if (_num == num)
        {
            _alfa = 0.3f;
            _rend.material.color = new Color(_rend.material.color.r, _rend.material.color.g, _rend.material.color.b,_alfa);
        }
    }
}