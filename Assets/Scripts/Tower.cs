using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamagable
{
    public event Action OnKill;
    [SerializeField] private int health = 10;
    [SerializeField] private AggroZone aggroZone;
    [SerializeField] private Transform towerRotator;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float aimTolerance = 5f;

    [SerializeField] private Projectile projectilePrefab;

    private List<GameObject> _targets;
    private GameObject _currentTarget;

    private float _attackTimer;

    private void Update()
    {
        _targets = aggroZone.GetTargets();

        GameObject candidateTarget = SelectTarget();

        if (_currentTarget == null)
        {
            _currentTarget = candidateTarget;
        }

        if (_currentTarget != null && _currentTarget.TryGetComponent<IDamagable>(out var damagable))
        {
            if (_currentTarget != null && !damagable.IsAlive())
            {
                _currentTarget = candidateTarget;
            }
        }

        if (_currentTarget != null)
        {
            RotateTowardsTarget();
            AttackTarget();
        }
    }

    private GameObject SelectTarget()
    {
        if (_targets.Count == 0)
        {
            _currentTarget = null;
            return null;
        }

        float closestDistance = float.MaxValue;
        GameObject nearestTarget = null;

        foreach (var target in _targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }

    private void RotateTowardsTarget()
    {
        if (_currentTarget == null) return;

        Vector3 targetDirection = _currentTarget.transform.position - towerRotator.position;
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        towerRotator.rotation = Quaternion.Slerp(
            towerRotator.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void AttackTarget()
    {
        _attackTimer += Time.deltaTime;

        Vector3 targetDirection = _currentTarget.transform.position - towerRotator.position;
        targetDirection.y = 0;

        float angle = Vector3.Angle(towerRotator.forward, targetDirection);

        if (_attackTimer >= attackCooldown && angle <= aimTolerance)
        {
            Shoot();
            _attackTimer = 0;
        }
    }

    private void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        projectile.Launch(_currentTarget.transform.position);
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (!IsAlive())
        {
            Debug.Log("Game Over");
        }
    }

    public bool IsAlive() => health >= 0;
}