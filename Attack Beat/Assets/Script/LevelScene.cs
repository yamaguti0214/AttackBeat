using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    public void OnLevel1()
    {
        SceneManager.LoadScene("LevelOneScene");
    }
    public void OnLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void OnLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void OnTitle()
    {
        SceneManager.LoadScene("Title Scene");
    }
}
