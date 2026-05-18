using System.Collections;
using System.Collections.Generic;
using System.Timers;
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
    public static bool CountDownEnd = false;
    private float CurrentTimer;

    public static bool SoundEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        ESCButton.Pause = true;

        BGMSound_public = BGMSound;
        SESound_public = SESound;

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ESCButton.Pause)
        {
            Debug.Log("PAUSE");
        }

        BGMSound.volume =
            SoundManager.BGMVolume;

        if (firstCountDown)
        {
            CurrentTimer += Time.unscaledDeltaTime;
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

    public float CountDown(float CountTime)
    {
        if (CountDownEnd)
            CountDownEnd = false;

        if (CountTime <= 1f)
        {
            CountDownText.text = ("----- 5 -----");
        }
        else if (CountTime < 2f)
        {
            CountDownText.text = ("---- 4 ----");
            CountDownText.color = new Color(0f, 1f, 0f);
        }
        else if (CountTime < 3f)
        {
            CountDownText.text = ("--- 3 ---");
            CountDownText.color = new Color(1f, 1f, 0f);
        }
        else if (CountTime < 4f)
        {
            CountDownText.text = ("-- 2 --");
            CountDownText.color = new Color(1f, 0.5f, 0f);
        }
        else if (CountTime < 5f)
        {
            CountDownText.text = ("- 1 -");
            CountDownText.color = new Color(1f, 0f, 0f);
        }
        else if (CountTime <= 6f)
        {
            ESCButton.Pause = false;
            CountDownText.text = ("");
            if (firstCountDown)
            {
                BGMPlay();

                Time.timeScale = 1f;
            }
            else
            {
                BGMSound.UnPause();
            }
            CurrentTimer = 0f;
            firstCountDown = false;
            CountDownEnd = true;
        }
        return 0;
    }
}
