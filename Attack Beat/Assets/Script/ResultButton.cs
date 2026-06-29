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
            //ƒŠƒXƒg‚É•Û‘¶
            SaveDataManager.SaveDataInstance.SetNewSongData();
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
