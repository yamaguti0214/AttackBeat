using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullAttack : MonoBehaviour
{
    EnemyHP enemyHP;
    [SerializeField] private Image[] AttackgergeImage;
    // Start is called before the first frame update
    void Start()
    {
        enemyHP = FindFirstObjectByType<EnemyHP>();
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
    }


    void EnemyDamage(int Damage)
    {
        enemyHP.TakeDamage(Damage);
    }
}
