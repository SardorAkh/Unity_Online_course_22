using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected int healthPoint;
    [SerializeField] protected int damage;
    [SerializeField] protected int attackSpeed;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected int movementSpeed;
    [SerializeField] protected Rigidbody rb;

    protected Transform TargetTransform;
    
    public void SetTarget(Transform targetTransform)
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

    protected virtual void Attack() {}
    protected virtual void ReceiveDamage() {}
    
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
        
        Debug.Log(name);
        
        // if (healthPoint <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }
}
