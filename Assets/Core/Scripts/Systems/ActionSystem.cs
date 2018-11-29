using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public List<ActionConfig> actions;

    void Start()
    {
        foreach (var action in actions)
        {
            action.Execute(this);
        }
    }
}
