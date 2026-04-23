using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NoteRecorder : MonoBehaviour
{
    public enum Mode
    {
        Record,
        Play
    }

    public Mode mode = Mode.Record;
    public AudioSource musicSource;

    [System.Serializable]
    public class NoteInput
    {
        public float timing;
        public int lane;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<NoteInput> notes = new List<NoteInput>();
    }

    public List<NoteInput> notes = new List<NoteInput>();

    string path;

    void Awake()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        path = Path.Combine(desktopPath, "notes_song1.json");

        Debug.Log("保存先: " + path);

        Load();
    }

    void Update()
    {
        //Rキーでモードの切り替え
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (mode == Mode.Record)
            {
                mode = Mode.Play;
            }
            else if (mode == Mode.Play)
            {
                mode = Mode.Record;
            }
;
        }

        if (mode == Mode.Record)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddNote(0);
            }
        }

        // 手動保存（例：Sキー）
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    void AddNote(int lane)
    {
        float time = musicSource.time;

        notes.Add(new NoteInput
        {
            timing = time,
            lane = lane
        });

        Debug.Log("記録: " + time);
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.notes = notes;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("保存したで: " + path);
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            notes = data.notes;

            Debug.Log("読み込み完了: " + notes.Count + "個");
        }
        else
        {
            Debug.Log("保存データなし");
        }
    }
}