using System.Collections;
using TMPro;
using UnityEngine;

public class PowerupHintUI : MonoBehaviour
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private float showTime = 3f;

    private bool hasShown = false;

    public void ShowPowerupHint()
    {
        if (hasShown) return;

        hasShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        hintText.text = "Light gained! Try press E to use Light Burst.";
        hintPanel.SetActive(true);
        yield return new WaitForSeconds(showTime);
        hintPanel.SetActive(false);
    }
}