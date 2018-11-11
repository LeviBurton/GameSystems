using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Action/Behaviors/Attack")]
public class AttackActionBehavior : ActionBehavior
{
    public override void Execute(ActionSystem system, ActionConfig config)
    {
        var targetingSystem = system.GetComponent<TargetingSystem>();

        Debug.LogFormat("{0} {1} AttackActionBehavior", system.name, config.actionName);
        Debug.LogFormat("\t Target: {0}", targetingSystem.TargetWorldPosition());
    }
}