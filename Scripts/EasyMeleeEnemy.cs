using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMeleeEnemy : MonoBehaviour {
	
	public int room;
	public bool isObstacle;
	public float moveSpeed, detectMult;
	private float patrolCount, patrolTimer;
	public Vector2[] patrolVectors;
	public GameObject target;
	public GameObject alert;
	private Vector2 targetPos;
	private int round;
	public bool found;
	private bool stop;
	public float alertCounter, alertEnd;
	
	private Vector2 startPosition;
	private Vector2[] patrolPositions;
	private Animator anim;
	// Use this for initialization
	
	void Start ()
	{
		startPosition = gameObject.transform.position;
		round = 0;
		patrolTimer = Random.Range(1, 3);
		anim = GetComponent<Animator>();
		patrolPositions = new Vector2[patrolVectors.Length];
		float xDisplacement = 0;
		float yDisplacement = 0;
		for (int i = 0; i < patrolVectors.Length; i++)
		{
			xDisplacement += patrolVectors[i].x;
			yDisplacement += patrolVectors[i].y;
			patrolPositions[i] = new Vector2(transform.position.x + xDisplacement, transform.position.y + yDisplacement);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currRoom == room)
		{
			if (gameObject.GetComponent<Enemy>().isTakingDamage() && !found)
				detect(GameObject.FindGameObjectWithTag("Player"));
			if (!found)
				patrol();
			else
			{
				alertCounter += Time.deltaTime;
				if (alertCounter >= alertEnd)
				{
					alert.SetActive(false);
				}
				chase();
			}
		}
		else
		{
			transform.position = startPosition;
			gameObject.GetComponent<Enemy>().health = gameObject.GetComponent<Enemy>().slider.maxValue;
		}
	}
	
	void patrol()
	{
		if (patrolCount >= patrolTimer)
		{
			Debug.Log("Switch");
			round++;
			patrolCount = 0;
			stop = false;
		}
		if (round == patrolPositions.Length)
			round = 0;
		targetPos = new Vector2(patrolPositions[round].x, patrolPositions[round].y);
		if (stop)
			targetPos = transform.position;
		anim.SetFloat("MoveX", patrolVectors[round].x);
		anim.SetFloat("MoveY", patrolVectors[round].y);
		transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
		patrolCount += Time.deltaTime;
	}
	
	void chase()
	{
		targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
		transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
		float xDist = targetPos.x - transform.position.x;
		float yDist = targetPos.y - transform.position.y;
		anim.SetFloat("MoveX", xDist);
		anim.SetFloat("MoveY", yDist);
		found = true;
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		stop = true;
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name.Equals("Player") && !found && !isObstacle)
		{
			detect(other.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name.Equals("Player") && !found && !isObstacle)
		{
			detect(other.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name.Equals("Player") && !found && !isObstacle)
		{
			detect(other.gameObject);
		}
	}
	
	public void detect(GameObject theTarget)
	{
		gameObject.GetComponent<AudioSource>().Play();
		found = true;
		moveSpeed *= detectMult;
		target = theTarget;
		alert.SetActive(true);
	}
}
