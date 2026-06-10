using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SFB;

public class MusicLoader : MonoBehaviour
{
    public static MusicLoader Instance;

    [Header("AudioSource")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    public AudioClip LoadedBGM { get; private set; }
    public AudioClip LoadedSE { get; private set; }

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

    private IEnumerator LoadAudio(string path, bool isBGM)
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

                if (isBGM)
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