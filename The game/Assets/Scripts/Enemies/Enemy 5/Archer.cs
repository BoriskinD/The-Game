using UnityEngine;

public class Archer : MonoBehaviour, IDamagable
{
    public int maxHealth = 10;
    public int attackDamage = 6;
    //Time between attacks
    public float timeBtwAttacks = 2.5f;
    public GameObject arrowPrefab;
    public GameObject canvas;
    public GameObject triggerArea;
    [HideInInspector]
    public bool playerInRange = false;
    [HideInInspector]
    public Transform player;

    private int currenthHealth;
    private float timer;
    private Animator animator;
    private HealthBar healthBar;
    private Bow bow;
    private bool cdAfterAttack = false;
    private bool alive = true;

    private void Awake()
    {
        currenthHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar = canvas.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(currenthHealth);
        bow = GetComponentInChildren<Bow>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            Flip();
            Attack();
        }
    }

    private void Attack()
    {
        if (cdAfterAttack)
        {
            animator.SetBool("b_isAttack", false);
            AttackCooldown();
        }
        else
        {
            cdAfterAttack = true;
            timer = timeBtwAttacks;
            animator.SetBool("b_isAttack", true);
            bow.Shoot();
        }
    }

    private void AttackCooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cdAfterAttack)
            cdAfterAttack = false;
    }

    public void TakeDamage(int damageValue)
    {
        animator.SetTrigger("t_hurt");
        currenthHealth -= damageValue;
        healthBar.SetHealth(currenthHealth);
        if (currenthHealth < 1) Die();
    }

    private void Die()
    {
        animator.SetBool("b_isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        playerInRange = false;
        triggerArea.SetActive(false);
    }

    private void Flip()
    {
        Vector3 archerRotation = transform.eulerAngles;
        Vector3 canvasRotation = canvas.transform.eulerAngles;

        if (transform.position.x > player.position.x) archerRotation.y = 180;
        else archerRotation.y = 0;

        canvasRotation.y = 0;
        transform.eulerAngles = archerRotation;
        canvas.transform.eulerAngles = canvasRotation;
    }

    public bool IsAlive() => alive;
}
