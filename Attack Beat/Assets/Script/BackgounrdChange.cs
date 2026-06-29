using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveDataManager;

public class BackgroundChange : MonoBehaviour
{
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Sprite[] ChangeSprite = new Sprite[3];
    // Start is called before the first frame update
    public void DungeonChange()
    {
        BackgroundImage.sprite = ChangeSprite[0];
        SaveDataManager.SaveDataInstance.Current_backgroundType = BackgroundType.Dungeon;
        SaveDataManager.SaveDataInstance.Current_backgroundPath = "";
    }

    public void RuinsChange()
    {
        BackgroundImage.sprite = ChangeSprite[1];
        SaveDataManager.SaveDataInstance.Current_backgroundType = BackgroundType.Ruins;
        SaveDataManager.SaveDataInstance.Current_backgroundPath = "";
    }

    public void CastleChange()
    {
        BackgroundImage.sprite = ChangeSprite[2];
        SaveDataManager.SaveDataInstance.Current_backgroundType = BackgroundType.Castle;
        SaveDataManager.SaveDataInstance.Current_backgroundPath = "";
    }
}
