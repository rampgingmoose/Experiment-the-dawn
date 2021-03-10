using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class PlayerStats : CharacterStats
    {
        public HealthBar healthBar;
        public StaminaBar staminaBar;

        AnimatorHandler animatorHandler;
        PlayerManager playerManager;

        public float staminaRegenerationAmount = 30f;
        public float staminaRegenerationTimer = 0;

        private void Awake()
        {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)
        {
            if (playerManager.isInvulnerable)
                return;

            if (isDead)
                return;

            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Damage_01", true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death_01", true);
                isDead = true;
                //HandlePlayerDeath
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenerationTimer = 0;
            }
            else
            {
                staminaRegenerationTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenerationTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
    }
}
