using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullAttack : MonoBehaviour
{
    EnemyHP enemyHP;
    [SerializeField] private Image[] AttackgergeImage;

    
    Color[] rainbow;            //ゲージマックスの虹色の配列
    private bool GergeMAX = false;

    private float GergeTimer = 0f;
    private int CurrenMoveColor = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyHP = FindFirstObjectByType<EnemyHP>();

        rainbow = new Color[15]
        {
            new Color32(255,0,0,255),
            new Color32(255,64,0,255),
            new Color32(255,128,0,255),
            new Color32(255,191,0,255),
            new Color32(255,255,0,255),
            new Color32(191,255,0,255),
            new Color32(128,255,0,255),
            new Color32(64,255,0,255),
            new Color32(0,255,0,255),
            new Color32(0,255,128,255),
            new Color32(0,255,255,255),
            new Color32(0,128,255,255),
            new Color32(0,0,255,255),
            new Color32(128,0,255,255),
            new Color32(255,0,255,255)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            EnemyDamage(CheckNotes.FullAttack);
        }

        if (CheckNotes.FullAttack >= 5 && AttackgergeImage[0].color.a < 1)
        {
            Color gergeImageColor = AttackgergeImage[0].color;
            gergeImageColor.a = 1f;
            gergeImageColor = new Color(0f, 1f, 0f);
            AttackgergeImage[0].color = gergeImageColor;
        } 
        else if (CheckNotes.FullAttack >= 10 && AttackgergeImage[1].color.a < 1)
        {
            Color gergeImageColor = AttackgergeImage[1].color;
            gergeImageColor.a = 1f;
            gergeImageColor = new Color(0f, 1f, 0f);
            AttackgergeImage[1].color = gergeImageColor;
        }
        else if (CheckNotes.FullAttack >= 15 && AttackgergeImage[2].color.a < 1)
        {
            Color gergeImageColor = AttackgergeImage[2].color;
            gergeImageColor.a = 1f;
            gergeImageColor = new Color(0f, 1f, 0f);
            AttackgergeImage[2].color = gergeImageColor;
        }

        if(GergeMAX)
        {
            GergeTimer += Time.deltaTime;
            if(GergeTimer >= 0.5f)
            {
                GergeTimer = 0;
                for(int i = 0;i < 15;i++)
                {
                    CurrenMoveColor++;
                    if (i + CurrenMoveColor >= 15)
                    {
                        
                    }
                }
            }
        }
    }


    void EnemyDamage(int Damage)
    {
        enemyHP.TakeDamage(Damage);
    }
}
