using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponRuntime
{
    public string assetName;
    public string Name;

    [NonSerialized]
    WeaponSystem owner;

    public WeaponRuntime(WeaponSystem owner, WeaponConfig config)
    {
        this.owner = owner;
        this.assetName = config.name;
        this.Name = config.weaponName;
    }
}
