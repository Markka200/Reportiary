using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_text : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
    }

    
    // Update is called once per frame
    void Update()
    {
 
        GameObject.Find("Dashtext").GetComponent<Text>().text = GameObject.Find("Player").GetComponent<PlayerController>().dash.ToString();
 
        if (GameObject.Find("Player").GetComponent<PlayerController>().teleportaika1 != GameObject.Find("Player").GetComponent<PlayerController>().teleportaika)
        {

            // GameObject.Find("Teleportvalmis").GetComponent<Text>().text = decimal.Round(decimal.Parse(GameObject.Find("Player").GetComponent<PlayerController>().teleportaika1.ToString()), 3).ToString();
            GameObject.Find("Teleportvalmis").GetComponent<Text>().text = GameObject.Find("Player").GetComponent<PlayerController>().teleportaika1.ToString();
        }
        else

        {

            GameObject.Find("Teleportvalmis").GetComponent<Text>().text = ("Valmis");
        }
    }
}

