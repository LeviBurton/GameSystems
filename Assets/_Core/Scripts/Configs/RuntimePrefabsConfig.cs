using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Runtime Prefab Settings")]
public class RuntimePrefabsConfig : ScriptableObject
{
    public GameObject heroPrefab;
    public GameObject npcCharacterPrefab;
    public GameObject enemyCharacterPrefab;
}
