using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehavior : ScriptableObject
{
    public virtual void Execute(SpellSystem actionSystem, SpellConfig config)
    { }
}


