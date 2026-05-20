using System.Collections;
using UnityEngine;

public class FinalEnemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;

    [Header("Movement")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Damage")]
    [SerializeField] private int contactDamage = 1;

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private Color hitFlashColor = Color.red;
    [SerializeField] private float hitFlashDuration = 0.12f;

    [Header("Animation")]
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private string speedParameter = "Speed";
    [SerializeField] private string hitTrigger = "Hit";
    [SerializeField] private string dieTrigger = "Die";

    [Header("Death")]
    [SerializeField] private ExitController exitController;
    [SerializeField] private float disappearDelay = 0.8f;

    [Header("Boss Health UI")]
    [SerializeField] private BossHealthBarUI bossHealthBarUI;

    private int currentHealth;
    private bool isDead = false;
    private Transform currentTarget;
    private Color originalColor;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (bodyRenderer == null)
        {
            bodyRenderer = GetComponent<SpriteRenderer>();
        }

        if (bodyRenderer != null)
        {
            originalColor = bodyRenderer.color;
        }

        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        currentTarget = rightPoint;

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.SetHealth(currentHealth, maxHealth);
        }
    }

    private void Update()
    {
        if (isDead) return;

        Patrol();
    }

    private void Patrol()
    {
        if (leftPoint == null || rightPoint == null)
        {
            if (enemyAnimator != null)
            {
                enemyAnimator.SetFloat(speedParameter, 0f);
            }

            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            moveSpeed * Time.deltaTime
        );

        if (enemyAnimator != null)
        {
            enemyAnimator.SetFloat(speedParameter, moveSpeed);
        }

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.05f)
        {
            if (currentTarget == rightPoint)
            {
                currentTarget = leftPoint;
                FaceLeft();
            }
            else
            {
                currentTarget = rightPoint;
                FaceRight();
            }
        }
    }

    private void FaceLeft()
    {
        if (bodyRenderer != null)
        {
            bodyRenderer.flipX = false;
        }
    }

    private void FaceRight()
    {
        if (bodyRenderer != null)
        {
            bodyRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        PlayerHealth playerHealth =
            other.GetComponent<PlayerHealth>() ??
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return;

        PlayerHealth playerHealth =
            other.GetComponent<PlayerHealth>() ??
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
    }

    public void SetBossHealthBar(BossHealthBarUI healthBar)
    {
        bossHealthBarUI = healthBar;

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeLightDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("Final Enemy hit. Current health: " + currentHealth);

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.SetHealth(currentHealth, maxHealth);
        }

        StartCoroutine(HitFlashRoutine());

        if (enemyAnimator != null && currentHealth > 0)
        {
            enemyAnimator.SetTrigger(hitTrigger);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitFlashRoutine()
    {
        if (bodyRenderer == null) yield break;

        bodyRenderer.color = hitFlashColor;

        yield return new WaitForSeconds(hitFlashDuration);

        if (!isDead && bodyRenderer != null)
        {
            bodyRenderer.color = originalColor;
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Final Enemy defeated.");

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.SetHealth(0, maxHealth);
        }

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger(dieTrigger);
        }

        if (exitController != null)
        {
            exitController.OpenExit();
        }

        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.Hide();
        }

        gameObject.SetActive(false);
    }
}