using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullAttack : MonoBehaviour
{
    EnemyHP enemyHP;
    [SerializeField] private Image[] AttackgergeImage;

    [SerializeField] private TextMeshProUGUI MAXText;
    [SerializeField] private GameObject MAXTextObject;
    Color[] rainbow;            //ゲージマックスの虹色の配列
    private bool GergeMAX = false;

    private float GergeTimer = 0f;
    private int CurrentMoveColor = 0;
    private int CurrentColor = 0;

    private bool Attack = false;

    //攻撃時の威力の判定
    private int MinimumAttack = 0;
    private int SmallAttack = 99999;
    private int MediumAttack = 99999;
    private int BigAttack = 99999;
    private int MaxAttack = 99999;
    private int OverAttack = 999999;
    [SerializeField] private GameObject AttackEffect;
    // Start is called before the first frame update
    void Start()
    {
        if (MAXText == null && MAXTextObject != null)
        {
            MAXText = MAXTextObject.GetComponent<TextMeshProUGUI>();
        }

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

        SmallAttack = Mathf.FloorToInt(enemyHP.maxHP * 0.3f); 
        MediumAttack = Mathf.FloorToInt(enemyHP.maxHP * 0.5f);
        BigAttack = Mathf.FloorToInt(enemyHP.maxHP * 0.7f);
        MaxAttack = enemyHP.maxHP;
        OverAttack = Mathf.FloorToInt(enemyHP.maxHP * 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            EnemyDamage(CheckNotes.FullAttack);
        }

        if(!GergeMAX)
        {
            if (CheckNotes.FullAttack >= OverAttack / 15 && AttackgergeImage[0].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[0].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[0].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 2&& AttackgergeImage[1].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[1].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[1].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 3 && AttackgergeImage[2].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[2].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[2].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 4&& AttackgergeImage[3].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[3].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[3].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 5 && AttackgergeImage[4].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[4].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[4].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 6 && AttackgergeImage[5].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[5].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[5].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 7&& AttackgergeImage[6].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[6].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[6].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 8 && AttackgergeImage[7].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[7].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[7].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 9 && AttackgergeImage[8].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[8].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[8].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 10&& AttackgergeImage[9].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[9].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[9].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 11 && AttackgergeImage[10].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[10].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[10].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 12 && AttackgergeImage[11].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[11].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[11].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 13 && AttackgergeImage[12].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[12].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[12].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 14 && AttackgergeImage[13].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[13].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[13].color = gergeImageColor;
            }
            else if (CheckNotes.FullAttack >= OverAttack / 15 * 15 && AttackgergeImage[14].color.a < 1)
            {
                Color gergeImageColor = AttackgergeImage[14].color;
                gergeImageColor.a = 1f;
                gergeImageColor = new Color(0f, 1f, 0f);
                AttackgergeImage[14].color = gergeImageColor;
                GergeMAX = true;
                MAXTextObject.SetActive(true);
            }
        }
        else if (GergeMAX)
        {
            //ゲージをレインボーにする
            GergeTimer += Time.deltaTime;
            if(GergeTimer >= 0.04f)
            {
                GergeTimer = 0;
                CurrentMoveColor++;
                if(CurrentMoveColor >= 14)
                {
                    CurrentMoveColor = 0;
                }
                for (int i = 0;i < 15;i++)
                {
                    if (i + CurrentMoveColor >= 15)
                    {
                        CurrentColor = i + CurrentMoveColor - 15;
                    }
                    else
                    {
                        CurrentColor = i + CurrentMoveColor;
                    }
                    AttackgergeImage[i].color = rainbow[CurrentColor];
                    MAXText.color = rainbow[CurrentColor];
                }
            }
        }

        if(SoundPlay.SoundEnd && !Attack)
        {
            Attack = true;

            switch (CheckNotes.FullAttack + 50)
            {
                case int n when n >= OverAttack:
                    CreateEffect(new Color(1f, 0f, 1f));
                    CreateEffect(Color.red);
                    CreateEffect(Color.blue);
                    CreateEffect(Color.green);
                    CreateEffect(Color.gray);
                    Attack = true;
                    break;
                case int n when n >= MaxAttack:
                    CreateEffect(Color.red);
                    CreateEffect(Color.blue);
                    CreateEffect(Color.green);
                    CreateEffect(Color.white);
                    Attack = true;
                    break;
                case int n when n >= BigAttack:
                    CreateEffect(Color.red);
                    CreateEffect(Color.blue);
                    CreateEffect(Color.green);
                    CreateEffect(Color.white);
                    Attack = false;
                    break;
                case int n when n >= MediumAttack:
                    CreateEffect(Color.blue);
                    CreateEffect(Color.green);
                    CreateEffect(Color.white);
                    Attack = true;
                    break;
                case int n when n >= SmallAttack:
                    CreateEffect(Color.green);
                    CreateEffect(Color.white);
                    Attack = true;
                    break;
                default:
                    CreateEffect(Color.white);
                    Attack = true;
                    break;
            }

            //Instantiate(AttackEffect);
            EnemyDamage(CheckNotes.FullAttack);
        }
    }


    void EnemyDamage(int Damage)
    {
        enemyHP.TakeDamage(Damage);
    }

    //攻撃時の威力によって色が変わるエフェクト
    void CreateEffect(Color color)
    {
        GameObject obj =
            Instantiate(AttackEffect);

        ParticleSystem[] psList =
            obj.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem ps in psList)
        {
            var main = ps.main;

            main.startColor = color;
        }
    }
}
