using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Action DB")]
public class ActionListDb : ScriptableObject
{
    public List<ActionConfig> items = new List<ActionConfig>();
}
