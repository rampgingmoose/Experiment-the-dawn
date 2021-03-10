using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ST
{
    public class EnemyHealthBar : MonoBehaviour
    {
        public Slider slider;

        public Color healthBarColor;

        public Vector3 offSet;
       
        private void Update()
        {
            slider.transform.position = transform.parent.position + offSet;
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
        public void SetCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}
