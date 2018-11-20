using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// note that I added this quickly to get animations up and running --
// we may need to take a step back and re-consider our options.

// TODO: consider removing the dependence on LocomotionSystem.
// we currently only need it for max forward/side speeds so we can scale the input velocties we send to the animator.
[RequireComponent(typeof(LocomotionSystem))]
[RequireComponent(typeof(WeaponSystem))]
[RequireComponent(typeof(Animator))]
public class HumanoidAnimator : MonoBehaviour
{
    public float animationSpeed = 1.0f;

    Animator animator;
    LocomotionSystem locomotion = null;
    WeaponConfig weaponConfig;

    void Awake()
    {
        animator = GetComponent<Animator>();
        locomotion = GetComponent<LocomotionSystem>();
        animator.applyRootMotion = false;
    }
   
    // This is messy because of the animator state machine that we are using is messy.
    public void SetAnimatorWeaponConfig(WeaponConfig weaponConfig)
    {
        this.weaponConfig = weaponConfig;

        animator.SetInteger("LeftRight", 0);
        animator.SetInteger("LeftWeapon", 0);
        animator.SetInteger("RightWeapon", 0);

        EWeaponAnimationHand hand = EWeaponAnimationHand.Right;
        EWeaponType weaponType = weaponConfig.animationType;

        switch (weaponType)
        {
            case EWeaponType.Two_Hand_Sword:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.TWOHANDSWORD);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.TWOHANDSWORD);
                break;

            case EWeaponType.Two_Hand_Club:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.TWOHANDCLUB);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.TWOHANDCLUB);
                break;

            case EWeaponType.Two_Hand_Spear:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.TWOHANDSPEAR);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.TWOHANDSPEAR);
                break;

            case EWeaponType.Two_Hand_Axe:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.TWOHANDAXE);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.TWOHANDAXE);
                break;

            case EWeaponType.Two_Hand_Bow:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.TWOHANDBOW);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.TWOHANDBOW);
                break;

            case EWeaponType.Two_Hand_Crossbow:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.TWOHANDCROSSBOW);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.TWOHANDCROSSBOW);
                break;

            case EWeaponType.Staff:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.STAFF);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationType.STAFF);
                break;

            case EWeaponType.Dagger:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger("LeftRight", (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger("LeftWeapon", (int)EWeaponAnimationArmedType.LEFT_DAGGER);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger("RightWeapon", (int)EWeaponAnimationArmedType.RIGHT_DAGGER);

                }
                break;

            case EWeaponType.Sword:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger("LeftRight", (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger("LeftWeapon", (int)EWeaponAnimationArmedType.LEFT_SWORD);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger("RightWeapon", (int)EWeaponAnimationArmedType.RIGHT_SWORD);

                }
                break;

            case EWeaponType.Mace:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger("LeftRight", (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger("LeftWeapon", (int)EWeaponAnimationArmedType.LEFT_MACE);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger("RightWeapon", (int)EWeaponAnimationArmedType.RIGHT_MACE);

                }
                break;

            case EWeaponType.Pistol:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.ONEHANDED);
                animator.SetInteger("LeftRight", (int)hand);
                if (hand == EWeaponAnimationHand.Left)
                {
                    animator.SetInteger("LeftWeapon", (int)EWeaponAnimationArmedType.LEFT_PISTOL);
                }
                else if (hand == EWeaponAnimationHand.Right)
                {
                    animator.SetInteger("RightWeapon", (int)EWeaponAnimationArmedType.RIGHT_PISTOL);

                }
                break;

            case EWeaponType.Unarmed:
                animator.SetInteger("Weapon", (int)EWeaponAnimationType.UNARMED);
                animator.SetInteger("LeftRight", (int)hand);
                animator.SetInteger("AttackSide", (int)hand);
                animator.SetInteger("LeftWeapon", (int)EWeaponAnimationArmedType.UNARMED);
                animator.SetInteger("RightWeapon", (int)EWeaponAnimationArmedType.UNARMED);

                break;

            default:
                break;
        }

        animator.SetTrigger("InstantSwitchTrigger");
    }

    public void SetVelocity(Vector3 velocity)
    {
        animator.SetFloat("AnimationSpeed", animationSpeed);

        // Side velocity
        float velocityX = transform.InverseTransformDirection(velocity).x / locomotion.maxSideSpeed;

        //// Forward velocity
        float velocityZ = transform.InverseTransformDirection(velocity).z / locomotion.maxForwardSpeed;

        // the animator expects ranges of -6,6 for some odd fucking reason.
        Vector3 animatorVelocity = new Vector3(velocityX.Map(-1, 1, -6, 6), 0.0f, velocityZ.Map(-1, 1, -6, 6));

        if (animatorVelocity.magnitude > 0.01f)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity Z", animatorVelocity.z, 0.1f, Time.deltaTime);
            animator.SetFloat("Velocity X", animatorVelocity.x, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetFloat("Velocity Z", 0);
            animator.SetFloat("Velocity X", 0);
        }
    }
}
