using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Nopeus kerroin kolikon pyörimiselle
    public float rotationSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Pyöritetään kolikkoa jatkuvasti (y akselilla)
        this.transform.Rotate(Vector3.up * rotationSpeed);
    }
}
