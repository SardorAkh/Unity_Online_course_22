using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private string[] aggroTags;

    private ArrayList _targets = new ArrayList();

    public ArrayList GetTargetsInZone()
    {
        return _targets;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTag(other.tag))
        {
            if (!_targets.Contains(other.transform))
            {
                _targets.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckTag(other.tag))
        {
            if (_targets.Contains(other.transform))
            {
                _targets.Remove(other.transform);
            }
        }
    }

    private bool CheckTag(string otherTag)
    {
        for (int i = 0; i < aggroTags.Length; i++)
        {
            if (otherTag == aggroTags[i])
            {
                return true;
            }
        }

        return false;
    }
}