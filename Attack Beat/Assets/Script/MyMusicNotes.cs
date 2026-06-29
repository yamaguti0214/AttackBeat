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

    [SerializeField]
    private AudioSource musicSource;

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
        public List<NoteInput> notes =
            new List<NoteInput>();
    }

    public List<NoteInput> notes =
        new List<NoteInput>();

    private float holdStartTime = 0f;
    private bool isHoldingSpace = false;

    private string path;

    private void Awake()
    {
        CreatePath();
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mode =
                mode == Mode.Record
                ? Mode.Play
                : Mode.Record;
        }

        if (mode == Mode.Record)
        {
            if (Input.GetKeyDown(
                KeySettingManager.LeftKey))
            {
                AddNote(
                    0,
                    0f,
                    musicSource.time
                );
            }

            if (Input.GetKeyDown(
                KeySettingManager.RightKey))
            {
                AddNote(
                    1,
                    0f,
                    musicSource.time
                );
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdStartTime =
                    musicSource.time;

                isHoldingSpace = true;
            }

            if (Input.GetKeyUp(KeyCode.Space)
                && isHoldingSpace)
            {
                isHoldingSpace = false;

                float duration =
                    musicSource.time
                    - holdStartTime;

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
            SaveDataManager
            .SaveDataInstance
            .Current_musicName;

        string noteFolder =
            Path.Combine(
                Application.persistentDataPath,
                "SongData",
                "Notes",
                musicName
            );

        Directory.CreateDirectory(
            noteFolder
        );

        path = Path.Combine(
            noteFolder,
            musicName + ".json"
        );
    }

    private void AddNote(
        int lane,
        float length,
        float startTime)
    {
        notes.Add(
            new NoteInput
            {
                timing = startTime,
                lane = lane,
                length = length
            }
        );

        Debug.Log(
            $"ãLò^ : {startTime:F2}" +
            $" Lane:{lane}" +
            $" Length:{length:F2}"
        );
    }

    public void Save()
    {
        CreatePath();

        SaveData data =
            new SaveData();

        data.notes = notes;

        string json =
            JsonUtility.ToJson(
                data,
                true
            );

        File.WriteAllText(
            path,
            json
        );

        SaveDataManager
            .SaveDataInstance
            .Current_notesPath =
            path;

        // ÉäÉXÉgñ¢ìoò^Ç»ÇÁí«â¡
        if (!SaveDataManager
            .SaveDataInstance
            .IsMusicNameExists(
                SaveDataManager
                .SaveDataInstance
                .Current_musicName))
        {
            SaveDataManager
                .SaveDataInstance
                .SetNewSongData();
        }

        Debug.Log(
            "ï€ë∂äÆóπ : "
            + path
        );
    }

    public void Load()
    {
        CreatePath();

        if (!File.Exists(path))
        {
            Debug.Log(
                "ï€ë∂ÉfÅ[É^Ç»Çµ"
            );

            return;
        }

        string json =
            File.ReadAllText(path);

        SaveData data =
            JsonUtility.FromJson<SaveData>(
                json
            );

        notes = data.notes;

        Debug.Log(
            "ì«Ç›çûÇ›äÆóπ : "
            + notes.Count
            + "å¬"
        );
    }
}