using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MyMusicNoteRecorder : MonoBehaviour
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
        public float timing; // 始点の時間
        public int lane;
        public float length; // 連打ノーツの長さ（単押しは0）
    }

    [System.Serializable]
    public class SaveData
    {
        public List<NoteInput> notes = new List<NoteInput>();
    }

    public List<NoteInput> notes = new List<NoteInput>();

    [SerializeField] private string jsonFileName = "notes_song2.json";

    string path;

    // 長押し（スペースキー）記録用のワーク変数
    private float holdStartTime = 0f;
    private bool isHoldingSpace = false;

    public static MyMusicNoteRecorder MyMusicRecorderinstance;
    void Awake()
    {
        mode = Mode.Record;

        if (MyMusicRecorderinstance == null)
        {
            MyMusicRecorderinstance = this;
        }
    }

    void Update()
    {
        // Rキーでモード切り替え
        if (Input.GetKeyDown(KeyCode.R))
        {
            mode = (mode == Mode.Record) ? Mode.Play : Mode.Record;
        }

        if (mode == Mode.Record)
        {
            // 左のキー入力（初期はFキー）
            if (Input.GetKeyDown(KeySettingManager.LeftKey))
            {
                AddNote(0, 0f, musicSource.time);
            }
            // 右のキー入力（初期はHキー）
            else if (Input.GetKeyDown(KeySettingManager.RightKey))
            {
                AddNote(1, 0f, musicSource.time);
            }

            // --- スペースキー：連打ノーツ（長押し） ---
            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdStartTime = musicSource.time;
                isHoldingSpace = true;
            }

            if (Input.GetKeyUp(KeyCode.Space) && isHoldingSpace)
            {
                isHoldingSpace = false;
                float duration = musicSource.time - holdStartTime;

                // 0.2秒以上押していたら連打ノーツ(2)として記録
                if (duration >= 0.2f)
                {
                    AddNote(2, duration, holdStartTime);
                }
                else
                {
                    // 押し時間が短すぎた場合は、普通の青ノーツ(0)として現在の時間で記録
                    AddNote(0, 0f, musicSource.time);
                }
            }
        }
    }

    void AddNote(int lane, float length, float startTime)
    {
        notes.Add(new NoteInput
        {
            timing = startTime,
            lane = lane,
            length = length
        });

        Debug.Log($"記録: {startTime:F2}秒 (Lane: {lane}, Length: {length:F2}秒)");
    }

    public void Save()
    {
        string noteFolder = Path.Combine(
             Application.persistentDataPath,
            "SongData",
            "MusicNotes",
            SaveDataManager.SaveDataInstance.Current_musicName
        );

        Directory.CreateDirectory(noteFolder);

        path = Path.Combine(
            noteFolder,
            SaveDataManager.SaveDataInstance.Current_musicName + ".json"
        );

        SaveData data = new SaveData
        {
            notes = notes
        };

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);

        SaveDataManager.SaveDataInstance.Current_notesPath = path;
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