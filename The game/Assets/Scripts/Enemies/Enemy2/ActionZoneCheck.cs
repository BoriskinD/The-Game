using UnityEngine;

public class ActionZoneCheck : MonoBehaviour
{
    private Enemy2 enemyBehaviourScript;
    private Animator animator;
    private bool inRange;

    private void Awake()
    {
        enemyBehaviourScript = GetComponentInParent<Enemy2>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            enemyBehaviourScript.Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyBehaviourScript.inRange = false;
            enemyBehaviourScript.triggerArea.SetActive(true);
            enemyBehaviourScript.SelectTarget();
        }
    }
}
