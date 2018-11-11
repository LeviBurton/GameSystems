using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/Behaviors/Burn")]
public class BurnSpellBehavior : SpellBehavior
{
    public override void Execute(SpellSystem system, SpellConfig config)
    {
        Debug.LogFormat("{0} {1} BurnSpellBehavior", system.name, config.spellName);
    }
}