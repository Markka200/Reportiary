using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public static float speedw = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    // Update is called once per frame
    void Update()
    {
         speedw = PlayerController.dash;
        Debug.Log(speedw);
    }
}
