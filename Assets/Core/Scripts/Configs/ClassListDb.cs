using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Class DB")]
public class ClassListDb : ScriptableObject
{
    public List<ClassConfig> items = new List<ClassConfig>();
    
}
