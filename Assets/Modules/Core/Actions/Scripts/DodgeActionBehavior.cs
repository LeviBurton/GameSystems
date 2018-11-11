using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Action/Behaviors/Dodge")]
public class DodgeActionBehavior : ActionBehavior
{
    public override void Execute(ActionSystem system, ActionConfig config)
    {
        Debug.LogFormat("{0} {1} DodgeActionBehavior", system.name, config.actionName);
    }
}

