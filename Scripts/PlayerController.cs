using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject GeneralMusic;
    public GameObject fairy;
    public GameObject gameOver;

    public Color colorStart;
    public Color damageEnd;
    public Color fatigueEnd;

    public int currRoom;

    public float movespeed, runFactor;
    public float fatigueDuration, fatigueFactor;
    private float fatigueCounter;
    private bool stop, fatigued, takingDamage;

    private Animator anim; 

    public float healthMax;
    private float health;
    public Slider runSlider, healthSlider;
    public GameObject runUI;
    public float runMax;
    private float runMeter;
    private float damageTimer, damageFrequency;

    private Renderer rend;

    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponents<AudioSource>()[0].Play();
        damageFrequency = 1.25f;
        damageTimer = 0f;
        stop = false;
        anim = GetComponent<Animator>();
        runMeter = runMax;
        health = healthMax;
        runSlider.maxValue = runMax;
        healthSlider.maxValue = healthMax;
        fatigued = false;
        takingDamage = false;
        rend = GetComponent<Renderer>();
        colorStart = Color.white;
        damageEnd = Color.red;
        fatigueEnd = Color.yellow;
    }
	
	// Update is called once per frame

	void Update ()
    {

        if(health >= healthMax){
        	health = healthMax;
        	}
        if (runMeter >= runMax)
        {
            runUI.SetActive(false);
        }
        else
        {
            runUI.SetActive(true);
        }
        if (health <= 0)
        {
            gameOver.SetActive(true);
            gameOver.GetComponent<AudioSource>().PlayDelayed(0.5f);
            GeneralMusic.SetActive(false);
            fairy.GetComponent<FairyScript>().dead = true;
            Debug.Log("You are dead.");
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
        if (health <= healthMax / 10)
        {
            float lerp = Mathf.PingPong(Time.time, 1f) / 1f;
            rend.material.color = Color.Lerp(colorStart, damageEnd, lerp);
        }
        else
        {
            rend.material.color = colorStart;
        }
        if (!fatigued)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && runMeter > 0)
            {
                movespeed *= runFactor;
                stop = false;
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && !stop)
            {
                movespeed /= runFactor;
            }
            if (Input.GetKey(KeyCode.Mouse1) && !stop)
            {
                if (runMeter > 0)
                    runMeter -= 1.25f * Time.deltaTime;
                else
                {
                    movespeed /= runFactor;
                    movespeed /= fatigueFactor;
                    stop = true;
                    fatigued = true;
                }
            }
            if (!Input.GetKey(KeyCode.Mouse1))
            {
                if (runMeter < runMax)
                    runMeter += 0.5f * Time.deltaTime;
            }
        }
        else
        {
            fatigueCounter += Time.deltaTime;
            if (!takingDamage)
            {
                float lerp = Mathf.PingPong(Time.time, .75f) / .75f;
                rend.material.color = Color.Lerp(colorStart, fatigueEnd, lerp);
            }
            if (fatigueCounter >= fatigueDuration)
            {
                movespeed *= fatigueFactor;
                fatigued = false;
                fatigueCounter = 0;
                rend.material.color = colorStart;
            }
        }
        

        bool isWalking = (Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"))) > 0;

        anim.SetBool("isWalking", isWalking);

        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * movespeed * Time.deltaTime);
        if (isWalking)
        {
            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            gameObject.GetComponents<AudioSource>()[0].UnPause();
        }
        else
        {
            gameObject.GetComponents<AudioSource>()[0].Pause();
        }
        runSlider.value = runMeter;
        healthSlider.value = health;
        damageTimer += Time.deltaTime;
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(health);
        if (collision.collider.gameObject.tag == "Enemy" && damageTimer > damageFrequency)
        {
            gameObject.GetComponents<AudioSource>()[1].Play();
            takingDamage = true;
            if (health >= 0)
            {
                health -= collision.collider.gameObject.GetComponent<Enemy>().damage;
            }
            damageTimer = 0f;
            float lerp = Mathf.PingPong(Time.time, .1f) / .1f;
            rend.material.color = Color.Lerp(colorStart, damageEnd, lerp);
        }
        
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(health);
        if (collision.collider.gameObject.tag == "Enemy" && damageTimer > damageFrequency)
        {
            takingDamage = true;
            gameObject.GetComponents<AudioSource>()[1].Play();
            if (health >= 0)
            {
                health -= collision.collider.gameObject.GetComponent<Enemy>().damage;
            }
            damageTimer = 0f;
            float lerp = Mathf.PingPong(Time.time, .1f) / .1f;
            rend.material.color = Color.Lerp(colorStart, damageEnd, lerp);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            damageTimer = 0f;
            takingDamage = false;
            rend.material.color = colorStart;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HealthOrb" && health < healthMax)
        {
            gameObject.GetComponents<AudioSource>()[2].Play();    
            health += collision.gameObject.GetComponent<HealthOrb>().restore;
            Destroy(collision.gameObject);
            if (health > healthMax)
                health = healthMax;
        }
        else if (collision.gameObject.tag == "EnergyOrb" && fairy.GetComponent<FairyScript>().energy < fairy.GetComponent<FairyScript>().energyMax)
        {
            gameObject.GetComponents<AudioSource>()[2].Play();
            fairy.GetComponent<FairyScript>().energy += collision.gameObject.GetComponent<EnergyOrb>().restore;
            Destroy(collision.gameObject);
            if (fairy.GetComponent<FairyScript>().energy > fairy.GetComponent<FairyScript>().energyMax)
                fairy.GetComponent<FairyScript>().energy = fairy.GetComponent<FairyScript>().energyMax;
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            gameObject.GetComponents<AudioSource>()[1].Play();
            takingDamage = true;
            if (health >= 0 && health <= healthMax)
            {
                health -= collision.gameObject.GetComponent<Projectile>().damage;
            }
            damageTimer = 0f;
            float lerp = Mathf.PingPong(Time.time, .1f) / .1f;
            rend.material.color = Color.Lerp(colorStart, damageEnd, lerp);
        }
    }
}
