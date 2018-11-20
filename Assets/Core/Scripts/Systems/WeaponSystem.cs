using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// note that I added this quickly to get animations up and running --
// we may need to take a step back and re-consider our options.
public class WeaponSystem : MonoBehaviour
{
    public UnityEvent_WeaponEquipped onWeaponEquipped;
    public WeaponConfig weaponConfig;

    void Start()
    {
        EquipWeapon(weaponConfig);    
    }

    public void EquipWeapon(WeaponConfig weaponConfig)
    {
        if (weaponConfig != this.weaponConfig)
        {
            this.weaponConfig = weaponConfig;
        }

        onWeaponEquipped.Invoke(this.weaponConfig);
    }
}
