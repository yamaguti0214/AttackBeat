using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider bgmSlider;
    public Slider seSlider;

    void Start()
    {
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    public void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }

    public void SetSEVolume(float value)
    {
        audioMixer.SetFloat("SEVolume", Mathf.Log10(value) * 20);
    }
}