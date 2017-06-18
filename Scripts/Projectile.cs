using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage;
    public float speed;

    public float liveCounter, liveTime;

    private Vector2 target;

   
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == new Vector3(target.x, target.y, transform.position.z))
        {
            liveCounter += Time.deltaTime;
        }
        if(liveCounter >= liveTime)
        {
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject theTarget)
    {
        target = theTarget.transform.position;
    }

    public void setDestination(Vector2 destination)
    {
    	target = destination;	
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }   
}
