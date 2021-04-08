


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

        public float dashcap = 25001;
        // Hypyn voimakkuus
        public float dash = 25000;

    public float dashconsume = 50;
        // Ker‰tyt kolikot
        public int collectedCoins = 0;

        public int drag = 20;

        public int airdrag = 0;

        public float Jumpheight = 555;

        public Rigidbody rb;

        Vector3 jump = new Vector3(0, 0, 0);

    bool gravitySwitch;
        // Rigidbody komponentint referenssi, joka haetaan Start -metodissa
        // Private => Ei n‰y


        bool grounded = true;

        // Start is called before the first frame update
        void Start()
        {
            // Noudetaan samaan GameObjektiin liitetty "RigidBody" komponentti
            // Jos RigidBodya ei ole, tulee "rb" olemaan "null" ja "FixedUpdate"-metodin sis‰ll‰ oleva liikkumisen logiikka ei toimi
            rb = GetComponent<Rigidbody>();
        }


        private void Update()
        {
        Move();



        if (dash <= dashcap)
            {
                dash++;
            }
            if (Input.GetKeyDown(KeyCode.F)) // Kun painetaan kerran Spacebar -n‰pp‰int‰, toteutetaan Jump -metodi
            {
                Debug.Log("dash power == " + dash);
                dashmethod();

            }


            if (Input.GetKeyDown(KeyCode.G))
            {
                gravitySwitch = !gravitySwitch;
                if (gravitySwitch)
                {
                    Physics.gravity = new Vector3(0, 9.81f, 0);
                }
                else if (!gravitySwitch)
                {
                    Physics.gravity = new Vector3(0, -9.81f, 0);
                }
            }







   


        }

        /// <summary>
        ///  Fixed Update p‰ivitt‰‰ joka fysiikka frame, eli perustuu koneen ja pelin "Frames per Second":iin
        /// </summary>
        private void FixedUpdate()
        {
         
           



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
                    Debug.Log("geavity = " + rb.useGravity);


                }
                else
                {
                    rb.useGravity = true;
                    grounded = false;
                    rb.drag = airdrag;
                    Debug.Log("geavity = " + rb.useGravity);
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
            if (dash >= 0)
            {
                if (grounded == false)
                {
                    float weakdash = dash / 15;
                    Debug.Log("weak dash == ");
                    float horizontal = Input.GetAxis("Horizontal");
                    float vertical = Input.GetAxis("Vertical");


                transform.Translate(new Vector3(horizontal * weakdash, weakdash + weakdash, vertical * weakdash));


                    dash = dash - dashconsume;
                }
                else
                {

                    float horizontal = Input.GetAxis("Horizontal");
                    float vertical = Input.GetAxis("Vertical");


                    dash = dash - dashconsume;

                    Vector3 MoveDir2 = new Vector3(horizontal * dash, 0, vertical * dash);
                    rb.AddForce(MoveDir2);
                }

            }



        }

      
void Move()
{
       
        if (Input.GetKey(KeyCode.Space) )
        {
            Vector3 horizity2 = (transform.up * Jumpheight) * Time.fixedDeltaTime;
            jump = new Vector3(0, 5, 0);

            print("spacebar is held");

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

