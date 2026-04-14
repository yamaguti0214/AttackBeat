using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckNotes : MonoBehaviour
{
    [System.Serializable]
    public class Note
    {
        public float timing;
        public bool isHit;
    }

    public List<Note> notes = new List<Note>();
    public AudioSource musicSource;

    public TextMeshProUGUI resultText; // Å© Ç±Ç±Ç…ÉZÉbÉg

    public float perfectRange = 0.05f;
    public float greatRange = 0.1f;
    public float goodRange = 0.2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Judge();
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
            ShowResult("Perfect");
        }
        else if (closestDiff <= greatRange)
        {
            closestNote.isHit = true;
            ShowResult("Great");
        }
        else if (closestDiff <= goodRange)
        {
            closestNote.isHit = true;
            ShowResult("Good");
        }
        else
        {
            ShowResult("Miss");
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
        }
    }
}