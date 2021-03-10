using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        PlayerStats playerStats;
        public AudioSource audioSource;

        public string lastAttack;

        public float lightStaminaNeededToAttack = 25f;
        public float heavyStaminaNeededToAttack = 50f;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
            playerStats = GetComponent<PlayerStats>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_02, true);
                }

                if (lastAttack == weapon.OH_Heavy_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_02, true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina < lightStaminaNeededToAttack)
                return;

            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_01, true);
            lastAttack = weapon.OH_Light_Attack_01;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina < heavyStaminaNeededToAttack)
                return;

            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_01, true);
            lastAttack = weapon.OH_Heavy_Attack_01;
        }

        public void HandleSpecialAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Special_Attack, true);
            lastAttack = weapon.OH_Special_Attack;
        }
    }
}
