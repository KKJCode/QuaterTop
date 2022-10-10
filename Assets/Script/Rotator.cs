using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 60f;
    void Update()
    {
        if(tag=="ITEMH")
        {
            transform.Rotate(90f, 0f, rotationSpeed * Time.deltaTime);
        }
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        
    }
}
