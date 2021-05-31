

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

    public float dashcap = 251;
    // Hypyn voimakkuus
    public int dash = 250;

    public float dashconsume = 50;
    // Ker‰tyt kolikot
    public int collectedCoins = 0;

    public int drag = 20;

    public int airdrag = 0;

    public float Jumpheight = 555;

    public Rigidbody rb;

    private bool telorted = true;

    public float teleportaika = 10;

    public bool teleportvalmis = true;

    private bool teleporttimer = false;
    double jumpvar;
    float teleportaika1;

    Vector3 jump = new Vector3(0, 0, 0);
    Vector3 dashpower = new Vector3(0, 0, 0);

    bool gravitySwitch;
    // Rigidbody komponentint referenssi, joka haetaan Start -metodissa
    // Private => Ei n‰y


    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        teleportaika1 = teleportaika;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {

        // timer teleportille 
        if (teleporttimer == true)
        {


            if (teleportaika1 > 0)
            {
                teleportaika1 -= Time.deltaTime;
            }
            else
            {
                teleportvalmis = true;
                teleporttimer = false;
                teleportaika1 = teleportaika;
                Debug.Log("teleport = valmis");
            }
        }



        if (Input.GetKeyDown(KeyCode.F)) // Kun painetaan kerran Spacebar -n‰pp‰int‰,  -metodi
        {


            if (teleportvalmis == true)
            {
                teleporttimer = true;

                if (telorted == false)
                {
                    Debug.Log("sijainit == " + rb.transform.position);
                    dashmethod();
                    Vector3 teleports = new Vector3(200, 3, 200);


                    telorted = true;
                    rb.transform.position = rb.transform.position + teleports;
                    teleportvalmis = false;
                }
                else
                {
                    Debug.Log("sijainit == " + rb.transform.position);
                    dashmethod();
                    Vector3 teleports = new Vector3(-200, 3, -200);


                    telorted = false;
                    rb.transform.position = rb.transform.position + teleports;
                    teleportvalmis = false;



                }
            }
        }
        Move();






        if (Input.GetKeyDown(KeyCode.G))
        {
            gravitySwitch = !gravitySwitch;
            if (gravitySwitch)
            {
                Physics.gravity = new Vector3(0, 999.81f, 0);
            }
            else if (!gravitySwitch)
            {
                Physics.gravity = new Vector3(0, -999.81f, 0);
            }
        }










    }

    /// <summary>
    ///  Fixed Update p‰ivitt‰‰ joka fysiikka frame, eli perustuu koneen ja pelin "Frames per Second":iin
    /// </summary>
    private void FixedUpdate()
    {
        if ((grounded == true) && (dashcap >= dash))
        {
            dash = dash + 1;
        }




        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit osuma;

        if (Physics.Raycast(transform.position, Vector3.down, out osuma, Mathf.Infinity, mask))
        {
            //  Debug.Log("matka maahan " + osuma.distance + osuma + grounded);
            if (osuma.distance <= 0.60)
            {
                grounded = true;
                rb.useGravity = true;
                rb.drag = drag;
                Debug.Log("drag = " + rb.useGravity + drag);


            }
            else
            {
                rb.useGravity = true;
                grounded = false;
                rb.drag = airdrag;
                Debug.Log("airdrag = " + rb.useGravity + airdrag);
            }
        }
        else
        {
            rb.useGravity = true;
            grounded = false;
            rb.drag = airdrag;
        }



        // Toteutetaan Move -metodi jatkuvasti

    }





    void dashmethod()
    {




    }


    void Move()
    {


        if ((dash >= 0))
        {

            if (Input.GetKey(KeyCode.Space))
            {

                dash--;
                Vector3 horizity2 = (transform.up * dash);
                jump = horizity2;

                print("spacebar is held");


            }
            else
            {
                jump = new Vector3(0, 0, 0);
            }
        }
        else
        {

            jump = new Vector3(0, 0, 0);


        }




        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");


        Vector3 velocity = (transform.forward * vertical) * speed * Time.fixedDeltaTime;
        Vector3 horizity = (transform.right * horizontal) * speed * Time.fixedDeltaTime;

        rb.velocity = horizity + velocity + jump;



        /// <summary>
        /// Hyppy metodi, joka lis‰‰ (Vector3.up * jumpForce) verran voimaa ylˆsp‰in (eli pallo saa y-akselille voiman: 1 * jumpForce)
        /// </summary>
        /// <summary>
        ///  Kun pelaaja menee triggerin sis‰lle, toteutetaan automaattisesti t‰m‰ toiminto
        /// </summary>
        /// <param name="other"></param>





    }
    void OnTriggerEnter(Collider other)
    {
        // Jos collider (trigger), johon koskettiin sis‰lt‰‰ komponentin "Coin", toteutetaan if-lauseen sis‰ltˆ
        if (other.GetComponent<Coin>())
        {

            Destroy(other.GetComponent<Coin>().gameObject); // Tuhoaa kolikko objektin kokonaan kent‰lt‰

            collectedCoins++; // ker‰‰ pelaajalle jatkuvasti pisteist‰. Aina kun ker‰t‰‰ kolikko => lis‰t‰‰n 1 lis‰‰ "collectedCoins" muuttujaan
                              //  Debug.Log(collectedCoins);

        }
    }
}

