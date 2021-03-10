using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        }
    }
}
