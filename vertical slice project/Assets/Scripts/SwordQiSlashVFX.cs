using System.Collections;
using UnityEngine;

public class SwordQiSlashVFX : MonoBehaviour
{
    [Header("Slash Visual")]
    [SerializeField] private GameObject slashVisualObject;
    [SerializeField] private SpriteRenderer slashRenderer;

    [Header("Position References")]
    [SerializeField] private Transform slashStartPoint;
    [SerializeField] private SpriteRenderer playerRenderer;

    [Header("Shader Property")]
    [SerializeField] private string alphaPropertyName = "_EffectAlpha";

    [Header("Timing")]
    [SerializeField] private float slashDelay = 0.08f;
    [SerializeField] private float slashDuration = 0.22f;

    [Header("Movement")]
    [SerializeField] private float travelDistance = 2.8f;
    [SerializeField] private float startForwardOffset = 0.2f;
    [SerializeField] private float verticalOffset = 0.15f;

    [Header("Scale")]
    [SerializeField] private Vector3 startScale = new Vector3(1.1f, 0.55f, 1f);
    [SerializeField] private Vector3 endScale = new Vector3(1.8f, 0.85f, 1f);

    [Header("Alpha")]
    [SerializeField] private float maxAlpha = 1f;

    private Material runtimeMaterial;
    private Coroutine slashRoutine;

    private void Awake()
    {
        if (slashRenderer != null)
        {
            runtimeMaterial = Instantiate(slashRenderer.material);
            slashRenderer.material = runtimeMaterial;
        }

        if (slashVisualObject != null)
        {
            slashVisualObject.SetActive(false);
        }

        SetAlpha(0f);
    }

    public void PlaySwordQiSlash()
    {
        if (slashRoutine != null)
        {
            StopCoroutine(slashRoutine);
        }

        slashRoutine = StartCoroutine(SlashRoutine());
    }

    private IEnumerator SlashRoutine()
    {
        yield return new WaitForSeconds(slashDelay);

        Vector2 direction = GetFacingDirection();

        Vector3 baseStartPosition = transform.position;

        if (slashStartPoint != null)
        {
            baseStartPosition = slashStartPoint.position;
        }

        Vector3 startPosition = baseStartPosition 
            + (Vector3)(direction * startForwardOffset)
            + new Vector3(0f, verticalOffset, 0f);

        Vector3 endPosition = startPosition + (Vector3)(direction * travelDistance);

        slashVisualObject.SetActive(true);
        slashVisualObject.transform.SetParent(null);
        slashVisualObject.transform.position = startPosition;

        Vector3 actualStartScale = startScale;
        Vector3 actualEndScale = endScale;

        if (direction.x < 0f)
        {
            actualStartScale.x = -Mathf.Abs(actualStartScale.x);
            actualEndScale.x = -Mathf.Abs(actualEndScale.x);
        }
        else
        {
            actualStartScale.x = Mathf.Abs(actualStartScale.x);
            actualEndScale.x = Mathf.Abs(actualEndScale.x);
        }

        slashVisualObject.transform.localScale = actualStartScale;
        SetAlpha(maxAlpha);

        float timer = 0f;

        while (timer < slashDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / slashDuration);

            slashVisualObject.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            slashVisualObject.transform.localScale = Vector3.Lerp(actualStartScale, actualEndScale, t);

            float alpha = Mathf.Lerp(maxAlpha, 0f, t);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
        slashVisualObject.SetActive(false);

        slashVisualObject.transform.SetParent(transform);
        slashVisualObject.transform.localPosition = Vector3.zero;
    }

    private Vector2 GetFacingDirection()
    {
        if (playerRenderer != null && playerRenderer.flipX)
        {
            return Vector2.left;
        }

        return Vector2.right;
    }

    private void SetAlpha(float alpha)
    {
        if (runtimeMaterial != null)
        {
            runtimeMaterial.SetFloat(alphaPropertyName, alpha);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 direction = Vector2.right;

        if (playerRenderer != null && playerRenderer.flipX)
        {
            direction = Vector2.left;
        }

        Vector3 baseStartPosition = transform.position;

        if (slashStartPoint != null)
        {
            baseStartPosition = slashStartPoint.position;
        }

        Vector3 startPosition = baseStartPosition 
            + (Vector3)(direction * startForwardOffset)
            + new Vector3(0f, verticalOffset, 0f);

        Vector3 endPosition = startPosition + (Vector3)(direction * travelDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(startPosition, endPosition);
        Gizmos.DrawWireSphere(startPosition, 0.1f);
        Gizmos.DrawWireSphere(endPosition, 0.1f);
    }
}