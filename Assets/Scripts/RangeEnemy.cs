using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float strafeSpeed = 3f;
    [SerializeField] private Projectile projectilePrefab;

    private int _strafeDirection;
    private float _strafeTimer;

    protected override void FixedUpdate()
    {
        UpdateBehavior();
    }

    protected override void UpdateBehavior()
    {
        if (TargetTransform == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, TargetTransform.position);

        ManageDistance(distanceToTarget);


        if (distanceToTarget <= minDistance)
        {
            Attack();
            AttackTimer = 0;
        }

        AttackTimer += Time.fixedDeltaTime;
    }

    private void ManageDistance(float currentDistance)
    {
        _strafeTimer += Time.fixedDeltaTime;
        if (_strafeTimer >= 2f)
        {
            GenerateStrafeDirection();
            _strafeTimer = 0;
        }

        Vector3 directionToTarget = (TargetTransform.position - transform.position).normalized;
        Vector3 strafeMovement = Vector3.Cross(directionToTarget, Vector3.up);


        if (currentDistance < minDistance)
        {
            rb.MovePosition(rb.position - directionToTarget * (movementSpeed * Time.fixedDeltaTime));
        }
        else if (currentDistance > maxDistance)
        {
            rb.MovePosition(rb.position + directionToTarget * (movementSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.MovePosition(rb.position + strafeMovement * (_strafeDirection * (strafeSpeed * Time.fixedDeltaTime)));
        }
    }

    private void GenerateStrafeDirection()
    {
        _strafeDirection = Random.value > 0.5f ? 1 : -1;
    }

    protected override void Attack()
    {
        Projectile projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.LookRotation(TargetTransform.position - transform.position)
        );

        projectile.Launch(TargetTransform.position);
    }
}