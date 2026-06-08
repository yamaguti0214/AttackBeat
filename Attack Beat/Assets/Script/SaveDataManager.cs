using TMPro;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager SaveDataInstance;
    [SerializeField] private TMP_InputField musicNameInput;

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
}