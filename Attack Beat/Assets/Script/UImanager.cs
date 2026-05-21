using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject titleUI;
    public GameObject settingUI;
    public GameObject levelSelectPanel;
    // Start is called before the first frame update
    public void OpenLevelSelect()
    {
        titleUI.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        titleUI.SetActive(true);
        levelSelectPanel.SetActive(false);
    }
    public void OpenSetting()
    {
        titleUI.SetActive(false);
        settingUI.SetActive(true);
    }

    // Update is called once per frame
    public void CloseSetting()
    {
        titleUI.SetActive(true);
        settingUI.SetActive(false);
    }

    public void BackToTitle()
    {
        titleUI.SetActive(true);
        levelSelectPanel.SetActive(false);
    }
}
