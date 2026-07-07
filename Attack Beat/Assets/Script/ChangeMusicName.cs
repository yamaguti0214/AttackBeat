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
        if(SaveDataManager.SaveDataInstance != null && SaveDataManager.SaveDataInstance.Current_musicName != null)
        {
            musicName.text = SaveDataManager.SaveDataInstance.Current_musicName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
