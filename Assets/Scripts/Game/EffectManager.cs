using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _tapEffect;

    public void PlayTapEffect(int num)
    {
        _tapEffect[num].Play();
    }
}
