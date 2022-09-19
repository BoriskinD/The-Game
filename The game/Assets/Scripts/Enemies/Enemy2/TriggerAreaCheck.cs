using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Skeleton enemyBehaviourScript;

    private void Awake() => enemyBehaviourScript = GetComponentInParent<Skeleton>();

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
