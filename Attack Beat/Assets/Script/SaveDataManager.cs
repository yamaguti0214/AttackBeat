using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public EnemyType Current_enemyType;
    public BackgroundType Current_backgroundType;


    [SerializeField] private Sprite[] BackgroundSprite = new Sprite[3];
    [SerializeField] private Sprite[] EnemySprite = new Sprite[3];

    public enum EnemyType
    {
        Armor,
        Ghost,
        Night,
        MY
    }

    public enum BackgroundType
    {
        Dungeon,
        Castle,
        Ruins,
        MY
    }

    public class SongData
    {
        public string musicName;
        public string musicPath;
        public string sePath;
        public string notesPath;

        public EnemyType enemyType;
        public string enemyPath;

        public BackgroundType backgroundType;
        public string backgroundPath;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MyMusicCreateNotes")
        {
            SetStageData();
        }
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
        songData.enemyType = Current_enemyType;
        songData.backgroundType = Current_backgroundType;

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

    public void SetStageData()
    {
        if (SceneManager.GetActiveScene().name == "MyMusicCreateNotes")
        {
            GameObject background = GameObject.Find("background");
            GameObject enemy = GameObject.Find("Dark Knight");

            if (background == null || enemy == null)
            {
                Debug.LogError("îwåiÇ‹ÇΩÇÕìGÇ™å©Ç¬Ç©ÇÁÇ»Ç¢");
                return;
            }
            
            //ìGÇ™Night,Armor,GhostÇÃéû
            if (Current_enemyType != EnemyType.MY)
            {
                switch (Current_enemyType)
                {
                    case EnemyType.Night:
                        enemy.GetComponent<SpriteRenderer>().sprite = EnemySprite[0];
                        break;
                    case EnemyType.Armor:
                        enemy.GetComponent<SpriteRenderer>().sprite = EnemySprite[1];
                        break;
                    case EnemyType.Ghost:
                        enemy.GetComponent<SpriteRenderer>().sprite = EnemySprite[2];
                        break;
                }
            }
            else if (!string.IsNullOrEmpty(SaveDataManager.SaveDataInstance.Current_enemyPath) && Current_enemyType == EnemyType.MY)  //ìGÇ™ÉvÉåÉCÉÑÅ[Ç™ópà”ÇµÇΩÇ‡ÇÃÇÃèÍçá
            {
                enemy.GetComponent<SpriteRenderer>().sprite = MusicLoader.Instance.LoadSprite(
                    SaveDataManager.SaveDataInstance.Current_enemyPath);
            }

            //îwåiÇ™Dungeon,Castle,RuinsÇÃéû
            if (Current_backgroundType != BackgroundType.MY)
            {
                switch (Current_backgroundType)
                {
                    case BackgroundType.Dungeon:
                        background.GetComponent<SpriteRenderer>().sprite = BackgroundSprite[0];
                        break;
                    case BackgroundType.Castle:
                        background.GetComponent<SpriteRenderer>().sprite = BackgroundSprite[1];
                        break;
                    case BackgroundType.Ruins:
                        background.GetComponent<SpriteRenderer>().sprite = BackgroundSprite[2];
                        break;
                }
            }
            else if (!string.IsNullOrEmpty(SaveDataManager.SaveDataInstance.Current_backgroundPath) && Current_backgroundType == BackgroundType.MY)  //ìGÇ™ÉvÉåÉCÉÑÅ[Ç™ópà”ÇµÇΩÇ‡ÇÃÇÃèÍçá
            {
                background.GetComponent<SpriteRenderer>().sprite = MusicLoader.Instance.LoadSprite(
                    SaveDataManager.SaveDataInstance.Current_backgroundPath);
            }
        }
    }
}