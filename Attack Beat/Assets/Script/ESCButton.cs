using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCButton : MonoBehaviour
{
    [SerializeField]
    private GameObject BGMSlider, SESlider, BGMText, SEText;

    [SerializeField] private SoundPlay soundPlay;
    public static bool Pause = false;
    private int UnPause = 0;           //ポーズを解除したとき(0を除く２の段の時)

    private float CurrentTimer;
    private bool CountDownEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscbutton();
        }

        if(!Pause && UnPause != 0 && UnPause % 2 == 0 && !CountDownEnd)
        {
            CurrentTimer += Time.deltaTime;
            soundPlay.CountDown(CurrentTimer);
            if (CurrentTimer >= 6f)
            {
                CurrentTimer = 0;
                CountDownEnd = true;
            }
        }
    }

    public void OnEscbutton()
    {
        Pause = !Pause;

        UnPause++;
        CountDownEnd = false;

        BGMSlider.SetActive(!BGMSlider.activeSelf);
        SESlider.SetActive(!SESlider.activeSelf);
        BGMText.SetActive(!BGMText.activeSelf);
        SEText.SetActive(!SEText.activeSelf);
    }
}
