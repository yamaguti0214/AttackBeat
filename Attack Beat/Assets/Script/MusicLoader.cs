using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SFB;
using System.IO;
using UnityEngine.UI;
using static SaveDataManager;

public class MusicLoader : MonoBehaviour
{
    public static MusicLoader Instance;

    [Header("AudioSource")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    public AudioClip LoadedBGM { get; private set; }
    public AudioClip LoadedSE { get; private set; }

    [SerializeField] private Image Background;
    [SerializeField] private Image Enemy;
    public enum AudioFileType
    {
        BGM,
        SE
    }

    public enum ImageFileType
    {
        Background,
        Enemy
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenAudioFile(AudioFileType type)
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            type == AudioFileType.BGM ? "BGMを選択" : "SEを選択",
            "",
            new ExtensionFilter[]
            {
            new ExtensionFilter("Audio Files", "mp3", "wav", "ogg")
            },
            false
        );

        if (paths.Length <= 0)
            return;

        string folderName =
            type == AudioFileType.BGM
            ? "Music"
            : "SE";

        string songFolder = Path.Combine(
            Application.persistentDataPath,
            "SongData",
            folderName,
            SaveDataManager.SaveDataInstance.Current_musicName
        );

        string extension = Path.GetExtension(paths[0]);

        string fileName =
            type == AudioFileType.BGM
            ? "music" + extension
            : "se" + extension;

        string copiedPath = Path.Combine(songFolder, fileName);

        File.Copy(paths[0], copiedPath, true);

        if (type == AudioFileType.BGM)
        {
            SaveDataManager.SaveDataInstance.Current_musicPath = copiedPath;
        }
        else
        {
            SaveDataManager.SaveDataInstance.Current_sePath = copiedPath;
        }

        Debug.Log($"コピー完了 : {copiedPath}");

        StartCoroutine(
            LoadAudio(
                copiedPath,
                type
            )
        );
    }

    public void OpenImageFile(ImageFileType type)
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            type == ImageFileType.Background
                ? "背景画像を選択"
                : "敵画像を選択",
            "",
            new ExtensionFilter[]
            {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
            },
            false
        );

        if (paths.Length <= 0)
            return;

        string folderName =
            type == ImageFileType.Background
            ? "Background"
            : "Enemy";

        string songFolder = Path.Combine(
            Application.persistentDataPath,
            "SongData",
            folderName,
            SaveDataManager.SaveDataInstance.Current_musicName
        );

        string extension = Path.GetExtension(paths[0]);

        string fileName =
            type == ImageFileType.Background
            ? "background" + extension
            : "enemy" + extension;

        string copiedPath = Path.Combine(songFolder, fileName);

        File.Copy(paths[0], copiedPath, true);

        if (type == ImageFileType.Background)
        {
            SaveDataManager.SaveDataInstance.Current_backgroundPath = copiedPath;
        }
        else
        {
            SaveDataManager.SaveDataInstance.Current_enemyPath = copiedPath;
        }

        Debug.Log($"{folderName}画像コピー完了 : {copiedPath}");
    }

    public Sprite LoadSprite(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError($"ファイルが見つからん: {path}");
            return null;
        }

        byte[] bytes = File.ReadAllBytes(path);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    private IEnumerator LoadAudio(string path, AudioFileType type)
    {
        string url = "file://" + path;

        using (UnityWebRequest www =
               UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                AudioClip clip =
                    DownloadHandlerAudioClip.GetContent(www);

                if (type == AudioFileType.BGM)
                {
                    LoadedBGM = clip;
                    bgmSource.clip = clip;

                    Debug.Log("BGM読み込み完了");
                }
                else
                {
                    LoadedSE = clip;
                    seSource.clip = clip;

                    Debug.Log("SE読み込み完了");
                }
            }
        }
    }

    public void OpenBGMFile()
    {
        OpenAudioFile(AudioFileType.BGM);
    }

    public void OpenSEFile()
    {
        OpenAudioFile(AudioFileType.SE);
    }

    public void OpenBackgroundFile()
    {
        OpenImageFile(ImageFileType.Background);
    }

    public void OpenEnemyFile()
    {
        OpenImageFile(ImageFileType.Enemy);
    }

    public void Stage1Background()
    {
        string path = Path.Combine(
         Application.dataPath,
         "SongData",
         "Background",
         "Stage1.png"
        );

        Background.sprite = LoadSprite(path);
    }
    public void Stage2Background()
    {
        string path = Path.Combine(
         Application.dataPath,
         "SongData",
         "Background",
         "Stage2.png"
        );

        Background.sprite = LoadSprite(path);
    }
    public void Stage3Background()
    {
        string path = Path.Combine(
         Application.dataPath,
         "SongData",
         "Background",
         "Stage3.png"
        );

        Background.sprite = LoadSprite(path);
    }
    public void Stage1Enemy()
    {

    }
    public void Stage2Enemy()
    {

    }
    public void Stage3Enemy()
    {

    }

    // BGM再生
    public void PlayBGM()
    {
        if (bgmSource.clip != null)
        {
            bgmSource.Play();
        }
    }

    // SE再生
    public void PlaySE()
    {
        if (LoadedSE != null)
        {
            seSource.PlayOneShot(LoadedSE);
        }
    }
}