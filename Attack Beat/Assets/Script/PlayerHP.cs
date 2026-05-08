using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;

    public Image hpBarFill;
    public GameObject gameOverPanel;

    bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateHPBar();

        if (currentHP >= 50)
        {
            hpBarFill.color = new Color(0f, 1f, 0f);
        }
        else if (currentHP >= 30)
        {

            hpBarFill.color = new Color(1f, 1f, 0f);
        }
        else if(currentHP >= 0)
        {
            hpBarFill.color = new Color(1f, 0f, 0f);
        }

        if (currentHP <= 0)
        {
            isDead = true;
            StartCoroutine(GameOverDelay());
        }
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.5f); 

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    void UpdateHPBar()
    {
        hpBarFill.fillAmount = (float)currentHP / maxHP;
    }
}
