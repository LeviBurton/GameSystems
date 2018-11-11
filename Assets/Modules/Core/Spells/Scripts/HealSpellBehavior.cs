using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/Behaviors/Heal")]
public class HealSpellBehavior : SpellBehavior
{
    public override void Execute(SpellSystem system, SpellConfig config)
    {
        Debug.LogFormat("{0} {1} HealSpellBehavior", system.name, config.spellName);
    }
}