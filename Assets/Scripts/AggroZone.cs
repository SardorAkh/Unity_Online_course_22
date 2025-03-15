using System;
using System.Collections.Generic;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private string tags;

    private List<GameObject> _targets = new();

    public List<GameObject> GetTargets()
    {
        CheckTargets();
        return _targets;
    }

    private void CheckTargets()
    {
        for (int i = _targets.Count - 1; i >= 0; i--)
        {
            if (_targets[i] == null)
            {
                _targets.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tags))
        {
            _targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tags))
        {
            _targets.Remove(other.gameObject);
        }
    }
}