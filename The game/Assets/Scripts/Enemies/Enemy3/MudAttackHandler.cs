using UnityEngine;

public class MudAttackHandler : MonoBehaviour
{
    private Mud mudScript;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        mudScript = GetComponentInParent<Mud>();
    }


    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player") && (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") 
                                                        || animator.GetCurrentAnimatorStateInfo(0).IsName("attack2")))
            trigger.GetComponent<IDamagable>().TakeDamage(mudScript.attackDamage);
    }
}
