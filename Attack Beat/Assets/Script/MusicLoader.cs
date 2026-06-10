using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SFB;
using UnityEngine.UI;

public class MusicLoader : MonoBehaviour
{
    public static MusicLoader Instance;

    [Header("AudioSource")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    public AudioClip LoadedBGM { get; private set; }
    public AudioClip LoadedSE { get; private set; }

    // EnemyImageNum
    public static int Enemynum = 1;
    public static Sprite ChoiceEnemy;

    // BackGround
    public static int BackGroundnum = 1;
    public static Sprite ChoiceBackGround;

    [SerializeField] private Button CreateButton;

    private void Awake()
    {
        CreateButton.interactable = false;
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

    // BGM選択
    public void OpenBGMFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "BGMを選択",
            "",
            new ExtensionFilter[]
            {
                new ExtensionFilter("Audio Files", "mp3", "wav", "ogg")
            },
            false
        );

        if (paths.Length > 0)
        {
            StartCoroutine(LoadAudio(paths[0], true));
        }
    }

    // SE選択
    public void OpenSEFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "SEを選択",
            "",
            new ExtensionFilter[]
            {
                new ExtensionFilter("Audio Files", "mp3", "wav", "ogg")
            },
            false
        );

        if (paths.Length > 0)
        {
            StartCoroutine(LoadAudio(paths[0], false));
        }
    }

    private AudioType GetAudioType(string path)
    {
        string extension = Path.GetExtension(path).ToLower();

        switch (extension)
        {
            case ".mp3":
                return AudioType.MPEG;

            case ".wav":
                return AudioType.WAV;

            case ".ogg":
                return AudioType.OGGVORBIS;

            default:
                return AudioType.UNKNOWN;
        }
    }

    private IEnumerator LoadAudio(string path, bool isBGM)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("ファイルパスが空です");
            yield break;
        }

        Debug.Log("選択ファイル : " + path);

        AudioType audioType = GetAudioType(path);

        if (audioType == AudioType.UNKNOWN)
        {
            Debug.LogError("対応していない形式 : " + Path.GetExtension(path));
            yield break;
        }

        string url = "file:///" + path.Replace("\\", "/");

        using (UnityWebRequest www =
               UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = false;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("読み込み失敗 : " + www.error);
                yield break;
            }

            AudioClip clip = null;

            try
            {
                clip = DownloadHandlerAudioClip.GetContent(www);
            }
            catch (System.Exception e)
            {
                Debug.LogError("AudioClip取得失敗 : " + e.Message);
                yield break;
            }

            if (clip == null)
            {
                Debug.LogError("AudioClipがnullです");
                yield break;
            }

            Debug.Log(
                $"読込成功 : {clip.name} " +
                $"Length={clip.length:F2}s " +
                $"Channels={clip.channels} " +
                $"Frequency={clip.frequency}"
            );

            if (isBGM)
            {
                LoadedBGM = clip;
                bgmSource.clip = clip;

                Debug.Log("BGM読み込み完了");

                CreateButton.interactable = true;
            }
            else
            {
                LoadedSE = clip;
                seSource.clip = clip;

                Debug.Log("SE読み込み完了");
            }
        }
    }

    // BGM再生
    public void PlayBGM()
    {
        if (bgmSource != null && bgmSource.clip != null)
        {
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGMが設定されていません");
        }
    }

    // SE再生
    public void PlaySE()
    {
        if (seSource != null && LoadedSE != null)
        {
            seSource.PlayOneShot(LoadedSE);
        }
        else
        {
            Debug.LogWarning("SEが設定されていません");
        }
    }
}