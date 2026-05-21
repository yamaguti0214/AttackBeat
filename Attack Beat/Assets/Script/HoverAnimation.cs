using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;

    [SerializeField]
    private string animationName = "Idle";

    void Start()
    {
        animator.Play(animationName, 0, 0f);
        animator.speed = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.speed = 1;
        Debug.Log("アニメーション");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play(animationName, 0, 0f);
        animator.speed = 0;
    }
}