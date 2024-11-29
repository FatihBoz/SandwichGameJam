using UnityEngine;

public class BeastPlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;

    [SerializeField] float attackRadius = 1f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] LayerMask towerLayer;
    [SerializeField] float attackAngle = 90f;

    float elapsedTimeAfterAttack;

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
        Vector2 attackDirection = (Vector2)transform.up;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRadius, towerLayer);
        foreach (Collider2D enemy in enemiesHit)
        {
            Vector2 toEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(attackDirection, toEnemy);
            if (angle < attackAngle)
            {
                //Access its script and damage it
            }
        }

    }
}
