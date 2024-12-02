using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeastPlayerCombat : MonoBehaviour,IPlayerCombat
{
    [SerializeField] private TextManager textmanager;
    [SerializeField] private Image beastHealthBar;
    [SerializeField] private float maxHp;
    private float currentHp;
    private bool isInvulnerable;
    private IPlayerMovement playerMovement;
    [SerializeField] private float yOffSetAfterDie = 1f;


    [Header("***ATTACK***")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] LayerMask towerLayer;

    public float doubleAttackTime = 0.1f;

    private float elapsedTimeAfterAttack;
    private bool isAttacking;
    private bool isDoubleAttacking;


    [Header("***SECONDARY ATTACK***")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float secondaryAttackCooldown = 5f;


    private float elapsedTimeAfterSecondaryAttack = 0f;

    bool isFiring = false;


    private Animator animator;
    private int comboCount=0;

    private bool ded=false;

    public static Action OnDed;
    
    private bool animFinished=false;
    private float Dedtimer;

    
    
    private void Awake()
    {
        animator=GetComponent<Animator>();
        currentHp = maxHp;
        playerMovement = GetComponent<IPlayerMovement>();
        
    }


    private void Update()
    {
        if (ded)
        {
            if (!animFinished)
            {
                Dedtimer=Time.time;
            }
            if (Time.time>=Dedtimer+2.0f && animFinished)
            {
                SceneManager.LoadScene("MainMenu");
            }
            return;
        }

        elapsedTimeAfterSecondaryAttack += Time.deltaTime;

        if (InputReceiver.Instance.GetBeastPlayerSecondaryAttackInput() == 1 &&
            elapsedTimeAfterSecondaryAttack >= secondaryAttackCooldown)
        {
            
            elapsedTimeAfterSecondaryAttack = 0f;

            Debug.Log("Second Atack casted");
            CastSecondaryAttack();
        }


        elapsedTimeAfterAttack += Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && doubleAttackTime>=elapsedTimeAfterAttack && isAttacking)
        {
            isDoubleAttacking=true;
        }

        if (Input.GetMouseButtonDown(0) && elapsedTimeAfterAttack >= attackCooldown && !isDoubleAttacking)
        {
            print("ilk vuruşa girdi");
            isAttacking=true;
           // Attack();
            elapsedTimeAfterAttack = 0;
        }

        animator.SetBool("isAttacking",isAttacking);
        animator.SetBool("isDoubleAttack",isDoubleAttacking);
    }

    void Attack()
    {
        Vector2 attackDirection = (Vector2)transform.up;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, towerLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            if (enemy.TryGetComponent<Tower>(out var tower))
            {
                tower.TakeDamage(6);
                ScreenShake.Instance.Shake();
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

    private void CastSecondaryAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 initialScale = projectile.transform.localScale;

        if (transform.localScale.x < 0)
        {
            projectile.transform.localScale = new Vector2(-initialScale.x, initialScale.y);
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
            OnDed?.Invoke();
        }

    }
    private void BPC_OnDeath()
    {
        print("geberdin"); //Asla gebermem
        ded = true;
        playerMovement.SetIsStopped(true);
        animator.SetBool("ded", true);
    }

    private void Beast_OnDayStarted()
    {
        isAttacking = false;
        isDoubleAttacking = false;  
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (gameObject.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.enabled = true;
        }
        TakeDamage(-(maxHp - currentHp) * 0.5f);

    }


    private void OnEnable()
    {
        BeastPlayerCombat.OnDed += BPC_OnDeath;
        CycleManager.OnDayStarted += Beast_OnDayStarted;
    }



    private void OnDisable()
    {
        BeastPlayerCombat.OnDed -= BPC_OnDeath;
        CycleManager.OnDayStarted -= Beast_OnDayStarted;
    }


    public void AnimationFinished()
    {
        animFinished=true;
        Dedtimer=Time.time;
    }
}
