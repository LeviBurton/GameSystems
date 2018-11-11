using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NpcRuntime
{
    public string saveGameId;
    public List<AbilityRuntime> abilityRuntimes;
    public ClassRuntime classRuntime;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public int maxHealth;
    public int currentHealth;
    public int minHealth;
    public bool isAlive;

    public NpcRuntime(string saveGameId, List<AbilityRuntime> abilityRuntimes, ClassRuntime classRuntime)
    {
        this.saveGameId = saveGameId;
        this.abilityRuntimes = abilityRuntimes;
        this.classRuntime = classRuntime;
    }
}
