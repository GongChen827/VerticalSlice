using System.Collections;
using TMPro;
using UnityEngine;

public class GameMessageUI : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float defaultShowTime = 3f;

    private Coroutine currentMessageRoutine;

    public void ShowMessage(string message)
    {
        ShowMessageForSeconds(message, defaultShowTime);
    }

    public void ShowMessageForSeconds(string message, float seconds)
    {
        if (currentMessageRoutine != null)
        {
            StopCoroutine(currentMessageRoutine);
        }

        currentMessageRoutine = StartCoroutine(MessageRoutine(message, seconds));
    }

    private IEnumerator MessageRoutine(string message, float seconds)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }

        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
        }

        yield return new WaitForSeconds(seconds);

        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }
}