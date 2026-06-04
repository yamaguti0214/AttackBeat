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
    }
}