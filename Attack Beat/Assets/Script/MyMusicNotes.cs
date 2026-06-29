using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyMusicNotes : MonoBehaviour
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
        public float length;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<NoteInput> notes = new List<NoteInput>();
    }

    public List<NoteInput> notes = new List<NoteInput>();

    // 長押し用
    private float holdStartTime = 0f;
    private bool isHoldingSpace = false;

    private string path;

    void Awake()
    {
        CreatePath();
        Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mode = (mode == Mode.Record)
                ? Mode.Play
                : Mode.Record;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdStartTime = musicSource.time;
                isHoldingSpace = true;
            }

            if (Input.GetKeyUp(KeyCode.Space) && isHoldingSpace)
            {
                isHoldingSpace = false;

                float duration =
                    musicSource.time - holdStartTime;

                if (duration >= 0.2f)
                {
                    AddNote(
                        2,
                        duration,
                        holdStartTime
                    );
                }
                else
                {
                    AddNote(
                        0,
                        0f,
                        musicSource.time
                    );
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    private void CreatePath()
    {
        string musicName =
            SaveDataManager.SaveDataInstance.Current_musicName;

        string noteFolder = Path.Combine(
            Application.persistentDataPath,
            "SongData",
            "Notes",
            musicName
        );

        Directory.CreateDirectory(noteFolder);

        path = Path.Combine(
            noteFolder,
            musicName + ".json"
        );

        Debug.Log("保存先 : " + path);
    }

    void AddNote(
        int lane,
        float length,
        float startTime)
    {
        notes.Add(new NoteInput
        {
            timing = startTime,
            lane = lane,
            length = length
        });

        Debug.Log(
            $"記録: {startTime:F2}秒 " +
            $"Lane:{lane} " +
            $"Length:{length:F2}"
        );
    }

    public void Save()
    {
        CreatePath();

        SaveData data = new SaveData
        {
            notes = notes
        };

        string json =
            JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);

        SaveDataManager.SaveDataInstance.Current_notesPath
            = path;

        Debug.Log("保存完了 : " + path);
    }

    public void Load()
    {
        CreatePath();

        if (File.Exists(path))
        {
            string json =
                File.ReadAllText(path);

            SaveData data =
                JsonUtility.FromJson<SaveData>(json);

            notes = data.notes;

            Debug.Log(
                "読み込み完了 : "
                + notes.Count
                + "個"
            );
        }
        else
        {
            Debug.Log("保存データなし");
        }
    }
}