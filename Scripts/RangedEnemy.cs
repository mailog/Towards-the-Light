using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedEnemy : MonoBehaviour {

    public int room;
    public GameObject projectile;
    public GameObject alert;
    public float delayCounter, projectileDelay;
    public bool found;
    public float alertCounter, alertEnd;

    public Animator anim;
    private GameObject target;
	// Use this for initialization
	void Start () {
        delayCounter = projectileDelay;
        anim = GetComponent<Animator>();
        gameObject.GetComponents<AudioSource>()[1].Play();
        gameObject.GetComponents<AudioSource>()[1].Pause();
    }
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("Attacking", false);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currRoom == room)
        {
            if (found)
            {
                gameObject.GetComponents<AudioSource>()[1].UnPause();
                anim.SetBool("Attacking", true);
                throwProjectile(target);
                alertCounter += Time.deltaTime;
                if (alertCounter >= alertEnd)
                {
                    alert.SetActive(false);
                }
            }
        }
        else
        {
            gameObject.GetComponents<AudioSource>()[1].Pause();
            gameObject.GetComponent<Enemy>().health = gameObject.GetComponent<Enemy>().slider.maxValue;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            anim.SetBool("Attacking", true);
            found = true;
            target = other.gameObject;
            alert.SetActive(true);
        }
    }

    public void throwProjectile(GameObject target)
    {
        if (delayCounter <= 0)
        {
            gameObject.GetComponents<AudioSource>()[1].Pause();
            gameObject.GetComponents<AudioSource>()[0].Play();
            delayCounter = projectileDelay;
            Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>().setTarget(target);
        }
        else
        {
            delayCounter -= Time.deltaTime;
        }
    }
}
