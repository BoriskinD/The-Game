using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy2 enemyBehaviourScript;

    private void Awake() => enemyBehaviourScript = GetComponentInParent<Enemy2>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyBehaviourScript.target = collision.transform;
            enemyBehaviourScript.inRange = true;
            enemyBehaviourScript.actionZone.SetActive(true);
        }
    }
}
