using UnityEngine;

public class BossMusicTrigger : MonoBehaviour
{
    [SerializeField] private bool triggerOnlyOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerOnlyOnce && hasTriggered)
        {
            return;
        }

        bool isPlayer =
            other.CompareTag("Player") ||
            other.GetComponent<PlayerHealth>() != null ||
            other.GetComponentInParent<PlayerHealth>() != null;

        if (!isPlayer)
        {
            return;
        }

        hasTriggered = true;

        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayBossMusic();
        }
    }
}