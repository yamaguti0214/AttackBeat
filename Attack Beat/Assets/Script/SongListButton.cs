using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongListButton : MonoBehaviour
{
    [SerializeField] private TMP_Text musicNameText;
    private SaveDataManager.SongData songData;

    public void SetData(SaveDataManager.SongData data)
    {
        songData = data;

        musicNameText.text =
            songData.musicName;
    }

    public void OnClick()
    {
        SongSelectManager.Instance.SelectSong(
            songData
        );
    }
}