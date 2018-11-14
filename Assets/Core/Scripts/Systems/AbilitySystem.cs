using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public List<AbilityConfig> abilityConfigs;
    public List<AbilityRuntime> abilityRuntimes;

    void Awake()
    {
        foreach (var config in abilityConfigs)
        {
            var abilityRuntime = config.ToRuntime(this);
            abilityRuntimes.Add(abilityRuntime);
        }

    }
}
