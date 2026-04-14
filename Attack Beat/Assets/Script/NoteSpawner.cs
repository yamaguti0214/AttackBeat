using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab; // ノーツのプレハブ
    public float spawnX = 10f;    // 右端の位置
    public float spawnY = 0f;     // 高さ
    public float interval = 1f;   // 何秒ごとに出すか

    void Start()
    {
        InvokeRepeating(nameof(SpawnNote), 1f, interval);
    }

    void SpawnNote()
    {
        Vector3 pos = new Vector3(spawnX, spawnY, 0);
        Instantiate(notePrefab, pos, Quaternion.identity);
    }
}
