using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public TargetableSystem targetable;

    public void SetTarget(TargetableSystem newTarget)
    {
        if (targetable != null)
        {
            targetable.systemsTargetingMe.Remove(this);
        }

        targetable = newTarget;
        newTarget.systemsTargetingMe.Add(this);
    }

    private void OnDisable()
    {
        if (targetable)
        {
            targetable.systemsTargetingMe.Remove(this);
        }
    }
}
