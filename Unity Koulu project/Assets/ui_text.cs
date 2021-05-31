using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_text : MonoBehaviour
{
    
   
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      Player = GameObject.Find("Player");
 
        
       
        GameObject.Find("Dashtext").GetComponent<Text>().text = Player.GetComponent<PlayerController>().dash.ToString();

    }
}

