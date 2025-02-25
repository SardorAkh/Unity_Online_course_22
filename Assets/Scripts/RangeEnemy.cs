using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        if (TargetTransform)
        {
            MoveTo(TargetTransform.position);
            //MoveTo();
            //Strafe();
        }
    }
}
