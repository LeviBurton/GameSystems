using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Hero")]
public class HeroConfig : ScriptableObject
{
    public string Name;
    public GameObject heroPrefab;
}
