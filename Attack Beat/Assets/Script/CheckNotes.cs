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
    public float length;
    public int lane;
    public bool isHit;
    }

    //�m�[�c�̔��茋��
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

    public TextMeshProUGUI resultText; // �� �����ɃZ�b�g

    public float perfectRange = 0.025f;
    public float greatRange = 0.075f;
    public float goodRange = 0.108f;

    // �A�Ńy�i���e�B�i���胍�b�N�j�̂��߂̃^�C�}�[�ϐ�
    private float fKeyLockTimer = 0f;
    private float hKeyLockTimer = 0f;
    private float lockDuration = 0.5f; // ���b�N���鎞�ԁi�b�j�����\

    //���v�ōU��
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
        // �^�C�}�[��i�߂鏈��
        if (fKeyLockTimer > 0) fKeyLockTimer -= Time.deltaTime;
        if (hKeyLockTimer > 0) hKeyLockTimer -= Time.deltaTime;

        if (!ESCButton.Pause)
        {
            // F�L�[�������ꂽ��F�̃m�[�c�𔻒�
            if (Input.GetKeyDown(KeyCode.F))
            {
                soundPlay.SEPlay();

                // ���b�N���łȂ���Δ�����s��
                if (fKeyLockTimer <= 0)
                {
                    Judge(true); // true = �m�[�c��_��
                }
            }
            // H�L�[�������ꂽ��ΐF�̃m�[�c�𔻒�
            else if (Input.GetKeyDown(KeyCode.H))
            {
                soundPlay.SEPlay();

                // ���b�N���łȂ���Δ�����s��
                if (hKeyLockTimer <= 0)
                {
                    Judge(false); // false = �΃m�[�c��_��
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

            // �������L�[�ƃm�[�c�̐F����v���Ă��Ȃ��ꍇ�́A��ԋ߂��m�[�c�̌�₩�珜�O�j����
            if (isBlue != isNoteBlue) continue;

            float diff = Mathf.Abs(currentTime - note.timing);

            if (diff < closestDiff)
            {
                closestDiff = diff;
                closestNote = note;
            }
        }

        //Debug.Log("closestDiff :"+closestDiff);

        // ��v����F�̃m�[�c����ʓ��Ɉ���Ȃ��ꍇ�́AMiss����ɐi�܂������𔲂���i��ł������e����ꍇ�j
        if (closestNote == null) return;

        // ��ԋ߂��m�[�c���A�܂�����]�[���igoodRange�j����O�ɂ���Ƃ��́A
        // �m�[�c���������ɁA�������L�[�Ɂu���胍�b�N�i���d�u���^�C���j�v��t�^����
        if (closestDiff > goodRange && currentTime < closestNote.timing)
        {
            if (isBlue) fKeyLockTimer = lockDuration; // F�L�[�����b�N
            else hKeyLockTimer = lockDuration;        // H�L�[�����b�N
            return; // �m�[�c�͏������ɂ����ŏI��
        }

        if (closestDiff <= perfectRange)
        {
            closestNote.isHit = true;
            Perfect++;
            ShowResult("Perfect");
            NotesEffect("Perfect");
            DestoryNotes++;
            FlyNote(closestNote.Notes);
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
            FlyNote(closestNote.Notes);
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
            FlyNote(closestNote.Notes);
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
    void FlyNote(GameObject noteObj)
{
    if (noteObj == null) return;

    StartCoroutine(FlyNoteCoroutine(noteObj));
}

IEnumerator FlyNoteCoroutine(GameObject noteObj)
{
    float time = 0f;
    float duration = 1.2f;

    Vector3 startPos = noteObj.transform.position;

    while (time < duration)
    {
        if (noteObj == null) yield break;

        time += Time.deltaTime;
        float t = time / duration;

        float x = Mathf.Lerp(0f, 14f, t);
        float y = (-4f * 3f * (t - 0.5f) * (t - 0.5f)) + 3f;

        noteObj.transform.position = startPos + new Vector3(x, y, 0f);
        noteObj.transform.Rotate(0f, 0f, -360f * Time.deltaTime);

        yield return null;
    }

    Destroy(noteObj);
}
}