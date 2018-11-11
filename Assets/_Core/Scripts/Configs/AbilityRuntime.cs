using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IntCurrentMinMax
{
    public int maxValue;
    public int minValue;
    public int currentValue;
}

[Serializable]
public class AbilityRuntime
{
    public string assetName;
    public string Name;
    public IntCurrentMinMax values;

    [NonSerialized]
    AbilitySystem owner;

    public AbilityRuntime(AbilitySystem owner, AbilityConfig config)
    {
        this.owner = owner;
        this.assetName = config.name;
        this.Name = config.Name;

        values.minValue = 0;
        values.maxValue = 30;
        values.currentValue = Dice.Roll(3, 6);
    }
}
