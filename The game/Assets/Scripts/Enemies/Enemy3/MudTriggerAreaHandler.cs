using UnityEngine;

public class MudTriggerAreaHandler : MonoBehaviour
{
    private Mud mudBehaviourScript;

    private void Awake() => mudBehaviourScript = GetComponentInParent<Mud>();

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player") && mudBehaviourScript.IsAlive())
        {
            gameObject.SetActive(false);
            mudBehaviourScript.target = trigger.transform;
            mudBehaviourScript.inRange = true;
            mudBehaviourScript.actionZone.SetActive(true);
        }
    }
}
