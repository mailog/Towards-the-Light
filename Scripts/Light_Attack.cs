using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Attack : MonoBehaviour {

    public float damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.GetType() == typeof(BoxCollider2D))
        {
            var enemy = other.GetComponent<Enemy>();
            //Debug.Log(enemy.name + "'s health: " + enemy.health);
            enemy.health -= damage * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.GetType() == typeof(BoxCollider2D))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.setTakingDamage();
            //Debug.Log(enemy.name + "'s health: " + enemy.health);
            enemy.health -= damage * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.GetType() == typeof(BoxCollider2D))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.setTakingDamage();
            //Debug.Log(enemy.name + "'s health: " + enemy.health);
            enemy.health -= damage * Time.deltaTime;
        }
    }
}
