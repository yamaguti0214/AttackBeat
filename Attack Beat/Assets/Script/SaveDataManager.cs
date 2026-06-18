using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager SaveDataInstance;
    [SerializeField] private TMP_InputField musicNameInput;

    //çÏïàÉäÉXÉg
    public List<SongData> songList = new List<SongData>();

    public class SongData
    {
        public string musicName;
        public string musicPath;
        public string sePath;
        public string backgroundPath;
        public string enemyPath;
        public string notesPath;
    }

    public string MusicName;
    private void Awake()
    {
        if (SaveDataInstance == null)
        {
            SaveDataInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicName()
    {
        MusicName = musicNameInput.text;

        Debug.Log("ã»ñºÅF" + MusicName);
    }

    public void SetNewSongData()
    {
        SongData songData = new SongData();
    }

    public void AddSongData(
    string musicName,
    string musicPath,
    string sePath,
    string backgroundPath,
    string enemyPath,
    string notesPath)
    {
        SongData song = new SongData();

        song.musicName = musicName;
        song.musicPath = musicPath;
        song.sePath = sePath;
        song.backgroundPath = backgroundPath;
        song.enemyPath = enemyPath;
        song.notesPath = notesPath;

        songList.Add(song);
    }
}