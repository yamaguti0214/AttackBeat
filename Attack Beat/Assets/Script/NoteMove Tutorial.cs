using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
<<<<<<< HEAD
=======

>>>>>>> 24241e3f33f707f191ff9305dc78bff35c231878
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
<<<<<<< HEAD
    public TextMeshProUGUI goodText; // ★元のGameObject形式に戻しました
=======
    public TextMeshProUGUI goodText;
>>>>>>> 24241e3f33f707f191ff9305dc78bff35c231878

    [Header("次に動かしたいノーツ（Note2など）")]
    public NoteMoveTutorial nextNote;

    [Header("このノーツを叩くためのキー（HやFなど）")]
    public KeyCode hitKey = KeyCode.H;

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

            promptText.text = "Press " + hitKey.ToString() + " To Strike";
            promptText.gameObject.SetActive(true);
        }

        if (!isStopped)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
<<<<<<< HEAD
            // 3. 一時停止中に正しいキーが押されたら
=======
>>>>>>> 24241e3f33f707f191ff9305dc78bff35c231878
            if (Input.GetKeyDown(hitKey))
            {
                promptText.gameObject.SetActive(false);

                // バトンを次のノーツに渡す
                if (nextNote != null)
                {
                    nextNote.enabled = true;
                }

                // 「Good!」テキストを表示する
                goodText.gameObject.SetActive(true);

                // ノーツ（自分自身）の見た目を消す
                GetComponent<SpriteRenderer>().enabled = false;

                // 1秒後に「Good!」テキストを消して、完全にノーツを削除する
                Invoke("ClearGoodEffect", 1.0f);
            }
        }
<<<<<<< HEAD
        // 「Good!」を消して、オブジェクトを完全に消去する
        void ClearGoodEffect()
        {
            if (goodText != null)
            {
                goodText.gameObject.SetActive(false);
            }
            Destroy(gameObject); // ここでノーツを完全に削除
        }
    }
}
=======
    } // ← ここでしっかり Update 関数を終わらせる！

    // ★ Update の外側に引っ越ししました
    // 「Good!」を消して、オブジェクトを完全に消去する
    void ClearGoodEffect()
    {
        if (goodText != null)
        {
            goodText.gameObject.SetActive(false);
        }
        Destroy(gameObject); // ここでノーツを完全に削除
    }
}
>>>>>>> 24241e3f33f707f191ff9305dc78bff35c231878
