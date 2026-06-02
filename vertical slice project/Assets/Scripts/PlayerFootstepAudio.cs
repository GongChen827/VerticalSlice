using UnityEngine;
using Unity.VisualScripting;

public class PlayerFootstepAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource footstepSource;

    [Header("Movement Settings")]
    [SerializeField] private float minMoveDistance = 0.001f;

    [Header("Landing Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float minLandingVelocity = -1.5f;

    [Header("Visual Scripting Variable")]
    [SerializeField] private string groundedVariableName = "isGrounded";

    private bool wasGrounded = false;
    private float previousVelocityY = 0f;
    private Vector3 previousPosition;

    private void Awake()
    {
        if (footstepSource == null)
        {
            footstepSource = GetComponent<AudioSource>();
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (footstepSource != null)
        {
            footstepSource.loop = true;
            footstepSource.playOnAwake = false;
            footstepSource.spatialBlend = 0f;
        }

        previousPosition = transform.position;
    }

    private void Update()
    {
        if (footstepSource == null)
        {
            return;
        }

        bool isGrounded = GetIsGrounded();

        float horizontalMoveDistance = Mathf.Abs(transform.position.x - previousPosition.x);
        bool isMovingHorizontally = horizontalMoveDistance > minMoveDistance;

        HandleFootsteps(isGrounded, isMovingHorizontally);
        HandleLandingSound(isGrounded);

        wasGrounded = isGrounded;

        if (rb != null)
        {
            previousVelocityY = rb.velocity.y;
        }

        previousPosition = transform.position;
    }

    private void HandleFootsteps(bool isGrounded, bool isMovingHorizontally)
    {
        if (isGrounded && isMovingHorizontally)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }

    private void HandleLandingSound(bool isGrounded)
    {
        bool justLanded = !wasGrounded && isGrounded;

        if (justLanded && previousVelocityY <= minLandingVelocity)
        {
            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayLandingSFX();
            }
        }
    }

    private bool GetIsGrounded()
    {
        try
        {
            return Variables.Object(gameObject).Get<bool>(groundedVariableName);
        }
        catch
        {
            return false;
        }
    }
}