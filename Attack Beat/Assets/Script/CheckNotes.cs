using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Collections;

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
    private int Perfect;
    private int Great;
    private int Good;

    private int DestoryNotes = 0;

    //Effect
    [SerializeField] public GameObject PerfectEffect;
    [SerializeField] public GameObject GreatEffect;
    [SerializeField] public GameObject GoodEffect;

    [SerializeField] public Transform Canvastransform;

    [SerializeField]public Vector2 CheckPosition;

    SoundPlay SoundPlay;
    EnemyHP enemyHP;

    public List<Note> notes = new List<Note>();
    public AudioSource musicSource;

    public TextMeshProUGUI resultText; // ← ここにセット

    public float perfectRange = 0.1f;
    public float greatRange = 0.15f;
    public float goodRange = 0.2f;

    void Start()
    {
        enemyHP = FindObjectOfType<EnemyHP>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Judge();
            //SoundPlay.Soundplay();
        }

        CheckMiss();
    }

    void Judge()
    {
        float currentTime = musicSource.time;

        Note closestNote = null;
        float closestDiff = float.MaxValue;

        foreach (var note in notes)
        {
            if (note.isHit) continue;

            float diff = Mathf.Abs(currentTime - note.timing);

            if (diff < closestDiff)
            {
                closestDiff = diff;
                closestNote = note;
            }
        }

        if (closestNote == null) return;

        if (closestDiff <= perfectRange)
        {
            closestNote.isHit = true;
            Perfect++;
            ShowResult("Perfect");
            NotesEffect("Perfect");
            DestoryNotes++;
            Destroy(closestNote.Notes);
            notes.Remove(closestNote);
            EnemyDamage(10);
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
            EnemyDamage(5);
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
            EnemyDamage(2);
        }
        else
        {
            ShowResult("Miss");
            DestoryNotes++;
            Destroy(closestNote.Notes);
            notes.Remove(closestNote);
        }
    }

    void CheckMiss()
    {
        float currentTime = musicSource.time;

        foreach (var note in notes)
        {
            if (note.isHit) continue;

            if (currentTime - note.timing > goodRange)
            {
                note.isHit = true;
                ShowResult("Miss");
            }
        }
    }

    void ShowResult(string result)
    {
        if (resultText != null)
        {
            resultText.text = result;
            switch(result)
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

        }

    }

    void NotesEffect(string Note_Check)
    {
        switch(Note_Check)
        {
            case "Perfect":
                Instantiate(PerfectEffect, CheckPosition, Quaternion.identity,Canvastransform);
                break;
            case "Great":
                Instantiate(GreatEffect, CheckPosition, Quaternion.identity,Canvastransform);
                break;
            case "Good":
                Instantiate(GoodEffect, CheckPosition, Quaternion.identity,Canvastransform);
                break;
        }
    }

    void EnemyDamage(int Damage)
    {
        enemyHP.TakeDamage(Damage);
    }
}