using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousey : MonoBehaviour
{
    float xxx2;

    

    public Transform Camera;

    public float mousesens2 = 555f;

    public bool rotate2 = true;


    public bool yyy2 = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotate2 == true)
        {

            float horizontal = Input.GetAxis("Mouse Y") * Time.deltaTime * mousesens2;

            if (yyy2 == true)
            {
                xxx2 -= horizontal;
                xxx2 = Mathf.Clamp(xxx2, -90f, 90f);
                transform.localEulerAngles = new Vector3(xxx2, 0f, 0f);
            }

        }
    }




}
