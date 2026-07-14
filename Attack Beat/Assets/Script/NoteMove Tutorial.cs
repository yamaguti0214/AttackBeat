using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteMoveTutorial : MonoBehaviour
{
    [Header("ノーツの移動速度")]
    public float speed = 5.0f;

    [Header("白い丸の画像（GameObject）")]
    public Transform targetTarget;

    [Header("停止する判定の広さ")]
    public float stopRange = 0.1f;

    [Header("プロンプトテキスト（叩くキーの案内）")]
    public TextMeshProUGUI promptText;

    [Header("成功テキスト（Good!）")]
    public TextMeshProUGUI goodText;

    [Header("次に動かしたいノーツ（Note2など）")]
    public NoteMoveTutorial nextNote;

    [Header("攻撃する敵のHPスクリプト")]
    public TutorialGoblinH enemyHP; // 新しく作ったGoblinHealthをセットする枠

    [Header("このノーツを叩くためのキー（HやFなど）")]
    public KeyCode hitKey = KeyCode.H;

    [Header("ロングノーツ用：FとHの両方で打てるようにする？")]
    public bool allowBothFandH = false;

    private bool isStopped = false;

    void Update()
    {
        if (targetTarget == null || promptText == null || goodText == null) return;

        float targetX = targetTarget.position.x;
        float currentX = transform.position.x;
        float distance = Mathf.Abs(currentX - targetX);

        if (!isStopped && distance <= stopRange)
        {
            isStopped = true;
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

            // ★ロングノーツ（FかH両方可）の時は、案内テキストを両方対応の表記に変える
            if (allowBothFandH)
            {
                promptText.text = "Press F Or H To Strike";
            }
            else
            {
                promptText.text = "Press " + hitKey.ToString() + " To Strike";
            }
            promptText.gameObject.SetActive(true);
        }

        if (!isStopped)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            // キーが正しく押されたか判定するフラグ
            bool isKeyHit = false;

            // ロングノーツ用設定がオンの場合は「F」または「H」のどちらでも反応する
            if (allowBothFandH)
            {
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.H))
                {
                    isKeyHit = true;
                }
            }
            else
            {
                // 通常のノーツは、インスペクターで指定した hitKey のみ反応する
                if (Input.GetKeyDown(hitKey))
                {
                    isKeyHit = true;
                }
            }

            // 正しい入力があった場合の処理
            if (isKeyHit)
            {
                promptText.gameObject.SetActive(false);

                // ★ここを追加：敵がインスペクターに登録されていればダメージ（例: 10）を与える
                if (enemyHP != null)
                {
                    enemyHP.TakeDamage(10);
                }

                // バトンを次のノーツに渡す
                if (nextNote != null)
                {
                    nextNote.enabled = true;
                }

                // 「Good!」テキストを表示する
                goodText.gameObject.SetActive(true);

                // ノーツ（子オブジェクトも含めて）の見た目を消す
                SetChildrenActive(false);

                // 1秒後に「Good!」テキストを消して、完全にノーツを削除する
                Invoke("ClearGoodEffect", 1.0f);
            }
        }
    } // Update の終わり

    // 「Good!」を消して、オブジェクトを完全に消去する
    void ClearGoodEffect()
    {
        if (goodText != null)
        {
            goodText.gameObject.SetActive(false);
        }
        Destroy(gameObject); // ここでノーツを完全に削除
    }

    // ノーツの見た目を（子オブジェクトも含めて）一括で非表示にするための関数
    void SetChildrenActive(bool active)
    {
        // もし自分自身に画像があれば消す
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = active;
        }

        // 中に入っている子オブジェクト（Head, Tail, Bodyなど）の画像もすべて消す
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = active;
        }
    }
}