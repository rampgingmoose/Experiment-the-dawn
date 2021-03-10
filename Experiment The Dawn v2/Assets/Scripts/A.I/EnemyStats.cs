using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;
        public AudioSource audioSource;
        public AudioClip damageSound;

        public EnemyHealthBar enemyHealthBar;
        public GameObject enemyHealthBarUI;
        

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            enemyHealthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;

            enemyHealthBarUI.SetActive(true);
            enemyHealthBar.SetCurrentHealth(currentHealth);

            animator.Play("Damage_01");
            audioSource.PlayOneShot(damageSound);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death_01");
                isDead = true;
                Destroy(gameObject);
            }
        }
    }
}
