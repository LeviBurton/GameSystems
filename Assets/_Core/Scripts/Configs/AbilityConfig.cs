using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Ability")]
public class AbilityConfig : ScriptableObject
{
    public int SortOrder;
    public string Name;
    public string ShortName;

    [TextArea(3, 30)]
    public string Description;

    public string assetPath;

    public AbilityRuntime ToRuntime(AbilitySystem owner)
    {
        var runtime = new AbilityRuntime(owner, this);
        return runtime;
    }
}