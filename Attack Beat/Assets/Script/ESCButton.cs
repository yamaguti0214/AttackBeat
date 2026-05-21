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

            BGMSlider.SetActive(true);
            SESlider.SetActive(true);
            BGMText.SetActive(true);
            SEText.SetActive(true);
            PauseImage.SetActive(true);
        }
        else if(Pause && SoundPlay.CountDownEnd)　　　　　　　　　　//ポーズ中　尚且つ　カウントダウン後
        {
            //ポーズを解くときの処理
            pauseOff = true;

            BGMSlider.SetActive(false);
            SESlider.SetActive(false);
            BGMText.SetActive(false);
            SEText.SetActive(false);
            PauseImage.SetActive(false);

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
