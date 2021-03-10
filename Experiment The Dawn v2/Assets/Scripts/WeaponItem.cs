using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    [CreateAssetMenu(menuName = "Items/Weapon")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Idle Animations")]
        public string right_Hand_Idle;
        public string left_Hand_Idle;

        [Header("One Handed Attack Animation")]
        public string OH_Light_Attack_01;
        public string OH_Heavy_Attack_01;
        public string OH_Light_Attack_02;
        public string OH_Heavy_Attack_02;
        public string OH_Special_Attack;

        [Header("Stamina Cost")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Weapon Sound FX")]
        public AudioClip lightAttackSoundFX;
        public AudioClip heavyAttackSoundFX;
    }
}
