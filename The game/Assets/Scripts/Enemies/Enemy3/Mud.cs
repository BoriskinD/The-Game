using UnityEngine;

public class Mud : MonoBehaviour, IDamagable
{
    public float attackDistance;
    public float moveSpeed;
    public float timer; 
    public int maxHealth;
    public int attackDamage;
    public Transform leftBound;
    public Transform rightBound;
    public GameObject actionZone;
    public GameObject hitBox;
    public GameObject triggerArea;
    public GameObject canvas;

    [HideInInspector]
    public bool inRange;   
    [HideInInspector]
    public Transform target;

    private Animator animator;
    private HealthBar healthBar;
    private float distance; 
    private float initTimer;
    private int currentHealth;
    private int attackIndex;
    private bool attackMode;
    private bool cdAfterAttack;
    private bool alive;

    private void Awake()
    {
        cdAfterAttack = false;
        alive = true;

        maxHealth = 10;
        attackDamage = 3;
        currentHealth = maxHealth;
        initTimer = timer;

        healthBar = canvas.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();

        SelectTarget();
    }

    void Update()
    {
        if (!attackMode)
            Move();

        if (!EnemyInBounds() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            SelectTarget();

        if (inRange)
            EnemyLogic();
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else
        {
            if (cdAfterAttack)
            {
                AttackCooldown();
            }
            else
            {
                attackIndex = GetAttackAnimationIndex();
                Attack(attackIndex);
            } 
        }
    }

    private int GetAttackAnimationIndex() => Random.Range(1, 3);

    private void Move()
    {
        animator.SetBool("b_isMoving", true);
        if (!(animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack2")))
        {
            Vector2 targetPosition = new(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StopAttack()
    {
        cdAfterAttack = false;
        attackMode = false;
        animator.SetBool("b_secondAttack", false);
        animator.SetBool("b_firstAttack", false);
    }

    private void Attack(int attackIndex)
    {
        timer = initTimer; //—бросить таймер
        attackMode = true;

        animator.SetBool("b_isMoving", false);
        if (attackIndex == 1)
        {
            animator.SetBool("b_secondAttack", false);
            animator.SetBool("b_firstAttack", true);
        }
        else
        {
            animator.SetBool("b_firstAttack", false);
            animator.SetBool("b_secondAttack", true);
        }
    }

    private void AttackCooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cdAfterAttack && attackMode)
        {
            cdAfterAttack = false;
            timer = initTimer;
        }
    }

    private void TriggerCooldown() => cdAfterAttack = true;

    private bool EnemyInBounds() => transform.position.x > leftBound.position.x && transform.position.x < rightBound.position.x;

    public void SelectTarget()
    {
        float distanceToRightBound = Vector2.Distance(transform.position, rightBound.position);
        float distanceToLeftBound = Vector2.Distance(transform.position, leftBound.position);

        if (distanceToLeftBound > distanceToRightBound) target = leftBound;
        else target = rightBound;

        Flip();
    }

    public void Flip()
    {
        Vector3 mudRotation = transform.eulerAngles;
        Vector3 canvasRotation = canvas.transform.eulerAngles;

        if (transform.position.x > target.position.x) mudRotation.y = 0;
        else mudRotation.y = 180;

        canvasRotation.y = 0;
        transform.eulerAngles = mudRotation;
        canvas.transform.eulerAngles = canvasRotation;
    }

    public void TakeDamage(int damageAmount)
    {
        animator.SetTrigger("t_hurt");
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1)
        {
            alive = false;
            Die();
        } 
    }

    public bool IsAlive() => alive;


    private void Die()
    {
        animator.SetBool("b_isDead", true);
        canvas.SetActive(false);
        hitBox.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
        actionZone.GetComponent<SkeletonActionZoneHandler>().enabled = false;
        actionZone.SetActive(false);
        triggerArea.SetActive(false);
    }
}
