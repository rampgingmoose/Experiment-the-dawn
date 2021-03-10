using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class BlockCharacterCollision : MonoBehaviour
    {
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlocker;

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlocker, true);
        }
    }
}
