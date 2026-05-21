using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Perfecttext, Greattext, Goodtext, Misstext,GAMEOVERtext;
    // Start is called before the first frame update
    void Start()
    {
        Perfecttext.text = CheckNotes.Perfect.ToString();
        Greattext.text = CheckNotes.Great.ToString();
        Goodtext.text = CheckNotes.Good.ToString();
        Misstext.text = CheckNotes.MISS.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("Title Scene");
    }
}
