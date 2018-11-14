using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Vfx/New Vfx")]
public class VfxConfig : ScriptableObject
{
    public string vfxName;
    public List<VfxBehavior> behaviors;
    public GameObject vfxPrefab;

    public void Execute(VfxSystem system)
    {
        foreach (var behavior in behaviors)
        {
            behavior.Execute(system, this);
        }
    }
}
