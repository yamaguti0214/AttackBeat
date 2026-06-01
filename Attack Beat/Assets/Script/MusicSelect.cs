using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SFB;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void OpenFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "MP3ÇëIë",
            "",
            new ExtensionFilter[]
            {
                new ExtensionFilter("Audio Files", "mp3", "wav", "ogg")
            },
            false
        );

        if (paths.Length > 0)
        {
            StartCoroutine(LoadMusic(paths[0]));
        }
    }

    private IEnumerator LoadMusic(string path)
    {
        string url = "file://" + path;

        using (UnityWebRequest www =
               UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
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

                audioSource.clip = clip;
                audioSource.Play();

                Debug.Log("çƒê∂äJén");
            }
        }
    }
}