using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameScoreView : MonoBehaviour
{
    [SerializeField] 
    private Slider _hpSlider;

    [SerializeField]
    private Text _hpText;
    
    [SerializeField] 
    private Slider _scoreSlider;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _comboText;
    
    public void SetHpValue(float value)
    {
        DOTween.To(() => _hpSlider.value,
            n => _hpSlider.value = n,
            value, 1.0f);

        _hpText.text = value.ToString();
    }

    public void SetScoreValue(float value)
    { 
        DOTween.To(() => _scoreSlider.value,
            n => _scoreSlider.value = n,
            value,
            duration: 1.0f);

        _scoreText.text = value.ToString();
    }
    
    public void SetComboValue(int value)
    {
        _comboText.text = value.ToString();
    }
}
