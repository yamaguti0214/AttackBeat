using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyImageLoader : MonoBehaviour
{
    [SerializeField] private GameObject PerfectText;
    [SerializeField] private GameObject GrearText;
    [SerializeField] private GameObject GoodText;
    [SerializeField] private GameObject MissText;
    [SerializeField] private GameObject[] Gerge;
    [SerializeField] private GameObject EnemyHPbar;

    [SerializeField] private SpriteRenderer BackgoundImage;
    [SerializeField] private SpriteRenderer EnemyImage;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name =="MyMusicCreateNote")
        {
            PerfectText.SetActive(false);
            GrearText.SetActive(false);
            GoodText.SetActive(false);
            MissText.SetActive(false);
            EnemyHPbar.SetActive(false);
            for(int i = 0;i<Gerge.Length;i++)
            {
                Gerge[i].SetActive(false);
            }
        }

        Debug.Log("Enemy" + MusicLoader.Enemynum);
        Debug.Log("Back" + MusicLoader.BackGroundnum);

        if (MusicLoader.Enemynum == 0)
        {
            anim.runtimeAnimatorController = null;

            Debug.Log("MY Enemy" + MusicLoader.ChoiceEnemy);
            EnemyImage.sprite = MusicLoader.ChoiceEnemy;

        }

        if(MusicLoader.BackGroundnum == 0)
        {
            Debug.Log("MY BackGround" + MusicLoader.ChoiceBackGround);
            BackgoundImage.sprite = MusicLoader.ChoiceBackGround;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
