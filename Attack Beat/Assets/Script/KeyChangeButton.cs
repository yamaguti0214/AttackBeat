using TMPro;
using UnityEngine;

public class KeyChangeButton : MonoBehaviour
{
    public enum KeyType
    {
        Left,
        Right
    }

    [SerializeField] private KeyType keyType;
    [SerializeField] private TextMeshProUGUI keyText;

    private bool waitingForKey = false;

    private void Start()
    {
        UpdateText();
    }

    public void ChangeKey()
    {
        waitingForKey = true;
        keyText.text = "キーを押してください";
    }

    private void Update()
    {
        if (!waitingForKey) return;

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (keyType == KeyType.Left)
                {
                    KeySettingManager.LeftKey = key;
                }
                else
                {
                    KeySettingManager.RightKey = key;
                }

                waitingForKey = false;
                UpdateText();
                break;
            }
        }
    }

    private void UpdateText()
    {
        if (keyType == KeyType.Left)
        {
            keyText.text = "左キー : " + KeySettingManager.LeftKey;
        }
        else
        {
            keyText.text = "右キー : " + KeySettingManager.RightKey;
        }
    }
}