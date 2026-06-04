using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaneScene : MonoBehaviour
{
    [SerializeField] private string SceneName;
    // Start is called before the first frame update
    public void OnChangeScene()
    {
        if(SceneName != null)
        {
            SceneManager.LoadScene(SceneName);
        }
        else if(SceneName == null)
        {
            Debug.Log("SceneNameがインスペクターからはいってない");
        }
    }
}
