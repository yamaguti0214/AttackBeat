using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;

    public void SelectBackground()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "”wŒi‰æ‘œ‚ð‘I‘ð",
            "",
            new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
            },
            false);

        if (paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
            return;

        string path = paths[0];

        byte[] imageData = File.ReadAllBytes(path);

        Texture2D texture = new Texture2D(2, 2);

        if (texture.LoadImage(imageData))
        {
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            backgroundImage.sprite = sprite;

            if (this.name == "MyEnemy")
            {
                MusicLoader.Enemynum = 0;
                MusicLoader.ChoiceEnemy = sprite;
            }
            else if (this.name == "MyBackground")
            {
                MusicLoader.BackGroundnum = 0;
                MusicLoader.ChoiceBackGround = sprite;
            }
        }
    }
}