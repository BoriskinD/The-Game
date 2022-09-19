using UnityEngine;

public class TokenItem : MonoBehaviour
{
    private Animator tokenAnimator;

    private void Awake()
    {
        tokenAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            tokenAnimator.SetTrigger("t_Collected");
            Messenger.Broadcast(GameEvent.TOKEN_COLLECTED);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, .5f);
        }
    }
}
