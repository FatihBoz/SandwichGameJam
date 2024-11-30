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


    private void Awake()
    {
        currentHp = maxHp;

        playerMovement = GetComponent<IPlayerMovement>();
    }


    private void Update()
    {
        elapsedTimeAfterAttack += Time.deltaTime;

        if (InputReceiver.Instance.GetBeastPlayerAttackInput() == 1 && elapsedTimeAfterAttack >= attackCooldown)
        {
            Attack();
            elapsedTimeAfterAttack = 0;
        }
    }

    void Attack()
    {
        print("attack inputu alýndý");
        Vector2 attackDirection = (Vector2)transform.up;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRadius, towerLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            Vector2 toEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(attackDirection, toEnemy);
            if (angle < attackAngle)
            {
                if (enemy.TryGetComponent<TowerHealth>(out var tower))
                {
                    print("tower health'e ulaþtý.");
                    tower.TakeDamage(15);
                }
            }
        }

    }

    public void TakeDamage(float damageAmount)
    {
        if (playerMovement.IsInvulnerable)
        {
            return;
        }

        //HASAR AZALTMA EFEKTÝ YOKSA

        currentHp -= damageAmount;

        beastHealthBar.fillAmount = currentHp / maxHp;

        textmanager.ShowDamageText(damageAmount);

        if (currentHp <= 0)
        {
            //GEBER
            print("geberdin");
        }

    }
}
