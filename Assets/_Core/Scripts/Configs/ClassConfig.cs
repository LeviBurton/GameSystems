using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Class")]
public class ClassConfig : ScriptableObject
{
    public string Name;

    [TextArea(3, 30)]
    public string Description;

    public ClassRuntime ToRuntime(ClassSystem owner)
    {
        var runtime = new ClassRuntime(owner, this);
        return runtime;
    }
}
