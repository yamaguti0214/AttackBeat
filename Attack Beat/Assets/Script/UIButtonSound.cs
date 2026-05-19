using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour,
    IPointerEnterHandler,
    IPointerClickHandler
{
    public AudioSource audioSource;

    public AudioClip hoverSE;
    public AudioClip clickSE;

    // Start is called before the first frame update
    // カーソルを乗せた時
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSE);
    }

    // Update is called once per frame
    // ボタンを押した時
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(clickSE);
    }
}
