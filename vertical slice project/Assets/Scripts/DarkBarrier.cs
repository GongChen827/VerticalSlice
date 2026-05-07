using UnityEngine;

public class DarkBarrier : MonoBehaviour
{
    [SerializeField] private GameObject barrierObject;
    [SerializeField] private GameMessageUI messageUI;
    [SerializeField] private string openedMessage = "The dark barrier fades away.";

    private bool isOpened = false;

    private void Awake()
    {
        if (barrierObject == null)
        {
            barrierObject = gameObject;
        }
    }

    public void OpenBarrier()
    {
        if (isOpened) return;

        isOpened = true;

        if (messageUI != null)
        {
            messageUI.ShowMessage(openedMessage);
        }

        barrierObject.SetActive(false);
    }
}