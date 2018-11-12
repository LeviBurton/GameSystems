using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/Behaviors/Poison Cloud")]
public class PoisonCloudSpellBehavior : SpellBehavior
{
    public override void Execute(SpellSystem system, SpellConfig config)
    {
        Debug.LogFormat("{0} {1} PoisonCloudSpellBehavior", system.name, config.spellName);

    }
}