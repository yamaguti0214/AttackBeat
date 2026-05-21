using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sampleSoundPlay2 : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    [SerializeField] TextMeshProUGUI CountDownText;

    private bool firstCountDown = false;
    private float CurrentTimer;

    void Start()
    {
        // 最初は音停止
        if (Sound != null)
        {
            Sound.Stop();
        }
    }

    void Update()
    {
        if (!firstCountDown)
        {
            CurrentTimer += Time.deltaTime;

            if (CurrentTimer <= 1f)
            {
                CountDownText.text = "----- 5 -----";
            }
            else if (CurrentTimer <= 2f)
            {
                CountDownText.text = "---- 4 ----";
                CountDownText.color = Color.green;
            }
            else if (CurrentTimer <= 3f)
            {
                CountDownText.text = "--- 3 ---";
                CountDownText.color = Color.yellow;
            }
            else if (CurrentTimer <= 4f)
            {
                CountDownText.text = "-- 2 --";
                CountDownText.color =
                    new Color(1f, 0.5f, 0f);
            }
            else if (CurrentTimer < 5f)
            {
                CountDownText.text = "- 1 -";
                CountDownText.color = Color.red;
            }
            else if (CurrentTimer <= 6f)
            {
                CountDownText.text = "";

                StartGame();

                CurrentTimer = 0f;
                firstCountDown = true;
            }
        }
    }

    void StartGame()
    {
        // 音楽開始
        Soundplay();

        // =========================
        // 判定開始
        // =========================
        SampleCheckNotes1 checkNotes =
            FindFirstObjectByType<SampleCheckNotes1>();

        if (checkNotes != null)
        {
            checkNotes.StartJudge();
        }

        // =========================
        // ノーツ移動開始
        // =========================
        sampleNoteMove1[] notes =
            FindObjectsByType<sampleNoteMove1>(
                FindObjectsSortMode.None
            );

        foreach (sampleNoteMove1 note in notes)
        {
            note.StartMove();
        }
    }

    public void Soundplay()
    {
        if (Sound != null)
        {
            Sound.Play();
        }
    }
}