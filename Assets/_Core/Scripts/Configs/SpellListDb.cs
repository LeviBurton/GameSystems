using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Spell DB")]
public class SpellListDb : ScriptableObject
{
    public List<SpellConfig> items = new List<SpellConfig>();
}

