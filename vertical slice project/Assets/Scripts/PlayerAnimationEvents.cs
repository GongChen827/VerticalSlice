using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }

    public void FinishAttack()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsAttacking", false);
        }
    }
}