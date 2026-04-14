using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float speed = 5f;          // ˆÚ“®‘¬“x
    public float destroyX = -10f;     // ‚±‚±‚æ‚è¶‚És‚Á‚½‚çíœ

    void Update()
    {
        // ¶‚ÉˆÚ“®
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // ˆê’èˆÊ’u‚ğ’´‚¦‚½‚çíœ
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
