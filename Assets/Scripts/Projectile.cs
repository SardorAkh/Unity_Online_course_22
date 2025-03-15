using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private int damage = 1;

    private Vector3 _targetPosition;
    private bool _isLaunched = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (_isLaunched)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPosition,
                speed * Time.deltaTime
            );
        }
    }

    public void Launch(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _isLaunched = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.Damage(damage);
            Destroy(gameObject);
        }
    }
}