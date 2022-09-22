using UnityEngine;

public class SkeletonAttackHandler : MonoBehaviour
{
    private Skeleton enemy2Script;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        enemy2Script = GetComponentInParent<Skeleton>();
    }
    

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player") && animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            trigger.GetComponent<IDamagable>().TakeDamage(enemy2Script.attackDamage);
    }
}
