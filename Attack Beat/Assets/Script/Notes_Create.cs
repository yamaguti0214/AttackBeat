using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Notes_Create: MonoBehaviour
{
    [System.Serializable]
    public class NoteInput
    {
        public float timing;
        public int lane;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<NoteInput> notes;
    }

    public Transform spawnPoint;

    public CheckNotes checkNotes;

    public Transform judgePoint;
    public GameObject notePrefab;
    public float speed = 5f;
    public AudioSource musicSource;

    private List<NoteInput> notes = new List<NoteInput>();
    private int spawnIndex = 0;

    string path;

    void Start()
    {
        // デスクトップから読み込み
        string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        path = Path.Combine(desktop, "notes_song1.json");

        Load();
    }

    void Update()
    {
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
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);

        // 移動
        NoteMove move = note.GetComponent<NoteMove>();
        if (move != null)
        {
            move.speed = speed;
        }

        //判定登録（これが本体）
        CheckNotes.Note newNote = new CheckNotes.Note
        {
            Notes = note,
            timing = data.timing,
            isHit = false
        };

        checkNotes.notes.Add(newNote);
    }

    float GetSpawnOffset()
    {
        float distance = Vector3.Distance(spawnPoint.position, judgePoint.position);
        return distance / speed;
    }

    void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            notes = data.notes;

            Debug.Log("譜面読み込み: " + notes.Count);
        }
        else
        {
            Debug.Log("譜面が見つからん");
        }
    }
}