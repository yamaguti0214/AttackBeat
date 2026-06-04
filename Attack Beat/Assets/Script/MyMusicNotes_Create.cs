using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMusicNotes_Create : MonoBehaviour
{
    public AudioClip MusicClip;
    public static int EnemyNum;
    // Start is called before the first frame update
    void Start()
    {
        MusicClip = MusicLoader.Instance.audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
