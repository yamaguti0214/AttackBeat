using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public static ChangeImage Instance;
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite ChangeSprite;

    private void Awake()
    {
        Instance = this;
    }
    public void OnChangeSprite()
    {
        targetImage.sprite = ChangeSprite;
        switch(this.name)
        {
            case "Dungeon":
                MusicLoader.BackGroundnum = 1;
                Debug.Log("Dungeon");
                break;
            case "Ruins":
                MusicLoader.BackGroundnum = 2;
                Debug.Log("Ruins");
                break;
            case "Castle":
                MusicLoader.BackGroundnum = 3;
                Debug.Log("Castle");
                break;
        }
    }
}