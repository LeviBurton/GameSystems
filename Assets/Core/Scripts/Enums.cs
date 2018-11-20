using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum EWeaponType
{
    Unarmed,
    Two_Hand_Sword,
    Two_Hand_Spear,
    Two_Hand_Axe,
    Two_Hand_Bow,
    Two_Hand_Crossbow,
    Two_Hand_Club,
    Staff,
    Sword,
    Mace,
    Dagger,
    Item,
    Pistol,
    Rifle,
}

// this patches up the rpg character animation pack stuff.
public enum EWeaponAnimationType
{
    UNARMED = 0,
    TWOHANDSWORD = 1,
    TWOHANDSPEAR = 2,
    TWOHANDAXE = 3,
    TWOHANDBOW = 4,
    TWOHANDCROSSBOW = 5,
    STAFF = 6,
    ONEHANDED = 7,
    RELAX = 8,
    //   RIFLE = 9,
    TWOHANDCLUB = 20,
    SHIELD = 11,
    ARMEDSHIELD = 12,
    RIFLE = 18
}

// this patches up the rpg character animation pack stuff.
public enum EWeaponAnimationArmedType
{
    UNARMED = 0,
    TWOHANDSWORD = 1,
    TWOHANDSPEAR = 2,
    TWOHANDAXE = 3,
    TWOHANDBOW = 4,
    TWOHANDCROSSBOW = 5,
    STAFF = 6,
    SHIELD = 7,
    LEFT_SWORD = 8,
    RIGHT_SWORD = 9,
    LEFT_MACE = 10,
    RIGHT_MACE = 11,
    LEFT_DAGGER = 12,
    RIGHT_DAGGER = 13,
    LEFT_ITEM = 14,
    RIGHT_ITEM = 15,
    LEFT_PISTOL = 16,
    RIGHT_PISTOL = 17,
    RIFLE = 18,
    RIGHT_SPEAR = 19,
    TWOHANDCLUB = 20
}

public enum EWeaponAnimationHand
{
    Two = 0,    // Two handed weapons
    Left = 1,
    Right = 2,
    Dual = 3,   // dual-wield wepaons
}

