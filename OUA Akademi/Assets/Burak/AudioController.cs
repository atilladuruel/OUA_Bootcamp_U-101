using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioController : MonoBehaviour
{
    //Ses ayarları
    public AudioSource backgroundSound;
    public Slider volumeSlider;
    public TextMeshProUGUI volumePercentageText;


    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }
    private void OnVolumeChanged(float value)
    {
        backgroundSound.volume = value;
    }
    public void UpdateVolumeText()
    {
        int volumePercentage = Mathf.RoundToInt(volumeSlider.value * 100f);
        volumePercentageText.text = "%" + volumePercentage.ToString();
    }
}
