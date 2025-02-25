using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void FixedUpdate() // Если работаем с физикой
    {
        if (TargetTransform)
        {
           // MoveTo(TargetTransform.position);
        }
    }
}
