using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyChangeButton : MonoBehaviour
{
    public static bool IsWaitingForKey = false;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private TextMeshProUGUI leftKeyText;
    [SerializeField] private TextMeshProUGUI rightKeyText;

    private bool waitingLeft;
    private bool waitingRight;

    private void Start()
    {
        UpdateTexts();
    }

    public void ChangeLeftKey()
    {
        if (IsWaitingForKey) return;

        IsWaitingForKey = true;
        waitingLeft = true;

        leftKeyText.text = "...";

        leftButton.interactable = false;
        rightButton.interactable = false;
    }

    public void ChangeRightKey()
    {
        if (IsWaitingForKey) return;

        IsWaitingForKey = true;
        waitingRight = true;

        rightKeyText.text = "...";

        leftButton.interactable = false;
        rightButton.interactable = false;
    }

    private void Update()
    {
        if (!waitingLeft && !waitingRight) return;

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(key))
                continue;

            if (waitingLeft)
            {
                //// ìØÇ∂ÉLÅ[ã÷é~
                //if (key == KeySettingManager.RightKey)
                //    return;

                KeySettingManager.LeftKey = key;
                waitingLeft = false;
            }

            if (waitingRight)
            {
                //// ìØÇ∂ÉLÅ[ã÷é~
                //if (key == KeySettingManager.LeftKey)
                //    return;

                KeySettingManager.RightKey = key;
                waitingRight = false;
            }

            IsWaitingForKey = false;

            leftButton.interactable = true;
            rightButton.interactable = true;

            UpdateTexts();
            break;
        }
    }

    private void UpdateTexts()
    {
        leftKeyText.text = KeySettingManager.LeftKey.ToString();
        rightKeyText.text = KeySettingManager.RightKey.ToString();
    }
}