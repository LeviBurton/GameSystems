using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableSystem : MonoBehaviour
{
    public List<TargetingSystem> systemsTargetingMe = new List<TargetingSystem>();

    private void OnDisable()
    {
        systemsTargetingMe.Clear();
    }

    public void AddWatcher(TargetingSystem watcher)
    {
        if (!systemsTargetingMe.Contains(watcher))
        {
            systemsTargetingMe.Add(watcher);
        }
    }

    public void RemoveWatcher(TargetingSystem watcher)
    {
        systemsTargetingMe.Remove(watcher);
    }
}
