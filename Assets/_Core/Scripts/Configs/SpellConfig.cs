using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/New Spell")]
public class SpellConfig : ScriptableObject
{
    public string spellName;
    [TextArea(3, 30)]
    public string Description;
    public Sprite spriteIcon;

    public List<SpellBehavior> behaviors = null;
    public VfxConfig targetVfx = null;

    public void Execute(SpellSystem system)
    {
        Transform targetTransform = system.transform;

        var target = system.GetComponent<TargetingSystem>().targetable;

        if (target != null)
        {
            targetTransform = target.transform;
        }

        foreach (var behavior in behaviors)
        {
            behavior.Execute(system, this);
        }

        if (targetVfx != null)
        {
            var vfx = Instantiate(targetVfx.vfxPrefab, targetTransform);
            vfx.GetComponent<VfxSystem>().config = targetVfx;
        }
    }
}
