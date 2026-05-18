using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCButton : MonoBehaviour
{
    [SerializeField]
    private GameObject BGMSlider, SESlider, BGMText, SEText,PauseImage;

    [SerializeField] private SoundPlay soundPlay;
    public static bool Pause = false;
    private bool pauseOff = false; 
    //private int ESCbuttonClick = 0;           //ポーズを解除したとき(0を除く２の段の時)

    private float CurrentTimer;

    private bool PauseCancel = false;          //ポーズ中にポーズできないようにする
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !PauseCancel)
        {
            OnEscbutton();
        }

        //ポーズを解除したとき
        if (Pause && pauseOff)
        {
            PauseOff();
        }
    }

    public void OnEscbutton()
    {
        if (!Pause)
        {
            //ポーズをする処理
            PauseCancel = true;

            Time.timeScale = 0f;

            Pause = true;

            //ESCbuttonClick++;

            BGMSlider.SetActive(!BGMSlider.activeSelf);
            SESlider.SetActive(!SESlider.activeSelf);
            BGMText.SetActive(!BGMText.activeSelf);
            SEText.SetActive(!SEText.activeSelf);
            PauseImage.SetActive(!PauseImage.activeSelf);
        }
        else if(Pause && SoundPlay.CountDownEnd)　　　　　　　　　　//ポーズ中　尚且つ　カウントダウン後
        {
            //ポーズを解くときの処理
            pauseOff = true;

            BGMSlider.SetActive(!BGMSlider.activeSelf);
            SESlider.SetActive(!SESlider.activeSelf);
            BGMText.SetActive(!BGMText.activeSelf);
            SEText.SetActive(!SEText.activeSelf);
            PauseImage.SetActive(!PauseImage.activeSelf);

        }
    }

    void PauseOff()
    {
        CurrentTimer += Time.unscaledDeltaTime;
        soundPlay.CountDown(CurrentTimer);
        if (CurrentTimer >= 6f)
        {
            CurrentTimer = 0;
            PauseCancel = false;
            Pause = false;

            Time.timeScale = 1f;
            pauseOff = false;
        }
    }

}
