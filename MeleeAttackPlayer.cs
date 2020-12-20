using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackPlayer : MonoBehaviour
{
    public Transform AttackPoint;
    public LayerMask DamageableLayerMask;
    public int Damage;
    public float AttackRange;
    public float TimeBtwAttack;

    private float timer;

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    private void Attack()
    {
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, DamageableLayerMask);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<TakeDamageForEnemy>().TakeDamage(Damage);
                    }
                }

                timer = TimeBtwAttack;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
