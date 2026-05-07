using UnityEngine;

public class LightBurstHitDetector : MonoBehaviour
{
    [Header("Light Burst Hitbox")]
    [SerializeField] private float burstRadius = 1.4f;
    [SerializeField] private float burstOffset = 1.0f;
    [SerializeField] private int burstDamage = 1;

    [Header("References")]
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Transform burstPoint;

    public void ActivateLightBurstHitbox()
    {
        Vector2 center = GetBurstCenter();

        Collider2D[] hits = Physics2D.OverlapCircleAll(center, burstRadius);

        Debug.Log("Light Burst activated. Objects hit: " + hits.Length);

        foreach (Collider2D hit in hits)
        {
            DarkBarrier barrier = hit.GetComponentInParent<DarkBarrier>();

            if (barrier != null)
            {
                barrier.OpenBarrier();
            }

            FinalEnemy enemy = hit.GetComponentInParent<FinalEnemy>();

            if (enemy != null)
            {
                enemy.TakeLightDamage(burstDamage);
            }
        }
    }

    private Vector2 GetBurstCenter()
    {
        Vector2 basePosition = transform.position;

        if (burstPoint != null)
        {
            basePosition = burstPoint.position;
        }

        Vector2 direction = Vector2.right;

        if (playerRenderer != null && playerRenderer.flipX)
        {
            direction = Vector2.left;
        }

        return basePosition + direction * burstOffset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetBurstCenter(), burstRadius);
    }
}