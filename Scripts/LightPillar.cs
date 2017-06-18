using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour {

	public GameObject player;
	public GameObject fairy;
	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if(fairy.GetComponent<FairyScript>().energy < fairy.GetComponent<FairyScript>().energyMax)
				fairy.GetComponent<FairyScript>().energy += 20 * Time.deltaTime;
			if(fairy.GetComponent<FairyScript>().energy > fairy.GetComponent<FairyScript>().energyMax)
				fairy.GetComponent<FairyScript>().energy = fairy.GetComponent<FairyScript>().energyMax;
		}
	}
}
