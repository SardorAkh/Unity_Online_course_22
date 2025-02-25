using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private AggroZone aggroZone;

    private ArrayList _targets;
    private Transform _currentTarget;

    private void Update()
    {
        if (_currentTarget == null)
        {
            _targets = aggroZone.GetTargetsInZone();

            float nearest = float.MaxValue;
            
            foreach (Transform targetTransform in _targets)
            {
                float distance = Vector3.Distance(transform.position, targetTransform.position);
                // Vector3 targetVector = targetTransform.position - transform.position;
                // targetVector.magnitude
                // targetVector.sqrMagnitude

                if (distance < nearest)
                {
                    _currentTarget = targetTransform;
                }
            }
        }
    }
}