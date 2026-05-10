using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [Header("Win UI")]
    [SerializeField] private GameObject winPanel;

    [Header("Player Settings")]
    [SerializeField] private bool stopPlayerOnWin = true;

    private bool hasWon = false;

    private void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        bool isPlayer = other.CompareTag("Player") || other.GetComponent<PlayerHealth>() != null;

        if (!isPlayer)
        {
            return;
        }

        Win(other.gameObject);
    }

    private void Win(GameObject player)
    {
        hasWon = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (stopPlayerOnWin && player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
            }

            ScriptMachine scriptMachine = player.GetComponent<ScriptMachine>();

            if (scriptMachine != null)
            {
                scriptMachine.enabled = false;
            }
        }

        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}