using UnityEngine;

public class SpriteGlowMirror : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sourceRenderer;
    [SerializeField] private SpriteRenderer glowRenderer;
    [SerializeField] private Color glowColor = new Color(0.35f, 0.85f, 1f, 0.55f);

    private void Awake()
    {
        if (glowRenderer == null)
        {
            glowRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void LateUpdate()
    {
        if (sourceRenderer == null || glowRenderer == null) return;

        glowRenderer.sprite = sourceRenderer.sprite;
        glowRenderer.flipX = sourceRenderer.flipX;
        glowRenderer.color = glowColor;
    }
}