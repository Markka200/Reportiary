using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    float xxx;

    public Transform player;



    public float mousesens = 555f;
    public float mousesensup = 555f;

    public bool rotate = true;

  
   public bool yyy = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate == true)
        {
           
               
            
                float vertical = Input.GetAxis("Mouse X") * Time.deltaTime * mousesens;

            player.Rotate(Vector3.up * vertical);   
            if (yyy == true)
            {   
                float horizontal = Input.GetAxis("Mouse Y") * Time.deltaTime * mousesensup;
                player.Rotate(Vector3.left * vertical);
            }
        }
    }




}
