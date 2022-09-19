using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private PlayerCombat playerCombatScript;
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponentInParent<Animator>();
        playerCombatScript = GetComponentInParent<PlayerCombat>();
    } 

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Enemy") && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            trigger.GetComponent<IDamagable>().TakeDamage(playerCombatScript.attackDamage);
    }
}
