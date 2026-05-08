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

    public float attackInterval = 2f;
    bool isDead = false;

    //  SE—p
    public AudioSource audioSource;
    public AudioClip attack1SE;
    public AudioClip attack2SE;
    public AudioClip attack3SE;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        UpdateHPBar();

        StartCoroutine(AttackLoop());
    }

    //  Animation Event‚©‚çŒÄ‚Î‚ê‚é
    public void PlayAttack1SE()
    {
        audioSource.PlayOneShot(attack1SE);
    }

    public void PlayAttack2SE()
    {
        audioSource.PlayOneShot(attack2SE);
    }

    public void PlayAttack3SE()
    {
        audioSource.PlayOneShot(attack3SE);
    }

    IEnumerator AttackLoop()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(attackInterval);

            int rand = Random.Range(0, 3);

            if (rand == 0)
                animator.SetTrigger("Attack1");
            else if (rand == 1)
                animator.SetTrigger("Attack2");
            else
                animator.SetTrigger("Attack3");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateHPBar();

        if (currentHP > 0)
        {
            animator.SetTrigger("Damage");
        }
        else
        {
            isDead = true;
            animator.SetTrigger("Die");
        }
    }

    void UpdateHPBar()
    {
        hpBarFill.fillAmount = (float)currentHP / maxHP;
    }
}