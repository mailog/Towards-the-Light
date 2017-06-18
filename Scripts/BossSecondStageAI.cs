using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondStageAI : MonoBehaviour {

	public GameObject target;
	public GameObject[] projectiles;
	public GameObject patt2target_1,patt2target_2,patt2target_3,patt2target_4;
	public GameObject patt3target_1, patt3target_2, patt3target_3, patt3target_4, patt3target_5;
	public float patt2RotationSpeed;
	public float projectileRate;

	public float patternRate;


	private int currPattern;
	private bool projSwitch;
	private float projectileCounter;
	private float patternCounter;
	private System.Random rand;
	private Vector2 dest;
	private int index;
	// Use this for initialization
	void Start () {
		projectileCounter = projectileRate;
		patternCounter = patternRate;
		rand = new System.Random();
		currPattern = (int)Mathf.Round(Random.Range(0.5f,3.49f));
		patt2target_1.transform.position = new Vector2(transform.position.x + 5, transform.position.y);
		patt2target_2.transform.position = new Vector2(transform.position.x - 5, transform.position.y);
		patt2target_3.transform.position = new Vector2(transform.position.x, transform.position.y + 5);
		patt2target_4.transform.position = new Vector2(transform.position.x, transform.position.y - 5);
	}
	
	// Update is called once per frame
	void Update () {
		if(patternCounter > 0){
			switch(currPattern){
				case 1: pattern1(); break;
				case 2: pattern2(); break;
				case 3: pattern3(); break;
				default: break;	
			}
			patternCounter -= Time.deltaTime;
		}
		else{
			patternCounter = patternRate;
			int tmp = currPattern;
			while(currPattern == tmp){
				currPattern = (int)Mathf.Round(Random.Range(0.5f,3.49f));
			}
			patt2target_1.transform.position = new Vector2(transform.position.x + 4, transform.position.y);
			patt2target_2.transform.position = new Vector2(transform.position.x - 4, transform.position.y);
			patt2target_3.transform.position = new Vector2(transform.position.x, transform.position.y + 4);
			patt2target_4.transform.position = new Vector2(transform.position.x, transform.position.y - 4);
		}
	}

	void pattern1(){
		if(projectileCounter <= 0){
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setTarget(target);
				projectileCounter = projectileRate;
		}
		else{
			projectileCounter -= Time.deltaTime;
		}
	}

	void pattern2(){
			patt2target_1.transform.position = new Vector2(transform.position.x + 4, transform.position.y);
			patt2target_2.transform.position = new Vector2(transform.position.x - 4, transform.position.y);
			patt2target_3.transform.position = new Vector2(transform.position.x, transform.position.y + 4);
			patt2target_4.transform.position = new Vector2(transform.position.x, transform.position.y - 4);
		if(projectileCounter <= 0){
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt2target_1.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt2target_2.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt2target_3.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt2target_4.transform.position);
	            projectileCounter = projectileRate;
		}
		else{
			projectileCounter -= Time.deltaTime;
		}
		patt2target_1.transform.RotateAround(transform.position, new Vector3(0,0,1), patt2RotationSpeed * Time.deltaTime);
		patt2target_2.transform.RotateAround(transform.position, new Vector3(0,0,1), patt2RotationSpeed * Time.deltaTime);
		patt2target_3.transform.RotateAround(transform.position, new Vector3(0,0,1), patt2RotationSpeed * Time.deltaTime);
		patt2target_4.transform.RotateAround(transform.position, new Vector3(0,0,1), patt2RotationSpeed * Time.deltaTime);
	}

	void pattern3(){
		patt3target_2.transform.position = new Vector2(transform.position.x, transform.position.y);
		patt3target_1.transform.position = new Vector2(transform.position.x + 2, transform.position.y);
		patt3target_3.transform.position = new Vector2(transform.position.x - 2, transform.position.y);
		patt3target_4.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
		patt3target_5.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
		if(projectileCounter <= 0){
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt3target_1.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt3target_2.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt3target_3.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt3target_4.transform.position);
				index = (int)Mathf.Round(Random.Range(-0.49f,1.6f));
				Instantiate(projectiles[index], transform.position, Quaternion.identity).GetComponent<Projectile>().setDestination(patt3target_5.transform.position);
	            projectileCounter = projectileRate;
		}
		else{
			projectileCounter -= Time.deltaTime;
		}
	}
}
