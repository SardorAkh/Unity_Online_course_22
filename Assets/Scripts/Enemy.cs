using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public event Action<Enemy> OnDie;

    [SerializeField] protected int healthPoint = 10;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float attackCooldown = 1f;
    [SerializeField] protected float attackRadius = 1f;
    [SerializeField] protected float movementSpeed = 5;
    [SerializeField] protected Rigidbody rb;

    protected Transform TargetTransform;
    protected float AttackTimer;

    public virtual void SetTarget(Transform targetTransform)
    {
        TargetTransform = targetTransform;
    }

    protected virtual void FixedUpdate() // Если работаем с физикой
    {
        if (TargetTransform)
        {
            MoveTo(TargetTransform.position);
        }
    }

    protected virtual void Attack()
    {
    }

    protected virtual void UpdateBehavior()
    {
    }

    protected void MoveTo(Vector3 targetPosition)
    {
        // Vector3 direction = targetPosition - transform.position;
        // rb.position += direction * movementSpeed * Time.deltaTime;

        rb.position = Vector3.MoveTowards(rb.position, targetPosition,
            movementSpeed * Time.deltaTime);
    }

    public void Damage(int amount)
    {
        healthPoint -= amount;

        if (healthPoint <= 0)
        {
            OnDie?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public bool IsAlive()
    {
        return healthPoint > 0;
    }
}