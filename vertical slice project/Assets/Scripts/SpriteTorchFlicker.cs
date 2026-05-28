using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpriteTorchFlicker : MonoBehaviour
{
    [SerializeField] private Light2D torchLight;

    [Header("Intensity")]
    [SerializeField] private float baseIntensity = 1.2f;
    [SerializeField] private float intensityFlickerAmount = 0.25f;

    [Header("Size")]
    [SerializeField] private float baseScale = 3f;
    [SerializeField] private float scaleFlickerAmount = 0.2f;

    [Header("Speed")]
    [SerializeField] private float flickerSpeed = 8f;

    private float randomOffset;

    private void Awake()
    {
        if (torchLight == null)
        {
            torchLight = GetComponent<Light2D>();
        }

        randomOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        if (torchLight == null) return;

        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, randomOffset);

        float centeredNoise = (noise - 0.5f) * 2f;

        torchLight.intensity = baseIntensity + centeredNoise * intensityFlickerAmount;

        float newScale = baseScale + centeredNoise * scaleFlickerAmount;
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }
}