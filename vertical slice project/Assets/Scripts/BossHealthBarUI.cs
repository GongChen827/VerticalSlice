using TMPro;
using UnityEngine;

public class BossHealthBarUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject bossHealthRoot;
    [SerializeField] private RectTransform redFillRect;
    [SerializeField] private TMP_Text bossNameText;
    [SerializeField] private TMP_Text healthText;

    [Header("Text")]
    [SerializeField] private string bossName = "Final Shadow";

    private int currentHealth;
    private int maxHealth;
    private bool isVisible = false;

    private void Start()
    {
        Hide();
    }

    public void Show(int current, int max)
    {
        isVisible = true;

        if (bossHealthRoot != null)
        {
            bossHealthRoot.SetActive(true);
        }

        if (bossNameText != null)
        {
            bossNameText.text = bossName;
        }

        SetHealth(current, max);
    }

    public void SetHealth(int current, int max)
    {
        currentHealth = Mathf.Max(0, current);
        maxHealth = Mathf.Max(1, max);

        float percent = (float)currentHealth / maxHealth;
        percent = Mathf.Clamp01(percent);

        if (redFillRect != null)
        {
            Vector3 scale = redFillRect.localScale;
            scale.x = percent;
            scale.y = 1f;
            scale.z = 1f;
            redFillRect.localScale = scale;
        }

        if (healthText != null)
        {
            healthText.text = currentHealth + " / " + maxHealth;
        }
    }

    public void Hide()
    {
        isVisible = false;

        if (bossHealthRoot != null)
        {
            bossHealthRoot.SetActive(false);
        }
    }

    public bool IsVisible()
    {
        return isVisible;
    }
}