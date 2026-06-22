using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveDataManager;

public class EnemyChange : MonoBehaviour
{
    [SerializeField] private Image EnemyImage;
    [SerializeField] private Sprite[] ChangeSprite = new Sprite[3];
    // Start is called before the first frame update
    public void NightChangeEnemy()
    {
        EnemyImage.sprite = ChangeSprite[0];
        SaveDataManager.SaveDataInstance.Current_enemyType = EnemyType.Night;
        SaveDataManager.SaveDataInstance.Current_enemyPath = "";
    }

    public void GhostChangeEnemy()
    {
        EnemyImage.sprite = ChangeSprite[1];
        SaveDataManager.SaveDataInstance.Current_enemyType = EnemyType.Ghost;
        SaveDataManager.SaveDataInstance.Current_enemyPath = "";
    }

    public void ArmorChangeEnemy()
    {
        EnemyImage.sprite = ChangeSprite[2];
        SaveDataManager.SaveDataInstance.Current_enemyType = EnemyType.Armor;
        SaveDataManager.SaveDataInstance.Current_enemyPath = "";
    }
}
