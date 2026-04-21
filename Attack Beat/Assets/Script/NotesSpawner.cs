using System.Collections.Generic;
using UnityEngine;

public class NotesSpawner : MonoBehaviour
{

    [System.Serializable]
    public class NoteData
    {
        public float timing;        // 叩く時間
        public int lane;            // レーン番号
        public float speed = 5f;    // 速度
        public GameObject prefab;   // ノーツ見た目
    }

    public List<NoteData> notesData = new List<NoteData>();

    public Transform[] lanePoints;   // 各レーンの出現位置
    public Transform judgePoint;     // 判定位置

    public CheckNotes checkNotes;
    public AudioSource musicSource;

    private int spawnIndex = 0;

    void Update()
    {
        float currentTime = musicSource.time;

        while (spawnIndex < notesData.Count &&
               notesData[spawnIndex].timing - currentTime <= GetSpawnOffset(notesData[spawnIndex]))
        {
            Spawn(notesData[spawnIndex]);
            spawnIndex++;
        }
    }

    void Spawn(NoteData data)
    {
        Transform lane = lanePoints[data.lane];

        GameObject noteObj = Instantiate(data.prefab, lane.position, Quaternion.identity);

        // 移動設定
        NoteMove move = noteObj.GetComponent<NoteMove>();
        if (move != null)
        {
            move.speed = data.speed;
        }

        // 判定に登録
        CheckNotes.Note newNote = new CheckNotes.Note
        {
            Notes = noteObj,
            timing = data.timing,
            isHit = false
        };

        checkNotes.notes.Add(newNote);
    }

    float GetSpawnOffset(NoteData data)
    {
        float distance = Vector3.Distance(lanePoints[data.lane].position, judgePoint.position);
        return distance / data.speed;
    }
}