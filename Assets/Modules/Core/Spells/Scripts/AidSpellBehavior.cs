using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/Behaviors/Aid")]
public class AidSpellBehavior : SpellBehavior
{
    public override void Execute(SpellSystem system, SpellConfig config)
    {
        Debug.LogFormat("{0} {1} AidSpellBehavior", system.name, config.spellName);

    }
}