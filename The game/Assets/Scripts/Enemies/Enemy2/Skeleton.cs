using UnityEngine;

public class Skeleton : MonoBehaviour, IDamagable
{
    public float attackDistance; 
    public float moveSpeed;
    public float timer; 
    public int maxHealth = 10;
    public int attackDamage = 3;
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

    private PlayerCombat playerCombat;
    private Animator animator;
    private HealthBar healthBar;
    private float distance; 
    private float initTimer;
    private int currentHealth;
    private bool attackMode;
    private bool cdAfterAttack = false; 

    private void Awake()
    {
        currentHealth = maxHealth;
        initTimer = timer;
        healthBar = canvas.GetComponentInChildren<HealthBar>();
        animator = GetComponent<Animator>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();

        healthBar.SetMaxHealth(maxHealth);

        Messenger.AddListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.AddListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);

        SelectTarget();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.RemoveListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
    }

    void Update()
    {
        if (!attackMode)
            Move();

        if (!EnemyInBounds() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            SelectTarget();

        if (inRange)
            SkeletonLogic();
    }
       
    private void SkeletonLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else 
        {
            if (playerCombat.IsAlive)
            {
                if (cdAfterAttack)
                {
                    animator.SetBool("b_isAttack", false);
                    AttackCooldown();
                }
                else Attack();
            }
            else
            {
                //inRange = false;
                SelectTarget();
            } 
        } 
    }

    private void Move()
    {
        animator.SetBool("b_isMoving", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Vector2 targetPosition = new (target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StopAttack()
    {
        cdAfterAttack = false;
        attackMode = false;
        animator.SetBool("b_isAttack", false);
    }

    private void Attack()
    {
        //начальный таймер
        timer = initTimer;
        attackMode = true;

        animator.SetBool("b_isMoving", false);
        animator.SetBool("b_isAttack", true);
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
        Vector3 skeletonRotation = transform.eulerAngles;
        Vector3 canvasRotation = canvas.transform.eulerAngles;

        if (transform.position.x > target.position.x) skeletonRotation.y = 180;
        else skeletonRotation.y = 0;

        canvasRotation.y = 0;
        transform.eulerAngles = skeletonRotation;
        canvas.transform.eulerAngles = canvasRotation;
    }

    public void TakeDamage(int damageAmount)
    {
        animator.SetTrigger("t_Hurt");
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1) Die();
    }

    private void Die()
    {
        animator.SetBool("b_isDead", true);
        canvas.SetActive(false);
        hitBox.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
        actionZone.GetComponent<SkeletonActionZoneHandler>().enabled = false;
        actionZone.SetActive(false);
    }

    private void OnGamePaused()
    {
        animator.enabled = false;
        enabled = false;
    }
    private void OnGameUnPaused()
    {
        animator.enabled = true;
        enabled = true;
    }
}
