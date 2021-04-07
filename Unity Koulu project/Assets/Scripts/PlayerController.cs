


using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

/// <summary>
/// Pelaaja luokka, joka hallitsee pelaajien kaikkia toimintoja
/// </summary>
    public class PlayerController : MonoBehaviour
    {
        // Nopeus muuttuja, jonka avulla voidaan tehdä pelaajasta nopeampi
        // Public => Näkyy inspectorissa
        public float speed = 10f;

        public float dashcap = 25001;
        // Hypyn voimakkuus
        public float dash = 25000;

        // Kerätyt kolikot
        public int collectedCoins = 0;

        public int drag = 20;

        public int airdrag = 0;

        public float Jumpheight = 555;

        public Rigidbody rb;



        bool gravitySwitch;
        // Rigidbody komponentint referenssi, joka haetaan Start -metodissa
        // Private => Ei näy


        bool grounded = true;

        // Start is called before the first frame update
        void Start()
        {
            // Noudetaan samaan GameObjektiin liitetty "RigidBody" komponentti
            // Jos RigidBodya ei ole, tulee "rb" olemaan "null" ja "FixedUpdate"-metodin sisällä oleva liikkumisen logiikka ei toimi
            rb = GetComponent<Rigidbody>();
        }


        private void Update()
        {


            if (dash <= dashcap)
            {
                dash++;
            }
            if (Input.GetKeyDown(KeyCode.F)) // Kun painetaan kerran Spacebar -näppäintä, toteutetaan Jump -metodi
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







            if (Input.GetKeyDown(KeyCode.Space) && grounded) // Kun painetaan kerran Spacebar -näppäintä, toteutetaan Jump -metodi
            {
                Jump();

            }


        }

        /// <summary>
        ///  Fixed Update päivittää joka fysiikka frame, eli perustuu koneen ja pelin "Frames per Second":iin
        /// </summary>
        private void FixedUpdate()
        {
            transform.rotation = Quaternion.identity;
            Move();



            LayerMask mask = LayerMask.GetMask("Wall");

            RaycastHit osuma;

            if (Physics.Raycast(transform.position, Vector3.down, out osuma, Mathf.Infinity, mask))
            {
                //  Debug.Log("matka maahan " + osuma.distance + osuma + grounded);
                if (osuma.distance <= 0.60)
                {
                    grounded = true;
                    rb.useGravity = false;
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




                    Vector3 MoveDir2 = new Vector3(horizontal * weakdash, weakdash + weakdash, vertical * weakdash);
                    rb.AddForce(MoveDir2);
                    dash = dash - 5000;
                }
                else
                {

                    float horizontal = Input.GetAxis("Horizontal");
                    float vertical = Input.GetAxis("Vertical");


                    dash = dash - 5000;

                    Vector3 MoveDir2 = new Vector3(horizontal * dash, 0, vertical * dash);
                    rb.AddForce(MoveDir2);
                }

            }



        }

        void Jump()
        {

            Debug.Log("Jumpheight == ");

            Vector3 MoveDir = new Vector3(0, Jumpheight, 0);

            rb.AddForce(MoveDir);


        }
        /// <summary>             
        /// Move metodi, joka hallinnoi pelaajan liikkumisen logiikkaa
        /// </summary>
        void Move()
        {
            if (grounded == true)
            {
                // Tapa yksi toteuttaa pelaajan liikkuminen
                // Input.GetAxis hakee siis Input Asetuksista "Horizontal" ja "Vertical" arvon.
                // Tässä palautetaan molemmat arvot erikseen omiin "float" muuttujiin
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                // Luodaan Movement Direction muuttuja, johon yhdistetään yllä olevat "horizontal" ja "vertical" muuttujat.
                // HUOM: horizontal on yhdistetty "A / D" näppäimiin ja Vertical on yhdistetty "W / S".
                // Tässä tilanteessa Vector3 muuttujan "y" arvo on asetettu 0, sillä se vaikuttaa objektin "ylös alas" liikkeeseen
                Vector3 MoveDir = new Vector3(horizontal, 0, vertical);

                //// Tapa kaksi toteuttaa pelaajan liikkuminen
                //// Asetetaan suoraan Vector3 Horizontal ja Vertical arvot, ilman että niitä asetetaan ensin erillisiin muuttujiin
                //Vector3 MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                // Käytetään RigidBody komponentin "AddForce(Vector3)" toimintoa, johon yhdistetään "työntö suunta", eli pelaajan antama Movement Direction 
                // Mihin suuntaan halutaan pelaajaa liikuttaa. 
                // (MoveDir * speed) lisää objektille lisää nopeutta "speed" muuttujan avulla

                rb.AddForce(MoveDir * speed);

            }
            /// <summary>
            /// Hyppy metodi, joka lisää (Vector3.up * jumpForce) verran voimaa ylöspäin (eli pallo saa y-akselille voiman: 1 * jumpForce)
            /// </summary>
            /// <summary>
            ///  Kun pelaaja menee triggerin sisälle, toteutetaan automaattisesti tämä toiminto
            /// </summary>
            /// <param name="other"></param>





        }
        void OnTriggerEnter(Collider other)
        {
            // Jos collider (trigger), johon koskettiin sisältää komponentin "Coin", toteutetaan if-lauseen sisältö
            if (other.GetComponent<Coin>())
            {

                Destroy(other.GetComponent<Coin>().gameObject); // Tuhoaa kolikko objektin kokonaan kentältä

                collectedCoins++; // kerää pelaajalle jatkuvasti pisteistä. Aina kun kerätää kolikko => lisätään 1 lisää "collectedCoins" muuttujaan
                                  //  Debug.Log(collectedCoins);

            }
        }
    }
   
