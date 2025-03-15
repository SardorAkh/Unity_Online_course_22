using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float stoppingDistance = 1.5f;

    protected override void FixedUpdate() // Если работаем с физикой
    {
        UpdateBehavior();
    }

    protected override void UpdateBehavior()
    {
        if (TargetTransform == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, TargetTransform.position);

        if (distanceToTarget > stoppingDistance)
        {
            Vector3 moveDirection = (TargetTransform.position - transform.position).normalized;
            rb.MovePosition(rb.position + moveDirection * (movementSpeed * Time.fixedDeltaTime));
        }

        AttackTimer += Time.fixedDeltaTime;
        if (distanceToTarget <= attackRadius && AttackTimer >= attackCooldown)
        {
            Attack();
            AttackTimer = 0;
        }
    }

    protected override void Attack()
    {
        if (TargetTransform.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.Damage(damage);
        }
    }
}