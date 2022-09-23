using UnityEngine;

public class MudActionZoneHandler : MonoBehaviour
{
    private Mud mudBehaviourScript;
    private Animator animator;
    private bool inRange;

    private void Awake()
    {
        mudBehaviourScript = GetComponentInParent<Mud>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !(animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
            && mudBehaviourScript.IsAlive())
        {
            mudBehaviourScript.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            mudBehaviourScript.inRange = false;
            mudBehaviourScript.triggerArea.SetActive(true);
            mudBehaviourScript.SelectTarget();
        }
    }
}
