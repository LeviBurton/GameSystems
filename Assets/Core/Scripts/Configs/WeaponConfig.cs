using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// note that I added this quickly to get animations up and running --
// we may need to take a step back and re-consider our options.
[CreateAssetMenu(menuName = "Config/Weapon/New Weapon")]
public class WeaponConfig : ScriptableObject
{
    [Header("Properties")]
    public string weaponName;
    public int range;

    [TextArea(3, 30)] public string Description;
    [PreviewSprite] public Sprite spriteIcon;

    // The character will use this to play the correct weapon animations associated with this weapon.
    public EWeaponType animationType;

    public GameObject weaponPrefab;

    // TODO: consider what we need to put here.
    // things like durability, any temporary bonuses, etc. etc.
    public WeaponRuntime ToRuntime(WeaponSystem owner)
    {
        var runtime = new WeaponRuntime(owner, this);

        return runtime;
    }
}
