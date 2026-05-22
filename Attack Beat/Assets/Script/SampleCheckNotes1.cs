using UnityEngine;
using TMPro;

public class SampleCheckNotes1 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Perfecttxt;
    [SerializeField] TextMeshProUGUI Greatttxt;
    [SerializeField] TextMeshProUGUI Goodtxt;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] GameObject PerfectEffect;
    [SerializeField] GameObject GreatEffect;
    [SerializeField] GameObject GoodEffect;

    [SerializeField] Transform Canvastransform;

    // 白い判定円
    [SerializeField] Transform JudgePoint;

    // 判定幅
    public float perfectRange = 0.3f;
    public float greatRange = 0.7f;
    public float goodRange = 1.2f;

    private int Perfect;
    private int Great;
    private int Good;

    // 判定開始管理
    private bool canJudge = false;

    void Update()
    {
        // ゲーム開始後だけ判定
        if (!canJudge) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Judge();
        }
    }

    // 判定開始
    public void StartJudge()
    {
        canJudge = true;
    }

    void Judge()
    {
        // 全ノーツ取得
        sampleNoteMove1[] sceneNotes =
            FindObjectsByType<sampleNoteMove1>(FindObjectsSortMode.None);

        sampleNoteMove1 closestNote = null;

        float closestDiff = float.MaxValue;

        foreach (sampleNoteMove1 note in sceneNotes)
        {
            if (note == null) continue;

            float diff =
                Mathf.Abs(
                    note.transform.position.x
                    - JudgePoint.position.x
                );

            if (diff < closestDiff)
            {
                closestDiff = diff;
                closestNote = note;
            }
        }

        // ノーツなし
        if (closestNote == null)
        {
            ShowResult("Miss");
            return;
        }

        Debug.Log("closestDiff = " + closestDiff);

        // Perfect
        if (closestDiff <= perfectRange)
        {
            Perfect++;

            ShowResult("Perfect");

            NotesEffect("Perfect");

            closestNote.Hit();
        }

        // Great
        else if (closestDiff <= greatRange)
        {
            Great++;

            ShowResult("Great");

            NotesEffect("Great");

            closestNote.Hit();
        }

        // Good
        else if (closestDiff <= goodRange)
        {
            Good++;

            ShowResult("Good");

            NotesEffect("Good");

            closestNote.Hit();
        }

        // Miss
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

                if (Perfecttxt != null)
                {
                    Perfecttxt.text =
                        "Perfect : " + Perfect;
                }

                break;

            case "Great":

                if (Greatttxt != null)
                {
                    Greatttxt.text =
                        "Great : " + Great;
                }

                break;

            case "Good":

                if (Goodtxt != null)
                {
                    Goodtxt.text =
                        "Good : " + Good;
                }

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

                if (PerfectEffect != null)
                {
                    Instantiate(
                        PerfectEffect,
                        spawnPos,
                        Quaternion.identity,
                        Canvastransform
                    );
                }

                break;

            case "Great":

                if (GreatEffect != null)
                {
                    Instantiate(
                        GreatEffect,
                        spawnPos,
                        Quaternion.identity,
                        Canvastransform
                    );
                }

                break;

            case "Good":

                if (GoodEffect != null)
                {
                    Instantiate(
                        GoodEffect,
                        spawnPos,
                        Quaternion.identity,
                        Canvastransform
                    );
                }

                break;
        }
    }
}