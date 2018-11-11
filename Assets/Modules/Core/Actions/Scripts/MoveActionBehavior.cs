using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Action/Behaviors/Move")]
public class MoveActionBehavior : ActionBehavior
{
    public override void Execute(ActionSystem system, ActionConfig config)
    {
        Debug.LogFormat("{0} {1} MoveActionBehavior", system.name, config.actionName);
    }
}

