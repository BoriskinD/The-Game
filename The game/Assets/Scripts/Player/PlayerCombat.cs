using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamagable
{
    public int attackDamage = 2;
    public int maxHealth = 10;
    public float timeToNextAttack = 1.5f;
    public HealthBar healthBar;

    private Animator animator;
    public float timer;
    private int currentHealth;
    private bool canAttack;

    public int CurrentHealth 
    {
        get => currentHealth;
    }

    public int MaxHealth
    {
        get => maxHealth;
    }


    private void Awake()
    {
        canAttack = true;
        timer = timeToNextAttack;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    private void Update()
    {
        if (canAttack)
        {
            if (Input.GetMouseButtonDown(0))
                Attack();
        }
        else AttackCoolDown();
    }

    private void AttackCoolDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !canAttack)
        {
            timer = timeToNextAttack;
            canAttack = true;
        }
    }

    private void Attack()
    {
        canAttack = false;
        animator.SetTrigger("t_Attack");
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("t_Hurt");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1) Die();
    }

    public void UpdateCurrentHealth(int healthValue)
    {
        currentHealth += healthValue;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        animator.SetBool("b_isDead", true);
        // ŒÕ≈÷ »√–€
    }
}
