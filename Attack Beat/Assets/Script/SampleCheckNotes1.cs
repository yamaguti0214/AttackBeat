using UnityEngine;
using TMPro;

public class SamplCheckNotes : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Perfecttxt;
    [SerializeField] public TextMeshProUGUI Greatttxt;
    [SerializeField] public TextMeshProUGUI Goodtxt;
    [SerializeField] public TextMeshProUGUI resultText;

    [SerializeField] public GameObject PerfectEffect;
    [SerializeField] public GameObject GreatEffect;
    [SerializeField] public GameObject GoodEffect;

    [SerializeField] public Transform Canvastransform;

    // 白い判定円をここに入れる
    [SerializeField] public Transform JudgePoint;

    public float perfectRange = 0.3f;
    public float greatRange = 0.7f;
    public float goodRange = 1.2f;

    private int Perfect;
    private int Great;
    private int Good;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Judge();
        }
    }

    void Judge()
    {
        SimpleNote[] sceneNotes = FindObjectsOfType<SimpleNote>();

        SimpleNote closestNote = null;
        float closestDiff = float.MaxValue;

        foreach (SimpleNote note in sceneNotes)
        {
            if (note == null) continue;

            float diff = Mathf.Abs(note.transform.position.x - JudgePoint.position.x);

            if (diff < closestDiff)
            {
                closestDiff = diff;
                closestNote = note;
            }
        }

        if (closestNote == null)
        {
            ShowResult("Miss");
            return;
        }

        Debug.Log("closestDiff = " + closestDiff);

        if (closestDiff <= perfectRange)
        {
            Perfect++;
            ShowResult("Perfect");
            NotesEffect("Perfect");
            closestNote.Hit();
        }
        else if (closestDiff <= greatRange)
        {
            Great++;
            ShowResult("Great");
            NotesEffect("Great");
            closestNote.Hit();
        }
        else if (closestDiff <= goodRange)
        {
            Good++;
            ShowResult("Good");
            NotesEffect("Good");
            closestNote.Hit();
        }
        else
        {
            ShowResult("Miss");
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
                    resultText.color = Color.red;
                    break;
                case "Great":
                    resultText.color = Color.blue;
                    break;
                case "Good":
                    resultText.color = Color.green;
                    break;
                default:
                    resultText.color = Color.white;
                    break;
            }
        }

        switch (result)
        {
            case "Perfect":
                if (Perfecttxt != null) Perfecttxt.text = "Perfect : " + Perfect;
                break;
            case "Great":
                if (Greatttxt != null) Greatttxt.text = "Great : " + Great;
                break;
            case "Good":
                if (Goodtxt != null) Goodtxt.text = "Good : " + Good;
                break;
        }
    }

    void NotesEffect(string noteCheck)
    {
        if (JudgePoint == null) return;

        Vector3 spawnPos = JudgePoint.position;

        switch (noteCheck)
        {
            case "Perfect":
                if (PerfectEffect != null) Instantiate(PerfectEffect, spawnPos, Quaternion.identity, Canvastransform);
                break;
            case "Great":
                if (GreatEffect != null) Instantiate(GreatEffect, spawnPos, Quaternion.identity, Canvastransform);
                break;
            case "Good":
                if (GoodEffect != null) Instantiate(GoodEffect, spawnPos, Quaternion.identity, Canvastransform);
                break;
        }
    }
}