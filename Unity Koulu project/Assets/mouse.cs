using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    float xxx;

    public Transform player;



    public float mousesens = 555f;

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
            if (yyy == true)
            {   
                float horizontal = Input.GetAxis("Mouse Y") * Time.deltaTime * mousesens;
                xxx -= horizontal;
                xxx = Mathf.Clamp(xxx, -90f, 90f);
                transform.localEulerAngles = new Vector3(xxx, 0f, 0f);
            }
            player.Rotate(Vector3.up * vertical);
        }
    }




}
