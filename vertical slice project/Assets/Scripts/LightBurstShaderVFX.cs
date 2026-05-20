using System.Collections;
using UnityEngine;

public class LightBurstShaderVFX : MonoBehaviour
{
    [Header("Visual Object")]
    [SerializeField] private GameObject burstVisualObject;
    [SerializeField] private SpriteRenderer burstRenderer;

    [Header("Shader Property Names")]
    [SerializeField] private string alphaPropertyName = "_EffectAlpha";

    [Header("Animation Settings")]
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private Vector3 startScale = new Vector3(0.4f, 0.4f, 1f);
    [SerializeField] private Vector3 endScale = new Vector3(2.4f, 2.4f, 1f);

    private Material runtimeMaterial;
    private Coroutine currentRoutine;

    private void Awake()
    {
        if (burstRenderer != null)
        {
            runtimeMaterial = Instantiate(burstRenderer.material);
            burstRenderer.material = runtimeMaterial;
        }

        if (burstVisualObject != null)
        {
            burstVisualObject.SetActive(false);
        }

        SetAlpha(0f);
    }

    public void PlayLightBurstShaderEffect()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(BurstRoutine());
    }

    private IEnumerator BurstRoutine()
    {
        burstVisualObject.SetActive(true);
        burstVisualObject.transform.localScale = startScale;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;
            t = Mathf.Clamp01(t);

            burstVisualObject.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            float alpha = Mathf.Lerp(maxAlpha, 0f, t);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
        burstVisualObject.SetActive(false);
    }

    private void SetAlpha(float alpha)
    {
        if (runtimeMaterial != null)
        {
            runtimeMaterial.SetFloat(alphaPropertyName, alpha);
        }
    }
}