using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static float BGMVolume = 1f;
    public static float SEVolume = 1f;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI BgmText;
    [SerializeField] private Slider seSlider;
    [SerializeField] private TextMeshProUGUI SeText;
    void Start()
    {
        BGMVolume =
            PlayerPrefs.GetFloat("BGMVolume", 1f);

        SEVolume =
            PlayerPrefs.GetFloat("SEVolume", 1f);

        bgmSlider.value = BGMVolume;
        seSlider.value = SEVolume;
    }

    void Update()
    {
        BgmText.color =
            Color.Lerp(
                Color.green,
                Color.red,
                bgmSlider.value);

        SeText.color =
            Color.Lerp(
                Color.green,
                Color.red,
                seSlider.value);
    }

    public void ChangeBGMVolume()
    {
        SoundManager.BGMVolume =
            bgmSlider.value;

        PlayerPrefs.SetFloat(
            "BGMVolume",
            SoundManager.BGMVolume);
    }

    public void ChangeSEVolume()
    {
        SoundManager.SEVolume =
            seSlider.value;


        PlayerPrefs.SetFloat(
            "SEVolume",
            SoundManager.SEVolume);
    }
}