using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// This system is necessary for our save game system to work.
/// Any game objects in the world that need to save themselves will
/// need one of these components attached to it.
/// The guid can be set when a save game is loaded, or during run time, for 
/// instance when it is spawned.
/// </summary>
public class SaveGameIdSystem : MonoBehaviour
{
    [InlineButton("GenerateNewId", "New")]
    public string SaveGameId;

    void GenerateNewId()
    {
        SaveGameId = Guid.NewGuid().ToString();
    }
}
