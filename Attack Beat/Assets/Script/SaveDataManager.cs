using System.Collections.Generic;
using System.IO;
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

    //作譜リスト
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

    [SerializeField] private Transform content;
    [SerializeField] private GameObject songButtonPrefab;
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
        
        if (scene.name == "MyMusicList")
        {
            content = GameObject.Find("Content").transform;

            CreateSongButtons();
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

    public void SaveSongData()
    {
        SongData song = new SongData();

        song.musicName = Current_musicName;
        song.musicPath = Current_musicPath;
        song.sePath = Current_sePath;
        song.notesPath = Current_notesPath;

        song.enemyType = Current_enemyType;
        song.enemyPath = Current_enemyPath;

        song.backgroundType = Current_backgroundType;
        song.backgroundPath = Current_backgroundPath;

        // 保存先
        string saveFolder = Path.Combine(
            Application.persistentDataPath,
            "SongData",
            "SaveData"
        );

        Directory.CreateDirectory(saveFolder);

        string path = Path.Combine(
            saveFolder,
            song.musicName + ".json"
        );

        // JSONへ変換
        string json = JsonUtility.ToJson(song, true);

        // 保存
        File.WriteAllText(path, json);

        Debug.Log("保存完了 : " + path);
    }
    private void CreateSongButtons()
    {
        string saveFolder = Path.Combine(
            Application.persistentDataPath,
            "SongData",
            "SaveData"
        );

        if (!Directory.Exists(saveFolder))
            return;

        // 前回作ったボタンを消す
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // json全部取得
        string[] files =
            Directory.GetFiles(saveFolder, "*.json");

        foreach (string file in files)
        {
            // Json読み込み
            string json = File.ReadAllText(file);

            SongData song =
                JsonUtility.FromJson<SongData>(json);

            // ボタン生成
            GameObject button =
                Instantiate(songButtonPrefab, content);

            // ボタン名
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
                song.musicName;

            // ボタンにデータを持たせる
            button.GetComponent<SongListButton>()
                .SetData(song);
        }
    }

    public void SetMusicName()
    {
        string inputName = musicNameInput.text;

        if (IsMusicNameExists(inputName))
        {
            Debug.Log("同じ曲名が存在します");

            musicNameInput.text = "";

            return;
        }

        Current_musicName = inputName;

        Debug.Log("曲名：" + Current_musicName);
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

        Debug.Log($"{songData.musicName} を登録しました");
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
                Debug.LogError("背景または敵が見つからない");
                return;
            }
            
            //敵がNight,Armor,Ghostの時
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
            else if (!string.IsNullOrEmpty(SaveDataManager.SaveDataInstance.Current_enemyPath) && Current_enemyType == EnemyType.MY)  //敵がプレイヤーが用意したものの場合
            {
                enemy.GetComponent<SpriteRenderer>().sprite = MusicLoader.Instance.LoadSprite(
                    SaveDataManager.SaveDataInstance.Current_enemyPath);
            }

            //背景がDungeon,Castle,Ruinsの時
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
            else if (!string.IsNullOrEmpty(SaveDataManager.SaveDataInstance.Current_backgroundPath) && Current_backgroundType == BackgroundType.MY)  //敵がプレイヤーが用意したものの場合
            {
                background.GetComponent<SpriteRenderer>().sprite = MusicLoader.Instance.LoadSprite(
                    SaveDataManager.SaveDataInstance.Current_backgroundPath);
            }
        }
    }
}