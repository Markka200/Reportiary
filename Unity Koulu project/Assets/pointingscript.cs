using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointingscript : MonoBehaviour
{

    // Start is called before the first frame update

    private GameObject Player;
    public Rigidbody rb;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(GameObject.Find("Player").transform.position);

        LayerMask mask = LayerMask.GetMask("Water");
        RaycastHit osuma;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out osuma, Mathf.Infinity, mask))
        {
            //  Debug.Log("matka maahan " + osuma.distance + osuma + grounded);
            if (osuma.distance <= 20)
            {

                Debug.Log("osuma1" + osuma.collider);


            }
            else
            {
                Debug.Log("osuma2" + osuma.collider);

            }
        }




    }
}

