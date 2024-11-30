using UnityEngine;
using UnityEngine.UI;

public class BeastPlayerCombat : MonoBehaviour,IPlayerCombat
{
    [SerializeField] private TextManager textmanager;
    [SerializeField] private Image beastHealthBar;
    [SerializeField] private float maxHp;
    private float currentHp;
    private bool isInvulnerable;
    private IPlayerMovement playerMovement;
    

    [Header("***ATTACK***")]
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] LayerMask towerLayer;
    [SerializeField] float attackAngle = 90f;
    float elapsedTimeAfterAttack;

    private bool isAttacking;
    private bool isDoubleAttacking;


    public float doubleAttackTime=0.1f;


    private Animator animator;
    private int comboCount=0;

    private void Awake()
    {
        animator=GetComponent<Animator>();
        currentHp = maxHp;
        playerMovement = GetComponent<IPlayerMovement>();
    }


    private void Update()
    {

        elapsedTimeAfterAttack += Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && doubleAttackTime>=elapsedTimeAfterAttack && isAttacking)
        {
            isDoubleAttacking=true;
        }

        if (Input.GetMouseButtonDown(0) && elapsedTimeAfterAttack >= attackCooldown && !isDoubleAttacking)
        {
            isAttacking=true;
           // Attack();
            elapsedTimeAfterAttack = 0;
        }

        animator.SetBool("isAttacking",isAttacking);
        animator.SetBool("isDoubleAttack",isDoubleAttacking);
    }

    void Attack()
    {
        print("attack inputu al�nd�");
        Vector2 attackDirection = (Vector2)transform.up;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRadius, towerLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            Vector2 toEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(attackDirection, toEnemy);
            if (angle < attackAngle)
            {
                if (enemy.TryGetComponent<Tower>(out var tower))
                {
                    print("tower health'e ula�t�.");
                    tower.TakeDamage(15);
                }
            }
        }

        if (!isAttacking && isDoubleAttacking)
        {
            isDoubleAttacking=false;
        }

        if (isAttacking)
        {
            isAttacking=false;
        }
        
    }

    public void TakeDamage(float damageAmount)
    {
        if (playerMovement.IsInvulnerable)
        {
            return;
        }

        //HASAR AZALTMA EFEKT� YOKSA

        currentHp -= damageAmount;

        beastHealthBar.fillAmount = currentHp / maxHp;

        textmanager.ShowDamageText(damageAmount);

        if (currentHp <= 0)
        {
            //GEBER
            print("geberdin"); //Asla gebermem
        }

    }
}
