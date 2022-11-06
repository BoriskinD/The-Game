using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthAmount = 2;
    public float moveSpeed = .5f;
    public float maxHeigth = 4f;
    public float minHeigth = 3.5f;
    public ParticleSystem healthEffect;

    private bool moveUp = true;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.AddListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.RemoveListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    private void Update() => Move();

    private void Move()
    {
        if (transform.position.y > maxHeigth)
            moveUp = false;
        else if (transform.position.y < minHeigth)
            moveUp = true;

        if (moveUp)
            transform.Translate(moveSpeed * Time.deltaTime * transform.up);
        else transform.Translate(-moveSpeed * Time.deltaTime * transform.up);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            PlayerCombat playerScript = trigger.gameObject.GetComponent<PlayerCombat>();
            if (playerScript.CurrentHealth < playerScript.MaxHealth)
            {
                playerScript.UpdateCurrentHealth(healthAmount);
                healthEffect.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject, 1);
            }
        }
    }

    private void OnGamePaused()
    {
        enabled = false;
    }

    private void OnGameUnPaused()
    {
        enabled = true;
    }
}
