using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject mummy;
	public GameObject player;

	private float spawnCounter, spawnRate;

	// Use this for initialization
	void Start () {
		spawnRate = Random.Range(5f,10f);
		spawnCounter = spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnCounter <= 0){
			GameObject tmp = Instantiate(mummy, transform.position, Quaternion.identity);
			tmp.GetComponent<EasyMeleeEnemy>().room = player.GetComponent<PlayerController>().currRoom;
			tmp.GetComponent<EasyMeleeEnemy>().detect(player);
			spawnRate = Random.Range(10f,15f);
			spawnCounter = spawnRate;
		}
		else{
			spawnCounter -= Time.deltaTime;
		}
	}
}
