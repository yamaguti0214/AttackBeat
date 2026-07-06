using UnityEngine;

public class ButtonSE : MonoBehaviour
{
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip clickSE;

    public void PlaySE()
    {
        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE);
        }
    }
}