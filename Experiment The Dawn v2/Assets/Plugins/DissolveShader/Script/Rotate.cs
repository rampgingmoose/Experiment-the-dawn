using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
   public float RotationSpeed = 50f;
    void Update()
    {
        transform.Rotate(0 , RotationSpeed* Time.deltaTime  , 0);
    }
}
