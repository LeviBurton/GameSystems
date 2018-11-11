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

    public void Execute(SpellSystem system)
    {
        foreach (var behavior in behaviors)
        {
            behavior.Execute(system, this);
        }
    }
}
