using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicName;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveDataManager.SaveDataInstance != null &&
            SaveDataManager.SaveDataInstance.MusicName != null)
        {
            musicName.text = SaveDataManager.SaveDataInstance.MusicName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
