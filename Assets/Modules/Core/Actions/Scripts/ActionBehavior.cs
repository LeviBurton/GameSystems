using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBehavior : ScriptableObject
{
    public virtual void Execute(ActionSystem actionSystem, ActionConfig config)
    { }
}


