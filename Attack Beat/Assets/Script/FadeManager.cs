using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        // シーン開始時にフェードイン
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LoadScene("Check_Yamaguchi");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);

        // 次のシーンでFadeInさせるため
    }

    IEnumerator FadeOut()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = time / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeIn()
    {
        float time = fadeDuration;

        while (time > 0f)
        {
            float alpha = time / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);

            time -= Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
    }
}