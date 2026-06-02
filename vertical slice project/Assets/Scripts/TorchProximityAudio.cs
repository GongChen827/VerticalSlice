using UnityEngine;

public class TorchProximityAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource torchAudioSource;
    [SerializeField] private Transform player;

    [Header("Distance Settings")]
    [SerializeField] private float maxHearDistance = 5f;
    [SerializeField] private float fullVolumeDistance = 1.5f;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float maxVolume = 0.35f;

    [SerializeField] private float fadeSpeed = 5f;

    private void Awake()
    {
        if (torchAudioSource == null)
        {
            torchAudioSource = GetComponent<AudioSource>();
        }

        if (torchAudioSource != null)
        {
            torchAudioSource.loop = true;
            torchAudioSource.playOnAwake = false;
            torchAudioSource.spatialBlend = 0f;
            torchAudioSource.volume = 0f;
        }
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (torchAudioSource == null || player == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        float targetVolume = CalculateVolume(distance);

        torchAudioSource.volume = Mathf.MoveTowards(
            torchAudioSource.volume,
            targetVolume,
            fadeSpeed * Time.deltaTime
        );

        if (targetVolume > 0f)
        {
            if (!torchAudioSource.isPlaying)
            {
                torchAudioSource.Play();
            }
        }
        else
        {
            if (torchAudioSource.isPlaying && torchAudioSource.volume <= 0.01f)
            {
                torchAudioSource.Stop();
            }
        }
    }

    private float CalculateVolume(float distance)
    {
        if (distance >= maxHearDistance)
        {
            return 0f;
        }

        if (distance <= fullVolumeDistance)
        {
            return maxVolume;
        }

        float fadeRange = maxHearDistance - fullVolumeDistance;
        float distancePastFullVolume = distance - fullVolumeDistance;

        float fadePercent = 1f - (distancePastFullVolume / fadeRange);

        return Mathf.Clamp01(fadePercent) * maxVolume;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fullVolumeDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxHearDistance);
    }
}