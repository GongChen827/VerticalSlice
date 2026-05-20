using UnityEngine;

public class BossArenaTrigger : MonoBehaviour
{
    [Header("Boss References")]
    [SerializeField] private FinalEnemy finalEnemy;
    [SerializeField] private BossHealthBarUI bossHealthBarUI;

    [Header("Trigger Settings")]
    [SerializeField] private bool onlyTriggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onlyTriggerOnce && hasTriggered)
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

        if (finalEnemy != null && bossHealthBarUI != null)
        {
            finalEnemy.SetBossHealthBar(bossHealthBarUI);
            bossHealthBarUI.Show(finalEnemy.CurrentHealth, finalEnemy.MaxHealth);
        }
    }
}