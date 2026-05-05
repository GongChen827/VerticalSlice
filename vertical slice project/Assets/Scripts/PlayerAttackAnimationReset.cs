using UnityEngine;

public class PlayerAttackAnimationReset : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;

    [Header("Parameter Names")]
    [SerializeField] private string attackStateName = "Attack";
    [SerializeField] private string isAttackingBoolName = "IsAttacking";

    [Header("Reset Timing")]
    [SerializeField] private float resetAtNormalizedTime = 0.9f;

    private void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (playerAnimator == null) return;

        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        bool isInAttackState = stateInfo.IsName(attackStateName);

        if (isInAttackState && stateInfo.normalizedTime >= resetAtNormalizedTime)
        {
            playerAnimator.SetBool(isAttackingBoolName, false);
        }
    }
}