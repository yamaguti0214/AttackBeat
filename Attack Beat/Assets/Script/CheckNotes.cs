using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Collections;
using static CheckNotes;
using UnityEngine.SceneManagement;

public class CheckNotes : MonoBehaviour
{
    [System.Serializable]
    public class Note
    {
        public GameObject Notes;
        public float timing;
        public bool isHit;
    }

    //ノーツの判定結果
    [SerializeField] public TextMeshProUGUI Perfecttxt;
    [SerializeField] public TextMeshProUGUI Greatttxt;
    [SerializeField] public TextMeshProUGUI Goodtxt;
    [SerializeField] public TextMeshProUGUI MISStxt;
    public static int Perfect;
    public static int Great;
    public static int Good;
    public static int MISS;

    private int DestoryNotes = 0;

    //Effect
    [SerializeField] public GameObject PerfectEffect;
    [SerializeField] public GameObject GreatEffect;
    [SerializeField] public GameObject GoodEffect;

    [SerializeField] public Transform Canvastransform;

    [SerializeField] public Vector2 CheckPosition;

    [SerializeField] private SoundPlay soundPlay;

    public List<Note> notes = new List<Note>();

    public TextMeshProUGUI resultText; // ← ここにセット

    public float perfectRange = 0.025f;
    public float greatRange = 0.075f;
    public float goodRange = 0.108f;

    // 連打ペナルティ（判定ロック）のためのタイマー変数
    private float fKeyLockTimer = 0f;
    private float hKeyLockTimer = 0f;
    private float lockDuration = 0.5f; // ロックする時間（秒）調整可能

    //合計で攻撃
    public static int FullAttack = 0;

    public AudioClip PerfectSound;
    public AudioClip GreatSound;
    public AudioClip GoodSound;
    public AudioClip MissSound;
    public AudioClip GSound;
    public AudioClip ESound;
    public AudioClip MSound;

    AudioSource audioSource;

    void Start()
    {
        Perfect = 0;
        Great = 0;
        Good = 0;
        MISS = 0;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MyMusicCreateNote")
        {
            if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.H))
            {
                Instantiate(PerfectEffect, CheckPosition, Quaternion.identity, Canvastransform);
                audioSource.clip = PerfectSound;
                audioSource.Play();
            }
        }
        // タイマーを進める処理
        if (fKeyLockTimer > 0) fKeyLockTimer -= Time.deltaTime;
        if (hKeyLockTimer > 0) hKeyLockTimer -= Time.deltaTime;

        if (!ESCButton.Pause)
        {
            // Fキーが押されたら青色のノーツを判定
            if (Input.GetKeyDown(KeyCode.F))
            {
                soundPlay.SEPlay();

                // ロック中でなければ判定を行う
                if (fKeyLockTimer <= 0)
                {
                    Judge(true); // true = 青ノーツを狙う
                }
            }
            // Hキーが押されたら緑色のノーツを判定
            else if (Input.GetKeyDown(KeyCode.H))
            {
                soundPlay.SEPlay();

                // ロック中でなければ判定を行う
                if (hKeyLockTimer <= 0)
                {
                    Judge(false); // false = 緑ノーツを狙う
                }
            }

            CheckMiss();
        }

        //if(!ESCButton.Pause) Debug.Log("SoundPlay" + SoundPlay.BGMSound_public.time);
    }


    void Judge(bool isBlue)
    {
        float currentTime = SoundPlay.BGMSound_public.time;

        Note closestNote = null;
        float closestDiff = float.MaxValue;

        Debug.Log(soundPlay);
        Debug.Log(SoundPlay.BGMSound_public);

        //Debug.Log("current:" + currentTime);
        //Debug.Log("note:" + closestNote.timing);
        //Debug.Log("diff:" + closestDiff);

        foreach (var note in notes)
        {
            if (note.isHit) continue;

            bool isNoteBlue = note.Notes.name.Contains("note2_0");

            // 押したキーとノーツの色が一致していない場合は、一番近いノーツの候補から除外）する
            if (isBlue != isNoteBlue) continue;

            float diff = Mathf.Abs(currentTime - note.timing);

            if (diff < closestDiff)
            {
                closestDiff = diff;
                closestNote = note;
            }
        }

        //Debug.Log("closestDiff :"+closestDiff);

        // 一致する色のノーツが画面内に一つもない場合は、Miss判定に進まず処理を抜ける（空打ちを許容する場合）
        if (closestNote == null) return;

        // 一番近いノーツが、まだ判定ゾーン（goodRange）より手前にあるときは、
        // ノーツを消さずに、押したキーに「判定ロック（お仕置きタイム）」を付与する
        if (closestDiff > goodRange && currentTime < closestNote.timing)
        {
            if (isBlue) fKeyLockTimer = lockDuration; // Fキーをロック
            else hKeyLockTimer = lockDuration;        // Hキーをロック
            return; // ノーツは消さずにここで終了
        }

        if (closestDiff <= perfectRange)
        {
            closestNote.isHit = true;
            Perfect++;
            ShowResult("Perfect");
            NotesEffect("Perfect");
            DestoryNotes++;
            Destroy(closestNote.Notes);
            notes.Remove(closestNote);
            FullAttack += 5;
            audioSource.PlayOneShot(PerfectSound);
            if (Perfect <= 10)
            {
                if (Perfect % 5 == 0)
                {
                    audioSource.PlayOneShot(GSound);
                }
            }
            else if (Perfect > 10 && Perfect <= 20)
            {
                if (Perfect % 5 == 0)
                {
                    audioSource.PlayOneShot(ESound);

                }

            }
            else if (Perfect > 20)
            {
                if (Perfect % 5 == 0)
                {
                    audioSource.PlayOneShot(MSound);
                }
            }
        }
        else if (closestDiff <= greatRange)
        {
            closestNote.isHit = true;
            Great++;
            ShowResult("Great");
            NotesEffect("Great");
            DestoryNotes++;
            Destroy(closestNote.Notes);
            notes.Remove(closestNote);
            FullAttack += 3;
            audioSource.PlayOneShot(GreatSound);
        }
        else if (closestDiff <= goodRange)
        {
            closestNote.isHit = true;
            Good++;
            ShowResult("Good");
            NotesEffect("Good");
            DestoryNotes++;
            Destroy(closestNote.Notes);
            notes.Remove(closestNote);
            FullAttack += 1;
            audioSource.PlayOneShot(GoodSound);
        }
        else
        {
            MISS++;
            ShowResult("Miss");
            DestoryNotes++;
            audioSource.PlayOneShot(MissSound);
            if (closestNote != null)
            {
                Destroy(closestNote.Notes);
                notes.Remove(closestNote);
            }
        }
    }


    void CheckMiss()
    {
        float currentTime = SoundPlay.BGMSound_public.time;

        for (int i = notes.Count - 1; i >= 0; i--)
        {
            var note = notes[i];

            if (note == null || note.Notes == null)
            {
                notes.RemoveAt(i);
                continue;
            }

            if (note.isHit) continue;

            if (currentTime - note.timing > goodRange)
            {
                Debug.Log("MISSTIMING");

                MISS++;
                note.isHit = true;
                ShowResult("Miss");

                Destroy(note.Notes);
                notes.RemoveAt(i);
            }
        }
    }

    void ShowResult(string result)
    {
        if (resultText != null)
        {
            resultText.text = result;
            switch (result)
            {
                case "Perfect":
                    resultText.color = new Color(16, 0, 0);
                    break;
                case "Great":
                    resultText.color = new Color(0, 0, 16);
                    break;
                case "Good":
                    resultText.color = new Color(0, 16, 0);
                    break;
                case "Miss":
                    resultText.color = new Color(16, 0, 16);
                    break;

            }
        }

        switch (result)
        {
            case "Perfect":
                Perfecttxt.text = "Perfect : " + Perfect;
                break;
            case "Great":
                Greatttxt.text = "Great : " + Great;
                break;
            case "Good":
                Goodtxt.text = "Good : " + Good;
                break;
            case "Miss":
                MISStxt.text = "Miss : " + MISS;
                Debug.Log("MISS");
                break;

        }

    }

    void NotesEffect(string Note_Check)
    {
        switch (Note_Check)
        {
            case "Perfect":
                Instantiate(PerfectEffect, CheckPosition, Quaternion.identity, Canvastransform);
                break;
            case "Great":
                Instantiate(GreatEffect, CheckPosition, Quaternion.identity, Canvastransform);
                break;
            case "Good":
                Instantiate(GoodEffect, CheckPosition, Quaternion.identity, Canvastransform);
                break;
        }
    }
}