using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChange : MonoBehaviour
{
    [SerializeField] private Image EnemyImage;
    [SerializeField] private Sprite[] ChangeSprite = new Sprite[3];
    // Start is called before the first frame update
    public void NightChangeEnemy()
    {
        EnemyImage.sprite = ChangeSprite[0];
        MyMusicNotes_Create.EnemyNum = 1;
    }

    public void GhostChangeEnemy()
    {
        EnemyImage.sprite = ChangeSprite[1];
        MyMusicNotes_Create.EnemyNum = 2;
    }

    public void AromorChangeEnemy()
    {
        EnemyImage.sprite = ChangeSprite[2];
        MyMusicNotes_Create.EnemyNum = 3;
    }
}
