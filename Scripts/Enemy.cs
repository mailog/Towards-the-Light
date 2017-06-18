using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	
	public Color colorStart;
	public Color colorEnd;
	public float health;
	public Slider slider;
	public float damage;
	public bool canDie;
	public GameObject[] drops;
	public GameObject healthBar;
	public GameObject deathAnimation;
	public float flashTimer = 0.25f;
	private Renderer rend;
	private bool takingDamage, stop;
	
	private Animator anim;
	
	// Use this for initialization
	void Start()
	{
		colorStart = Color.white;
		colorEnd = Color.red;
		rend = GetComponent<Renderer>();
		anim = GetComponent<Animator>();
		slider.maxValue = health;
		System.Random rand = new System.Random();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (takingDamage)
		{
			healthBar.SetActive(true);
		}
		else
		{
			healthBar.SetActive(false);
		}
		if (health < 0 && canDie)
		{
			death();
			//Debug.Log("Enemy is dead!");
		}
		if (takingDamage && canDie)
		{
			flash();
		}
		if (!takingDamage && canDie)
		{
			rend.material.color = Color.white;
		}
		slider.value = health;
	}
	
	void flash()
	{
		float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
		rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
	}
	
	void death()
	{
		Instantiate(deathAnimation, transform.position, Quaternion.identity);
		System.Random rand = new System.Random();
		int rng = rand.Next(0, 4);
		switch (rng)
		{
		case 0:
			Instantiate(drops[0], transform.position, Quaternion.identity);
			break;
		case 1:
			Instantiate(drops[1], transform.position, Quaternion.identity);
			break;
		default:
			break;
		}
		Debug.Log("I am dead.");
		Destroy(gameObject);
	}
	
	public void setTakingDamage()
	{
		if(canDie)
			takingDamage = !takingDamage;
	}
	
	public bool isTakingDamage()
	{
		return takingDamage;
	}
}
