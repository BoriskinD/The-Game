using UnityEngine;

public class TokenItem : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Messenger.AddListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.AddListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.RemoveListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("t_Collected");
            Messenger.Broadcast(GameEvent.TOKEN_COLLECTED);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, .5f);
        }
    }

    private void OnGamePaused()
    {
        animator.enabled = false;
    }

    private void OnGameUnPaused()
    {
        animator.enabled = true;
    }
}
