using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClassRuntime
{
    public string assetName;
    public string Name;

    [NonSerialized]
    public string Description;

    [NonSerialized]
    ClassSystem owner;

    public int level = 1;
    public int experience = 0;

    public ClassRuntime(ClassSystem owner, ClassConfig config)
    {
        this.owner = owner;
        this.Name = config.Name;
        this.assetName = config.name;
        this.Description = config.Description;
    }
}
