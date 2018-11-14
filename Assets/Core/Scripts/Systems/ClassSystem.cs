using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSystem : MonoBehaviour
{
    public ClassConfig classConfig = null;
    public ClassRuntime classRuntime = null;

    void Awake()
    {
        // TODO: have option to select where we would like to get the classRuntime from -- saved game, config, etc.
        // this will be part of the overall serialization scheme with systems.
        if (classConfig != null)
        {
            classRuntime = classConfig.ToRuntime(this);
        }
    }
}
  