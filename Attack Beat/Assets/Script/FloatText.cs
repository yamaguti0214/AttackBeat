using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatText : MonoBehaviour
{
    public float speed = 2f;      // ìÆÇ≠ë¨Ç≥
    public float height = 10f;   // ìÆÇ≠ïù

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * height;

        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
