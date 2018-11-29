using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(TargetableSystem))]
[RequireComponent(typeof(TargetingSystem))]
public class WeaponSystem : MonoBehaviour
{
    [Header("Properties")]
    public WeaponConfig weaponConfig;

    [Header("Attach Points")]
    public GameObject attachPoint_Hand_Right;
    public GameObject attachPoint_Hand_Left;
    public GameObject attachPoint_Waist_Right;
    public GameObject attachPoint_Waist_Left;
    public GameObject attachPoint_Back;

    [Header("Events")]
    public UnityEvent_WeaponSystem_Equip onEquipWeapon;
    public UnityEvent_WeaponSystem_UnEquip onUnEquipWeapon;
    public UnityEvent_WeaponSystem_Use onUseWeapon;

    TargetingSystem targetingSystem = null;
    GameObject spawnedWeapon = null;

    void Start()
    {
        targetingSystem = GetComponent<TargetingSystem>();

        Equip(weaponConfig);

        UnEquip();
    }

    public void Equip(WeaponConfig weaponConfig)
    {
        Destroy(spawnedWeapon);
        this.weaponConfig = weaponConfig;
        spawnedWeapon = Instantiate(this.weaponConfig.weaponPrefab, attachPoint_Hand_Right.transform);
        onEquipWeapon.Invoke(this);
    }

    // TODO: specify hand
    public void UnEquip()
    {
        spawnedWeapon.transform.SetParent(attachPoint_Back.transform);
        spawnedWeapon.transform.localPosition = Vector3.zero;
        spawnedWeapon.transform.localRotation = Quaternion.identity;
        onUnEquipWeapon.Invoke(this);
    }

    public bool IsTargetInRange(TargetableSystem target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= weaponConfig.range;
    }

    public void Use(TargetableSystem target)
    {
        if (!IsTargetInRange(target))
        {
            throw new UnityException("Error -- trying to use a weapon on something that is out of its range.  This should not happen!");
        }

        onUseWeapon.Invoke(this, target);
    }
}
