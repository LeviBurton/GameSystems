using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell/New Spell")]
public class SpellConfig : ScriptableObject
{
    [Header("Properties")]
    public string spellName;
    [TextArea(3, 30)] public string Description;
    [PreviewSprite] public Sprite spriteIcon;
    [Range(1, 9)]
    public int Level;

    [Header("Behaviors")]
    public List<SpellBehavior> behaviors = null;

    [Header("VFX")]
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
