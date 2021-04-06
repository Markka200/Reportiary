using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pelaaja luokka, joka hallitsee pelaajien kaikkia toimintoja
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Nopeus muuttuja, jonka avulla voidaan tehd‰ pelaajasta nopeampi
    // Public => N‰kyy inspectorissa
    public float speed = 10f;

    public float speedw = 25000;
    // Hypyn voimakkuus
    public float dash = 25000;

    // Ker‰tyt kolikot
    public int collectedCoins = 0;


    // Rigidbody komponentint referenssi, joka haetaan Start -metodissa
    // Private => Ei n‰y
    public Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        // Noudetaan samaan GameObjektiin liitetty "RigidBody" komponentti
        // Jos RigidBodya ei ole, tulee "rb" olemaan "null" ja "FixedUpdate"-metodin sis‰ll‰ oleva liikkumisen logiikka ei toimi
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        
        if (dash <= 50000) 
        {
            dash++;
        }
            if (Input.GetKeyDown(KeyCode.Space)) // Kun painetaan kerran Spacebar -n‰pp‰int‰, toteutetaan Jump -metodi
         {       Jump();
            Debug.Log(dash); Debug.Log(speedw);
            if (dash >= 0 )
            {
               
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");


                dash = dash - 5000;

                Vector3 MoveDir2 = new Vector3(horizontal * dash, 0, vertical * dash);
                rb.AddForce(MoveDir2);
                

            }
         }
    }

    /// <summary>
    ///  Fixed Update p‰ivitt‰‰ joka fysiikka frame, eli perustuu koneen ja pelin "Frames per Second":iin
    /// </summary>
    private void FixedUpdate()
    {
        
        Move(); 
        // Toteutetaan Move -metodi jatkuvasti

    }

    /// <summary>             
    /// Move metodi, joka hallinnoi pelaajan liikkumisen logiikkaa
    /// </summary>
    void Move()
    {
        // Tapa yksi toteuttaa pelaajan liikkuminen
        // Input.GetAxis hakee siis Input Asetuksista "Horizontal" ja "Vertical" arvon.
        // T‰ss‰ palautetaan molemmat arvot erikseen omiin "float" muuttujiin
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Luodaan Movement Direction muuttuja, johon yhdistet‰‰n yll‰ olevat "horizontal" ja "vertical" muuttujat.
        // HUOM: horizontal on yhdistetty "A / D" n‰pp‰imiin ja Vertical on yhdistetty "W / S".
        // T‰ss‰ tilanteessa Vector3 muuttujan "y" arvo on asetettu 0, sill‰ se vaikuttaa objektin "ylˆs alas" liikkeeseen
        Vector3 MoveDir = new Vector3(horizontal, 0, vertical);
      
        //// Tapa kaksi toteuttaa pelaajan liikkuminen
        //// Asetetaan suoraan Vector3 Horizontal ja Vertical arvot, ilman ett‰ niit‰ asetetaan ensin erillisiin muuttujiin
        //Vector3 MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // K‰ytet‰‰n RigidBody komponentin "AddForce(Vector3)" toimintoa, johon yhdistet‰‰n "tyˆntˆ suunta", eli pelaajan antama Movement Direction 
        // Mihin suuntaan halutaan pelaajaa liikuttaa. 
        // (MoveDir * speed) lis‰‰ objektille lis‰‰ nopeutta "speed" muuttujan avulla

        rb.AddForce(MoveDir * speed); 
    
    }
    /// <summary>
    /// Hyppy metodi, joka lis‰‰ (Vector3.up * jumpForce) verran voimaa ylˆsp‰in (eli pallo saa y-akselille voiman: 1 * jumpForce)
    /// </summary>
    void Jump()
    {
        Vector3 MoveDir3 = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        Debug.Log(collectedCoins);



          //  rb.AddForce( MoveDir3 * 50f); // Lis‰t‰‰n voima pallolle

        
    }


    /// <summary>
    ///  Kun pelaaja menee triggerin sis‰lle, toteutetaan automaattisesti t‰m‰ toiminto
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Jos collider (trigger), johon koskettiin sis‰lt‰‰ komponentin "Coin", toteutetaan if-lauseen sis‰ltˆ
        if (other.GetComponent<Coin>())
        {
           
            Destroy(other.GetComponent<Coin>().gameObject); // Tuhoaa kolikko objektin kokonaan kent‰lt‰

            collectedCoins++; // ker‰‰ pelaajalle jatkuvasti pisteist‰. Aina kun ker‰t‰‰ kolikko => lis‰t‰‰n 1 lis‰‰ "collectedCoins" muuttujaan
            Debug.Log(collectedCoins);
            Debug.Log(speedw);

        }
    }
}
