using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float timing;

    public float speed;

    public Transform judgePoint;

    public AudioSource musicSource;

    void Update()
    {
        // 現在の曲時間
        float currentTime = musicSource.time;

        // 判定時間まで残り何秒か
        float remainTime = timing - currentTime;

        // 判定ラインからどれだけ離れるか
        float distance = remainTime * speed;

        // 座標更新
        transform.position =
            judgePoint.position + Vector3.right * distance;

        Debug.Log(
            "note timing: " + timing +
            " / current: " + currentTime +
            " / remain: " + remainTime +
            " / distance: " + distance +
            " / pos: " + transform.position
        );
    }
}