using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Action/New Action")]
public class ActionConfig : ScriptableObject
{
    [Header("Properties")]
    public string actionName;
    [TextArea(3, 30)] public string Description;
    [PreviewSprite] public Sprite spriteIcon;

    [Header("Behaviors")]
    public List<ActionBehavior> behaviors = null;

    public void Execute(ActionSystem system)
    {
        foreach (var behavior in behaviors)
        {
            behavior.Execute(system, this);
        }
    }
}


