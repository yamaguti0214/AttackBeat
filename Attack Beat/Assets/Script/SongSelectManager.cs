using UnityEngine;

public class SongSelectManager : MonoBehaviour
{
    public static SongSelectManager Instance;

    public SaveDataManager.SongData CurrentSong;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSong(
        SaveDataManager.SongData song)
    {
        CurrentSong = song;

        Debug.Log(
            "‘I‘ð‹È : "
            + song.musicName
        );
    }
}