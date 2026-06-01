using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite ChangeSprite;

    public void OnChangeSprite()
    {
        targetImage.sprite = ChangeSprite;
    }
}