using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;

        public WeaponItem attackingWeapon;

        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        Animator animator;

        QuickSlotsUI quickSlotsUI;

        PlayerStats playerStats;

        public AudioSource audioSource;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();
            audioSource = GetComponentInParent<AudioSource>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                #region Handle Left Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.left_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                #region Handle Right Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.right_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion
            }
        }

        #region Handle Weapon's Damage Collider

        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollider();
            }
            else if (playerManager.isUsingLeftHand)
            {
                leftHandDamageCollider.EnableDamageCollider();
            }
        }

        public void CloseDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        #endregion

        #region Handle Weapon Stamina Drain
        public void DrainStaminaLightAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }
        #endregion

        public void LightAttackSoundFX()
        {
            audioSource.PlayOneShot(attackingWeapon.lightAttackSoundFX);
        }

        public void HeavyAttackSoundFX()
        {
            audioSource.PlayOneShot(attackingWeapon.heavyAttackSoundFX);
        }
    }
}
