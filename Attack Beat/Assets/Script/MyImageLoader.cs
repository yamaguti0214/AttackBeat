using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyImageLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BackgoundImage;
    [SerializeField] private SpriteRenderer EnemyImage;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
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
