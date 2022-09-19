using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamagable
{
    public int attackDamage = 2;
    public int maxHealth = 10;
    //public float attackRate = 2f;
    public float attackRange = .5f;
    public HealthBar healthBar;

    private Animator animator;
    //private float timeToNextAttack = 0f;
    private int currentHealth;
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
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))// && Time.time >= timeToNextAttack)
        {
            Attack();
            //timeToNextAttack = Time.time + 1 / attackRate;
        }
    }

    private void Attack()
    {
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
