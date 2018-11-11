using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Action/Behaviors/Spell")]
public class SpellActionBehavior : ActionBehavior
{
    public override void Execute(ActionSystem system, ActionConfig config)
    {
        Debug.LogFormat("{0} {1} SpellActionBehavior", system.name, config.actionName);

        var spellSystem = system.GetComponent<SpellSystem>();

        if (spellSystem.availableSpells.Count > 0)
        {
            // TODO: we need a way to tell the spell system which spell we would like to cast.
            spellSystem.availableSpells[0].Execute(spellSystem);
        }
    }
}
