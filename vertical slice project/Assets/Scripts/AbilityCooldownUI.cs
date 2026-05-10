using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private GameObject playerObject;

    [Header("UI References")]
    [SerializeField] private GameObject abilityPanel;
    [SerializeField] private Image abilityIconImage;
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text keyText;

    [Header("Visual Scripting Object Variable Names")]
    [SerializeField] private string hasPowerVariableName = "hasPower";
    [SerializeField] private string abilityReadyVariableName = "abilityReady";
    [SerializeField] private string cooldownTimerVariableName = "cooldownTimer";
    [SerializeField] private string abilityCooldownVariableName = "abilityCooldown";

    [Header("Display Settings")]
    [SerializeField] private string keyLabel = "E";
    [SerializeField] private string readyLabel = "READY";
    [SerializeField] private Color readyIconColor = Color.white;
    [SerializeField] private Color cooldownIconColor = new Color(0.45f, 0.45f, 0.45f, 1f);

    private void Start()
    {
        if (keyText != null)
        {
            keyText.text = keyLabel;
        }

        if (abilityPanel != null)
        {
            abilityPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerObject == null)
        {
            return;
        }

        bool hasPower = false;
        bool gotHasPower = TryGetObjectVariable(hasPowerVariableName, out hasPower);

        if (!gotHasPower || !hasPower)
        {
            if (abilityPanel != null && abilityPanel.activeSelf)
            {
                abilityPanel.SetActive(false);
            }

            return;
        }

        if (abilityPanel != null && !abilityPanel.activeSelf)
        {
            abilityPanel.SetActive(true);
        }

        bool abilityReady = true;
        float cooldownTimer = 0f;
        float abilityCooldown = 1f;

        TryGetObjectVariable(abilityReadyVariableName, out abilityReady);
        TryGetObjectVariable(cooldownTimerVariableName, out cooldownTimer);
        TryGetObjectVariable(abilityCooldownVariableName, out abilityCooldown);

        if (abilityCooldown <= 0f)
        {
            abilityCooldown = 1f;
        }

        bool isCoolingDown = !abilityReady || cooldownTimer > 0.05f;

        if (isCoolingDown)
        {
            ShowCooldown(cooldownTimer, abilityCooldown);
        }
        else
        {
            ShowReady();
        }
    }

    private void ShowReady()
    {
        if (abilityIconImage != null)
        {
            abilityIconImage.color = readyIconColor;
        }

        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0f;
            cooldownOverlay.enabled = false;
        }

        if (cooldownText != null)
        {
            cooldownText.text = readyLabel;
        }
    }

    private void ShowCooldown(float cooldownTimer, float abilityCooldown)
    {
        if (abilityIconImage != null)
        {
            abilityIconImage.color = cooldownIconColor;
        }

        if (cooldownOverlay != null)
        {
            cooldownOverlay.enabled = true;
            cooldownOverlay.fillAmount = Mathf.Clamp01(cooldownTimer / abilityCooldown);
        }

        if (cooldownText != null)
        {
            int secondsLeft = Mathf.CeilToInt(cooldownTimer);
            cooldownText.text = secondsLeft.ToString();
        }
    }

    private bool TryGetObjectVariable<T>(string variableName, out T value)
    {
        value = default;

        if (playerObject == null || string.IsNullOrEmpty(variableName))
        {
            return false;
        }

        try
        {
            object rawValue = Variables.Object(playerObject).Get(variableName);

            if (rawValue is T typedValue)
            {
                value = typedValue;
                return true;
            }

            if (rawValue != null)
            {
                value = (T)Convert.ChangeType(rawValue, typeof(T));
                return true;
            }
        }
        catch
        {
            return false;
        }

        return false;
    }
}