using UnityEngine;

public class ArcherTriggerAreaHandler : MonoBehaviour
{
    private Archer archer;

    private void Awake() => archer = GetComponentInParent<Archer>();

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player") && archer.IsAlive())
        {
            archer.player = trigger.transform;
            archer.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player") && archer.IsAlive())
        {
            archer.playerInRange = false;
        }
    }
}
