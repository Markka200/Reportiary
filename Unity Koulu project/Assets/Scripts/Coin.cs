using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Nopeus kerroin kolikon py�rimiselle
    public float rotationSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Py�ritet��n kolikkoa jatkuvasti (y akselilla)
        this.transform.Rotate(Vector3.up * rotationSpeed);
    }
}
