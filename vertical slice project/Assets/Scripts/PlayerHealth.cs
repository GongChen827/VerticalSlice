using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 7;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float deathY = -10f;

    [Header("UI References")]
    [SerializeField] private Image[] heartImages;
    [SerializeField] private GameObject deathPanel;

    [Header("Heart Colors")]
    [SerializeField] private Color fullHeartColor = new Color(1f, 0.23f, 0.23f, 1f);
    [SerializeField] private Color emptyHeartColor = new Color(0.15f, 0.15f, 0.15f, 1f);

    [Header("Hurt Flash")]
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private Color hurtFlashColor = Color.red;
    [SerializeField] private float hurtFlashDuration = 0.12f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private bool isDead = false;

    private ScriptMachine scriptMachine;
    private Rigidbody2D rb;
    private Coroutine flashRoutine;

    private void Awake()
    {
        scriptMachine = GetComponent<ScriptMachine>();
        rb = GetComponent<Rigidbody2D>();

        if (bodyRenderer == null)
        {
            bodyRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDead) return;

        if (transform.position.y < deathY)
        {
            KillPlayerInstantly();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Hazard"))
        {
            TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage || isDead) return;

        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHeartsUI();
        TriggerHurtFlash();

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(DamageCooldown());
    }

    public void KillPlayerInstantly()
    {
        if (isDead) return;

        currentHealth = 0;
        UpdateHeartsUI();
        Die();
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityDuration);
        canTakeDamage = true;
    }

    private void TriggerHurtFlash()
    {
        if (bodyRenderer == null) return;

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(HurtFlashRoutine());
    }

    private IEnumerator HurtFlashRoutine()
    {
        Color originalColor = bodyRenderer.color;
        bodyRenderer.color = hurtFlashColor;
        yield return new WaitForSeconds(hurtFlashDuration);

        if (!isDead && bodyRenderer != null)
        {
            bodyRenderer.color = originalColor;
        }
    }

    private void Die()
    {
        isDead = true;
        canTakeDamage = false;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        if (scriptMachine != null)
        {
            scriptMachine.enabled = false;
        }

        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }

    private void UpdateHeartsUI()
    {
        if (heartImages == null || heartImages.Length == 0) return;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] == null) continue;

            if (i < currentHealth)
            {
                heartImages[i].color = fullHeartColor;
            }
            else
            {
                heartImages[i].color = emptyHeartColor;
            }
        }
    }
}