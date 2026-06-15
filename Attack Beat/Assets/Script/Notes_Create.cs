using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Notes_Create : MonoBehaviour
{
    [System.Serializable]
    public class NoteInput
    {
        public float timing;
        public int lane;
        public float length;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<NoteInput> notes;
    }

    public Transform spawnPoint;
    public CheckNotes checkNotes;
    public Transform judgePoint;

    public GameObject[] notePrefabs;
    public float speed = 5f;
    public AudioSource musicSource;

    [SerializeField] private string jsonFileName = "notes_song2.json";

    private List<NoteInput> notes = new List<NoteInput>();
    private int spawnIndex = 0;
    string path;

    void Start()
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicSource が設定されていません");
            return;
        }

        string desktop =
            System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Desktop);

        path = Path.Combine(desktop, jsonFileName);

        Load();
    }

    void Update()
    {
        if (musicSource == null) return;

        float currentTime = musicSource.time;

        while (spawnIndex < notes.Count &&
               notes[spawnIndex].timing - currentTime <= GetSpawnOffset())
        {
            Spawn(notes[spawnIndex]);
            spawnIndex++;
        }
    }

    void Spawn(NoteInput data)
    {
        int prefabIndex =
            Mathf.Clamp(data.lane, 0, notePrefabs.Length - 1);

        GameObject selectedPrefab =
            notePrefabs[prefabIndex];

        GameObject note =
            Instantiate(selectedPrefab,
                        spawnPoint.position,
                        Quaternion.identity);

        Debug.Log("ノーツ生成 : " + note.name);

        if (data.length > 0f)
        {
            Transform bodyTransform =
                note.transform.Find("Body");

            Transform tailTransform =
                note.transform.Find("Tail");

            if (bodyTransform != null &&
                tailTransform != null)
            {
                float visualLength =
                    speed * data.length;

                bodyTransform.localScale =
                    new Vector3(
                        visualLength,
                        bodyTransform.localScale.y,
                        bodyTransform.localScale.z);

                bodyTransform.localPosition =
                    new Vector3(
                        visualLength / 2f,
                        0f,
                        0f);

                tailTransform.localPosition =
                    new Vector3(
                        visualLength,
                        0f,
                        0f);
            }
        }

        NoteMove move =
            note.GetComponent<NoteMove>();

        if (move == null)
        {
            move = note.AddComponent<NoteMove>();
        }

        move.speed = speed;
        move.judgePoint = judgePoint;
        move.musicSource = musicSource;
        move.timing = data.timing;

        CheckNotes.Note newNote =
            new CheckNotes.Note
            {
                Notes = note,
                timing = data.timing,
                length = data.length,
                lane = data.lane,
                isHit = false
            };

        checkNotes.notes.Add(newNote);
    }

    float GetSpawnOffset()
    {
        float distance =
            Vector3.Distance(
                spawnPoint.position,
                judgePoint.position);

        return distance / speed;
    }

    void Load()
{
    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data == null || data.notes == null)
        {
            notes = new List<NoteInput>();
            Debug.LogError("譜面データの形式がおかしい: " + path);
            return;
        }

        notes = data.notes;
        Debug.Log("譜面読み込み : " + notes.Count + " (" + path + ")");
    }
    else
    {
        Debug.LogError("譜面が見つからん : " + path);
    }
}
}