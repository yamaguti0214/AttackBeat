using UnityEngine;

public class SongListView : MonoBehaviour
{
    [SerializeField]
    private Transform content;

    [SerializeField]
    private SongListButton buttonPrefab;

    private void OnEnable()
    {
        CreateSongList();
    }

    public void CreateSongList()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var song in SaveDataManager.SaveDataInstance.songList)
        {
            SongListButton button =
                Instantiate(buttonPrefab, content);

            button.SetData(song);
        }
    }
}