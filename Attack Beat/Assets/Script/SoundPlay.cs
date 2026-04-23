using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    [SerializeField] TextMeshProUGUI CountDownText;

    private bool firstCountDown = false;
    private float CurrentTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!firstCountDown)
        //{
        //    CurrentTimer += Time.deltaTime;
        //    if(5-CurrentTimer >= )

        //}
    }

    public void Soundplay()
    {
       Sound.Play();
    }
}
