using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    public float speed = 4f;  //Pelaajan nopeuden kerroin
    public float jumpForce = 7f;   //Pelaajan hypyn voima

    private Rigidbody2D rb; 
    private Animator anim;
    private Sensor_Bandit groundSensor; // <-- Child objektin komponentti, joka tekee "grounded" tarkistukset

    private bool grounded = false; // Onko pelaaja maassa vai ei


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundSensor = GetComponentInChildren<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck(); // <-- Tarkistaa onko pelaaja maassa vai ei

        Move();
        JumpAction();
        AttackAction();
    }

    /// <summary>
    /// Pelaajan liikkumisen metodi
    /// </summary>
    void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal"); // <-- Luetaan näppäimistön "A / D" Liikkuminen

        // Jos horizontal input on suurempi kuin 0 (eli liikutaan oikealle D-näppäimellä) niin muutetaan pelaajan scale (-1, 1, 1)
        // Jos input on pienempi kuin 0 (eli liikutaan vasemmalle päin A-näppäimellä) niin muutetaan pelaajan scale (1, 1, 1)
        if (inputHorizontal > 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputHorizontal < 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        // Lisätään pelaajalle velocity input * speed mukaan. y-akselin velocity on merkattu "rb.velocity.y" joka on yleinen painovoima (vaihdetaan Physics2D asetuksissa)
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

        anim.SetFloat("AirSpeed", rb.velocity.y); // Muutetaan "AirSpeed" parametria, jonka avulla voidaan käynnistää "Jump" animaatio kun pelaaja kävelee esimerkiksi kulmalta alas (tippuu korkealta maahan)
        anim.SetFloat("Speed", rb.velocity.magnitude); // muutetaan animaattorin "Speed" -parametria velocity:n mukaan (rb.velocity on Vector2, joten täytyy käyttää ".magnitude" toimintoa, joka "yhdistää" x ja y akselin velocityn)
    }

    void JumpAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) // <-- Jos pelaaja painaa "Space" näppäintä JA pelaaja hahmo on "grounded", niin voidaan hypätä
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Lisätään Rigidbodylle velocity ylöspäin
            anim.SetTrigger("Jump"); // Käynnistetään "Jump" -trigger parametrin avulla hyppy animaatio
            groundSensor.Disable(0.2f); // disabloidaan 0.2 sekunniksi groundSensor komponentti
        }
    }

    void AttackAction()
    {
        // Kun pelaaja klikkaa hiiren vasenta painiketta, toteutetaan "Attack" animaatio
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// Ground check, eli tarkistetaan onko pelaaja maassa vai ei
    /// </summary>
    void GroundCheck()
    {
        if (!grounded && groundSensor.State()) // <-- Jos pelaaja EI ole maassa (!grounded) JA groundSensor komponentin "State" on true niin pelaaja on laskeutunut maahan
        {
            grounded = true;
        }

        if (grounded && !groundSensor.State()) // <-- Jos pelaaja ON maassa (grounded) JA groundSensor komponentint "STate" on false, niin pelaaja on hypännyt JumpActionin avulla tai tippuu maahan ilmasta
        {
            grounded = false;
        }

        anim.SetBool("Grounded", grounded);
    }
}
