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
            foreach (var spell in spellSystem.availableSpells)
            {
                spell.Execute(spellSystem);
            }
        }
    }
}
