using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public TargetableSystem targetable;

    Vector3 targetWorldPosition;

    void OnEnable()
    {
        if (targetable)
        {
            targetable.AddWatcher(this);
        }
    }

    void OnDisable()
    {
        if (targetable)
        {
            targetable.RemoveWatcher(this);
        }
    }

    public void SetTarget(Vector3 worldPosition)
    {
        if (targetable)
        {
            targetable.RemoveWatcher(this);
            targetable = null;
        }

        this.targetWorldPosition = worldPosition;
    }

    public void SetTarget(TargetableSystem newTarget)
    {
        if (targetable != null)
        {
            targetable.RemoveWatcher(this);
        }

        targetable = newTarget;

        newTarget.AddWatcher(this);
    }

    public Vector3 TargetWorldPosition()
    {
        if (targetable != null)
        {
            return targetable.transform.position;
        }
        else
        {
            return targetWorldPosition;
        }
    }
}
