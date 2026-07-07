using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public void ResultScene()
    {
        if (SceneManager.GetActiveScene().name == "MyMusicCreateNote")
        {
            MyMusicNoteRecorder.MyMusicRecorderinstance.Save();

            //ƒŠƒXƒg‚É•Û‘¶
            SaveDataManager.SaveDataInstance.SetNewSongData();

            SceneManager.LoadScene("MyMusicList");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
