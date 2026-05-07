using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private GameObject closedExit;
    [SerializeField] private GameObject openExit;
    [SerializeField] private Collider2D blockingCollider;
    [SerializeField] private GameMessageUI messageUI;
    [SerializeField] private string exitOpenedMessage = "The final exit is opened!";

    private bool isOpen = false;

    private void Start()
    {
        if (closedExit != null)
        {
            closedExit.SetActive(true);
        }

        if (openExit != null)
        {
            openExit.SetActive(false);
        }

        if (blockingCollider != null)
        {
            blockingCollider.enabled = true;
        }
    }

    public void OpenExit()
    {
        if (isOpen) return;

        isOpen = true;

        if (closedExit != null)
        {
            closedExit.SetActive(false);
        }

        if (openExit != null)
        {
            openExit.SetActive(true);
        }

        if (blockingCollider != null)
        {
            blockingCollider.enabled = false;
        }

        if (messageUI != null)
        {
            messageUI.ShowMessage(exitOpenedMessage);
        }
    }
}