using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/Behaviors/Test")]
public class TestSpellBehavior : SpellBehavior
{
    public override void Execute(SpellSystem system, SpellConfig config)
    {
        Debug.LogFormat("{0} {1} TestSpellBehavior", system.name, config.spellName);
    }
}
