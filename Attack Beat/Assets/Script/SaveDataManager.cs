using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager SaveDataInstance;
    [SerializeField] private TMP_InputField musicNameInput;
    [SerializeField] private Button saveButton;

    //çÏïàÉäÉXÉg
    public List<SongData> songList = new List<SongData>();

    public string Current_musicName;
    public string Current_musicPath;
    public string Current_sePath;
    public string Current_backgroundPath;
    public string Current_enemyPath;
    public string Current_notesPath;

    public class SongData
    {
        public string musicName;
        public string musicPath;
        public string sePath;
        public string backgroundPath;
        public string enemyPath;
        public string notesPath;
    }
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
        string inputName = musicNameInput.text;

        if (IsMusicNameExists(inputName))
        {
            Debug.Log("ìØÇ∂ã»ñºÇ™ë∂ç›ÇµÇ‹Ç∑");

            musicNameInput.text = "";

            return;
        }

        Current_musicName = inputName;

        Debug.Log("ã»ñºÅF" + Current_musicName);
    }

    public void SetNewSongData()
    {
        SongData songData = new SongData();

        songData.musicName = Current_musicName;
        songData.musicPath = Current_musicPath;
        songData.sePath = Current_sePath;
        songData.backgroundPath = Current_backgroundPath;
        songData.enemyPath = Current_enemyPath;
        songData.notesPath = Current_notesPath;

        songList.Add(songData);

        Debug.Log($"{songData.musicName} Çìoò^ÇµÇ‹ÇµÇΩ");
    }
    public bool IsMusicNameExists(string musicName)
    {
        foreach (var song in songList)
        {
            if (song.musicName == musicName)
            {
                return true;
            }
        }

        return false;
    }

    public void ChangeTMPMusicName()
    {
        saveButton.interactable =
            !string.IsNullOrWhiteSpace(musicNameInput.text)
            && !SaveDataManager.SaveDataInstance.IsMusicNameExists(musicNameInput.text);
    }
}