using UnityEngine;

public class SkeletonTriggerAreaHandler : MonoBehaviour
{
    private Skeleton skeletonBehaviourScript;

    private void Awake() => skeletonBehaviourScript = GetComponentInParent<Skeleton>();

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            skeletonBehaviourScript.target = trigger.transform;
            skeletonBehaviourScript.inRange = true;
            skeletonBehaviourScript.actionZone.SetActive(true);
        }
    }
}
