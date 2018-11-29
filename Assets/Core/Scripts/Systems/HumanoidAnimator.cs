using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocomotionSystem))]
[RequireComponent(typeof(WeaponSystem))]
[RequireComponent(typeof(Animator))]
public class HumanoidAnimator : MonoBehaviour
{
    public float animationSpeed = 1.0f;
    public float idleCheckTimeMin = 10;
    public float idleCheckTimeMax = 60;

    float idleCheckTime;
    float currentIdleCheckTime = 0.0f;

    Animator animator;
    LocomotionSystem locomotion = null;
    WeaponConfig weaponConfig;

    #region Animator paramaters
    public static readonly int id_Action = Animator.StringToHash("Action");
    public static readonly int id_Injured = Animator.StringToHash("Injured");
    public static readonly int id_Moving = Animator.StringToHash("Moving");
    public static readonly int id_Strafing = Animator.StringToHash("Strafing");
    public static readonly int id_Velocity_Z = Animator.StringToHash("Velocity Z");
    public static readonly int id_Velocity_X = Animator.StringToHash("Velocity X");
    public static readonly int id_AnimationSpeed = Animator.StringToHash("AnimationSpeed");
    public static readonly int id_AimHorizontal = Animator.StringToHash("AimHorizontal");
    public static readonly int id_AimVertical = Animator.StringToHash("AimVertical");
    public static readonly int id_Weapon = Animator.StringToHash("Weapon");
    public static readonly int id_AttackSide = Animator.StringToHash("AttackSide");
    public static readonly int id_LeftWeapon = Animator.StringToHash("LeftWeapon");
    public static readonly int id_RightWeapon = Animator.StringToHash("RightWeapon");
    public static readonly int id_LeftRight = Animator.StringToHash("LeftRight");
    public static readonly int id_BowPull = Animator.StringToHash("BowPull");
    public static readonly int id_IdleTrigger = Animator.StringToHash("IdleTrigger");
    public static readonly int id_TurnLeftTrigger = Animator.StringToHash("TurnLeftTrigger");
    public static readonly int id_TurnRightTrigger = Animator.StringToHash("TurnRightTrigger");
    public static readonly int id_DodgeTrigger = Animator.StringToHash("DodgeTrigger");
    public static readonly int id_InstantSwitchTrigger = Animator.StringToHash("InstantSwitchTrigger");
    public static readonly int id_AttackTrigger = Animator.StringToHash("AttackTrigger");
    public static readonly int id_AttackCastTrigger = Animator.StringToHash("AttackCastTrigger");
    public static readonly int id_SpecialAttackTrigger = Animator.StringToHash("SpecialAttackTrigger");
    public static readonly int id_SpecialEndTrigger = Animator.StringToHash("SpecialEndTrigger");
    public static readonly int id_ReloadTrigger = Animator.StringToHash("ReloadTrigger");
    public static readonly int id_ClimbLadderTrigger = Animator.StringToHash("ClimbLadderTrigger");
    public static readonly int id_CastTrigger = Animator.StringToHash("CastTrigger");
    public static readonly int id_CastEndTrigger = Animator.StringToHash("CastEndTrigger");

    #endregion

    void Awake()
    {
        animator = GetComponent<Animator>();
        locomotion = GetComponent<LocomotionSystem>();
        animator.applyRootMotion = false;
    }

    void OnEnable()
    {
        idleCheckTime = Random.Range(idleCheckTimeMin, idleCheckTimeMax);
    }

    void Update()
    {
        DoIdle();
    }

    // TODO: consider not doing this here.  seems odd to put it here.
    void DoIdle()
    {
        currentIdleCheckTime += Time.deltaTime;
      
        if (animator.GetBool(id_Moving) == false && currentIdleCheckTime >= idleCheckTime)
        {
            idleCheckTime = Random.Range(idleCheckTimeMin, idleCheckTimeMax);
            currentIdleCheckTime = 0;
            animator.SetInteger(id_Action, 1);
            animator.SetTrigger(id_IdleTrigger);
        }
    }

    public void SetRelaxed()
    {
        animator.SetInteger(id_Weapon, -1);
        animator.SetInteger(id_LeftRight, 0);
        animator.SetInteger(id_LeftWeapon, 0);
        animator.SetInteger(id_RightWeapon, 0);
        animator.SetTrigger(id_InstantSwitchTrigger);
    }

    // This is messy because of the animator state machine that we are using is messy.
    public void SetAnimatorWeaponConfig(WeaponSystem system)
    {
        this.weaponConfig = system.weaponConfig;

        animator.SetInteger(id_LeftRight, 0);
        animator.SetInteger(id_LeftWeapon, 0);
        animator.SetInteger(id_RightWeapon, 0);

        EWeaponAnimationHand hand = EWeaponAnimationHand.Right;
        EWeaponType weaponType = weaponConfig.animationType;

        switch (weaponType)
        {
            case EWeaponType.Two_Hand_Sword:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.TWOHANDSWORD);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.TWOHANDSWORD);
                break;

            case EWeaponType.Two_Hand_Club:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.TWOHANDCLUB);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.TWOHANDCLUB);
                break;

            case EWeaponType.Two_Hand_Spear:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.TWOHANDSPEAR);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.TWOHANDSPEAR);
                break;

            case EWeaponType.Two_Hand_Axe:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.TWOHANDAXE);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.TWOHANDAXE);
                break;

            case EWeaponType.Two_Hand_Bow:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.TWOHANDBOW);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.TWOHANDBOW);
                break;

            case EWeaponType.Two_Hand_Crossbow:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.TWOHANDCROSSBOW);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.TWOHANDCROSSBOW);
                break;

            case EWeaponType.Staff:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.STAFF);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationType.STAFF);
                break;

            case EWeaponType.Dagger:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger(id_LeftRight, (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger(id_LeftWeapon, (int)EWeaponAnimationArmedType.LEFT_DAGGER);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationArmedType.RIGHT_DAGGER);

                }
                break;

            case EWeaponType.Sword:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger(id_LeftRight, (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger(id_LeftWeapon, (int)EWeaponAnimationArmedType.LEFT_SWORD);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationArmedType.RIGHT_SWORD);

                }
                break;

            case EWeaponType.Mace:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger(id_LeftRight, (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger(id_LeftWeapon, (int)EWeaponAnimationArmedType.LEFT_MACE);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationArmedType.RIGHT_MACE);

                }
                break;

            case EWeaponType.Pistol:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger(id_LeftRight, (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger(id_LeftWeapon, (int)EWeaponAnimationArmedType.LEFT_PISTOL);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationArmedType.RIGHT_PISTOL);

                }
                break;

            case EWeaponType.Unarmed:
                animator.SetInteger(id_Weapon, (int)EWeaponAnimationType.UNARMED);
                animator.SetInteger(id_LeftRight, (int)hand);
                animator.SetInteger(id_AttackSide, (int)hand);
                animator.SetInteger(id_LeftWeapon, (int)EWeaponAnimationArmedType.UNARMED);
                animator.SetInteger(id_RightWeapon, (int)EWeaponAnimationArmedType.UNARMED);

                break;

            default:
                break;
        }

        animator.SetTrigger(id_InstantSwitchTrigger);
    }

    public void SetVelocity(Vector3 velocity)
    {
        animator.SetFloat(id_AnimationSpeed, animationSpeed);

        // Side velocity
        float velocityX = transform.InverseTransformDirection(velocity).x / locomotion.maxSideSpeed;

        //// Forward velocity
        float velocityZ = transform.InverseTransformDirection(velocity).z / locomotion.maxForwardSpeed;

        // the animator expects ranges of -6,6 for some odd fucking reason, so we map them into that range from -1, 1 here.
        Vector3 animatorVelocity = new Vector3(velocityX.Map(-1, 1, -6, 6), 0.0f, velocityZ.Map(-1, 1, -6, 6));

        if (animatorVelocity.magnitude > 0.01f)
        {
            animator.SetBool(id_Moving, true);
            animator.SetFloat(id_Velocity_Z, animatorVelocity.z, 0.1f, Time.deltaTime);
            animator.SetFloat(id_Velocity_X, animatorVelocity.x, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetBool(id_Moving, false);
            animator.SetFloat(id_Velocity_Z, 0);
            animator.SetFloat(id_Velocity_X, 0);
        }
    }

    public void SetAnimator_Trigger(int id)
    {
        animator.SetTrigger(id);
    }

    public void SetAnimator_Float(int id, float value)
    {
        animator.SetFloat(id, value);
    }

    public void SetAnimator_Int(int id, int value)
    {
        animator.SetInteger(id, value);
    }

    public void SetAnimator_Bool(int id, bool value)
    {
        animator.SetBool(id, value);
    }

    #region Animation EVents
    public void Hit()
    {
    }

    public void Shoot()
    {
    }

    public void FootR()
    {
    }

    public void FootL()
    {
    }

    public void Land()
    {
    }
    #endregion
}
