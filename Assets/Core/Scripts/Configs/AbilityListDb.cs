using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Ability DB")]
public class AbilityListDb : ScriptableObject
{
    public List<AbilityConfig> items = new List<AbilityConfig>();
}
