using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] AudioSource BGMSound;
    [SerializeField] AudioSource SESound;
    [SerializeField] TextMeshProUGUI CountDownText;

    public static AudioSource BGMSound_public;
    public static AudioSource SESound_public;

    private bool firstCountDown = true;
    private float CurrentTimer;

    public static bool SoundEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        BGMSound_public = BGMSound;
        SESound_public = SESound;
    }

    // Update is called once per frame
    void Update()
    {
        BGMSound.volume =
            SoundManager.BGMVolume;

        if (firstCountDown)
        {
            CurrentTimer += Time.deltaTime;
            CountDown(CurrentTimer);
        }

        if (!BGMSound.isPlaying && !SoundEnd && !firstCountDown)
        {
            SoundEnd = true;

            Debug.Log("ã»èIóπÅI");
        }

        if(ESCButton.Pause)
        {
            BGMSound.Pause();
            Debug.Log("hhhhhhhhhhh");
        }
    }

    public void BGMPlay()
    {
        BGMSound.Play();
    }

    public void SEPlay()
    {
        SESound.volume =
            SoundManager.SEVolume;

        SESound.Play();
    }

    public float CountDown(float Time)
    {
        if (Time <= 1f)
        {
            CountDownText.text = ("----- 5 -----");
        }
        else if (Time < 2f)
        {
            CountDownText.text = ("---- 4 ----");
            CountDownText.color = new Color(0f, 1f, 0f);
        }
        else if (Time < 3f)
        {
            CountDownText.text = ("--- 3 ---");
            CountDownText.color = new Color(1f, 1f, 0f);
        }
        else if (Time < 4f)
        {
            CountDownText.text = ("-- 2 --");
            CountDownText.color = new Color(1f, 0.5f, 0f);
        }
        else if (Time < 5f)
        {
            CountDownText.text = ("- 1 -");
            CountDownText.color = new Color(1f, 0f, 0f);
        }
        else if (Time <= 6f)
        {
            CountDownText.text = ("");
            if (firstCountDown)
            {
                BGMPlay();
            }
            else
            {
                BGMSound.UnPause();
            }
            CurrentTimer = 0f;
            firstCountDown = false;
        }
        return 0;
    }
}
