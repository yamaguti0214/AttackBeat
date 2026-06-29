using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Notes_Create : MonoBehaviour
{
    [System.Serializable]
    public class NoteInput
    {
        public float timing;
        public int lane;
        public float length; // 追加
    }

    [System.Serializable]
    public class SaveData
    {
        public List<NoteInput> notes;
    }

    public Transform spawnPoint;
    public CheckNotes checkNotes;
    public Transform judgePoint;

    // Element 0: 青, Element 1: 緑, Element 2: 連打（共通）
    public GameObject[] notePrefabs;
    public float speed = 5f;
    public AudioSource musicSource;

    [SerializeField] private string jsonFileName = "notes_song2.json";

    private List<NoteInput> notes = new List<NoteInput>();
    private int spawnIndex = 0;
    string path;

    void Start()
    {
        if(SaveDataManager.SaveDataInstance != null)
        {
            jsonFileName = SaveDataManager.SaveDataInstance.Current_musicName;
        }

        string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        path = Path.Combine(desktop, jsonFileName);
        Load();
    }

    void Update()
    {
        float currentTime = musicSource.time;

        while (spawnIndex < notes.Count &&
               notes[spawnIndex].timing - currentTime <= GetSpawnOffset())
        {
            Spawn(notes[spawnIndex]);
            spawnIndex++;
        }
    }

    void Spawn(NoteInput data)
    {
        int prefabIndex = Mathf.Clamp(data.lane, 0, notePrefabs.Length - 1);
        GameObject selectedPrefab = notePrefabs[prefabIndex];

        GameObject note = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);

        // --- Notes_Create.cs の Spawn メソッド内の該当部分を以下に差し替え ---

        // 連打ノーツ（length > 0）の場合の見た目引き伸ばし処理
        if (data.length > 0f)
        {
            Transform bodyTransform = note.transform.Find("Body");
            Transform tailTransform = note.transform.Find("Tail");

            if (bodyTransform != null && tailTransform != null)
            {
                // 速度5f × 長さ2秒 = 5ユニット分（横幅）
                float visualLength = speed * data.length;

                // 【1. 胴体をX軸（横方向）に伸ばす】
                bodyTransform.localScale = new Vector3(visualLength, bodyTransform.localScale.y, bodyTransform.localScale.z);

                // 【2. 胴体の位置を調整】
                // ノーツが「右から左」に進む場合、後ろ側（右側）に伸びてほしいので、
                // 伸びた分の半分（+方向）にずらして始点（Head）の位置をキープします
                bodyTransform.localPosition = new Vector3(visualLength / 2f, 0f, 0f);

                // 【3. 終点（Tail）を胴体の最果て（右側）に配置する】
                tailTransform.localPosition = new Vector3(visualLength, 0f, 0f);
            }
        }

        // 移動スクリプトへのデータ受け渡し
        NoteMove move = note.GetComponent<NoteMove>();
        if (move != null)
        {
            move.speed = speed;
            move.judgePoint = judgePoint;
            move.musicSource = musicSource;
            move.timing = data.timing;
        }

        // 判定用スクリプトに登録
        CheckNotes.Note newNote = new CheckNotes.Note
        {
            Notes = note,
            timing = data.timing,
            length = data.length, // 追加
            lane = data.lane,     // 追加
            isHit = false
        };

        checkNotes.notes.Add(newNote);
    }

    float GetSpawnOffset()
    {
        float distance = Vector3.Distance(spawnPoint.position, judgePoint.position);
        return distance / speed;
    }

    void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            notes = data.notes;
            Debug.Log("譜面読み込み: " + notes.Count + " (Path: " + path + ")");
        }
        else
        {
            Debug.Log("譜面が見つからん: " + path);
        }
    }
}