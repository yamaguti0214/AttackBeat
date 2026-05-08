using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    [SerializeField] TextMeshProUGUI CountDownText;

    private bool firstCountDown = false;
    private float CurrentTimer;

    public static bool SoundEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstCountDown)
        {
            CurrentTimer += Time.deltaTime;
            if (CurrentTimer <= 1f)
            {
                CountDownText.text = ("----- 5 -----");
            }
            else if(CurrentTimer <= 2f)
            {
                CountDownText.text = ("---- 4 ----");
                CountDownText.color = new Color(0f, 1f, 0f);
            }
            else if (CurrentTimer <= 3f)
            {
                CountDownText.text = ("--- 3 ---");
                CountDownText.color = new Color(1f, 1f, 0f);
            }
            else if (CurrentTimer <= 4f)
            {
                CountDownText.text = ("-- 2 --");
                CountDownText.color = new Color(1f, 0.5f, 0f);
            }
            else if (CurrentTimer < 5f)
            {
                CountDownText.text = ("- 1 -");
                CountDownText.color = new Color(1f, 0f, 0f);
            }
            else if(CurrentTimer <= 6f)
            {
                CountDownText.text = ("");
                Soundplay();
                CurrentTimer = 0f;
                firstCountDown = true;
            }
        }

        if (!Sound.isPlaying && !SoundEnd && firstCountDown)
        {
            SoundEnd = true;

            Debug.Log("‹ÈI—¹I");
        }
    }

    public void Soundplay()
    {
       Sound.Play();
    }
}
