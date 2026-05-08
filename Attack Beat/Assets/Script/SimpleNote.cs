using UnityEngine;

public class SimpleNote : MonoBehaviour
{
    public float speed = 2f;
    public float destroyX = -12f;

    private bool isHit = false;
    private float hitTimer = 0f;

    private Vector3 hitStartPos;
    private Vector3 hitTargetPos;
    private Vector3 startScale;
    private Vector3 targetScale;

    private SpriteRenderer sr;
    private Collider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        startScale = transform.localScale;

        if (sr != null)
        {
            sr.sortingOrder = 10;
        }
    }

    void Update()
    {
        if (isHit)
        {
            hitTimer += Time.deltaTime;
            float t = Mathf.Clamp01(hitTimer / 0.35f);
            float eased = 1f - Mathf.Pow(1f - t, 3f);

            // 上方向だけに飛ばす
            Vector3 pos = Vector3.Lerp(hitStartPos, hitTargetPos, eased);
            transform.position = pos;

            // 少し大きくする
            transform.localScale = Vector3.Lerp(startScale, targetScale, eased);

            // 少しずつ薄くする
            if (sr != null)
            {
                Color c = sr.color;
                c.a = Mathf.Lerp(1f, 0f, eased);
                sr.color = c;
            }

            if (t >= 1f)
            {
                Destroy(gameObject);
            }

            return;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        if (isHit) return;

        isHit = true;
        hitTimer = 0f;

        Debug.Log("HIT: " + gameObject.name);

        hitStartPos = transform.position;

        // 右上ではなく、上にだけ飛ばす
        hitTargetPos = hitStartPos + new Vector3(0f, 1.8f, 0f);

        targetScale = startScale * 1.35f;

        if (col != null)
        {
            col.enabled = false;
        }

        if (sr != null)
        {
            sr.sortingOrder = 50;
        }
    }
}