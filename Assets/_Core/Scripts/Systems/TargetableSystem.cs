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
}
