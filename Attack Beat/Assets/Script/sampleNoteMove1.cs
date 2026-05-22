using UnityEngine;

public class sampleNoteMove1 : MonoBehaviour
{
    public float speed = 5f;

    // ↓ ここを public と true に変更しました
    public bool canMove = true; 
    
    private bool isHit = false;
    private float hitTimer = 0f;

    private Vector3 hitStartPos;
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
            float t = Mathf.Clamp01(hitTimer / 0.7f);

            float x = Mathf.Lerp(0f, 12f, t);
            float y = 5f * 4f * t * (1f - t);

            transform.position = hitStartPos + new Vector3(x, y, 0f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            transform.Rotate(0f, 0f, 900f * Time.deltaTime);

            if (sr != null)
            {
                Color c = sr.color;
                c.a = Mathf.Lerp(1f, 0f, t);
                sr.color = c;
            }

            if (t >= 1f)
            {
                Destroy(gameObject);
            }

            return;
        }

        if (canMove)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public void StartMove()
    {
        canMove = true;
    }

    public void Hit()
    {
        if (isHit) return;

        isHit = true;
        hitTimer = 0f;
        hitStartPos = transform.position;
        targetScale = startScale * 1.5f;

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