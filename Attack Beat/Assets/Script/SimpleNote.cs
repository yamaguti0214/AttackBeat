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

        // ノーツを前面表示
        if (sr != null)
        {
            sr.sortingOrder = 10;
        }
    }

    void Update()
    {
        // ヒット後
        if (isHit)
        {
            hitTimer += Time.deltaTime;

            float t = Mathf.Clamp01(hitTimer / 0.45f);

            // なめらか補間
            float eased = 1f - Mathf.Pow(1f - t, 3f);

            // 右上方向へ
            Vector3 pos =
                Vector3.Lerp(hitStartPos, hitTargetPos, eased);

            // 放物線っぽく落とす
            pos.y -= 5f * t * t;

            transform.position = pos;

            // 少し拡大
            transform.localScale =
                Vector3.Lerp(startScale, targetScale, eased);

            // フェードアウト
            if (sr != null)
            {
                Color c = sr.color;

                c.a = Mathf.Lerp(1f, 0f, eased);

                sr.color = c;
            }

            // 終了
            if (t >= 1f)
            {
                Destroy(gameObject);
            }

            return;
        }

        // 通常移動
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 画面外削除
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        // 二重ヒット防止
        if (isHit) return;

        isHit = true;
        hitTimer = 0f;

        Debug.Log("HIT : " + gameObject.name);

        hitStartPos = transform.position;

        // 🔥 大きく右上へ飛ばす
        hitTargetPos =
            hitStartPos + new Vector3(7f, 6f, 0f);

        // 少し大きくする
        targetScale = startScale * 1.45f;

        // 当たり判定OFF
        if (col != null)
        {
            col.enabled = false;
        }

        // 最前面へ
        if (sr != null)
        {
            sr.sortingOrder = 50;
        }
    }
}