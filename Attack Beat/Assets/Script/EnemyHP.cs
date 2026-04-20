using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;

    public Image hpBarFill;
    Animator animator;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        UpdateHPBar();
    }

    void Update()
    {
        // スペースキーでダメージ（テスト用）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateHPBar();

        if (currentHP > 0)
        {
            // ダメージアニメ
            animator.SetTrigger("Damage");
        }
        else
        {
            // 死亡アニメ
            animator.SetTrigger("Die");

        }
    }

    void UpdateHPBar()
    {
        hpBarFill.fillAmount = (float)currentHP / maxHP;
    }
}
