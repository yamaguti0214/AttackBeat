using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool Death = false;
    public static bool Win = false;

    [SerializeField] private TextMeshProUGUI GAMEOVER;
    [SerializeField] private GameObject ResultButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Death)
        {
            Debug.Log("Death");
            GAMEOVER.text = "GAME OVER";
            ResultButton.SetActive(true);

            if(SceneManager.GetActiveScene().name == "MyMusicCreateNote")
            {
                Debug.Log("MyMusicCreateNote");
                GAMEOVER.text = "FINISH";
                ResultButton.SetActive(true);
            }
        }
        else if(Win)
        {
            Debug.Log("Win");
            GAMEOVER.text = "GAME CLEAR";
            ResultButton.SetActive(true);
        }
    }
}
