using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPivot : MonoBehaviour {

	public GameObject target;

	public float spiralTime;

    public float radius;
    public float rotationSpeed;

    public float currentRadius;
    public float spiralCounter;

    public Vector3 initialPosition;

	public float delayCounter, delayTime;
	// Use this for initialization
	void Start () {
		spiralCounter = 0;
		delayCounter = 0;
		currentRadius = radius;
		initialPosition = transform.position;
		transform.position = (initialPosition - target.transform.position).normalized * radius + target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(delayCounter < delayTime){
			delayCounter += Time.deltaTime;
			transform.position = (transform.position - target.transform.position).normalized * radius + target.transform.position;
		}
		else{
			transform.RotateAround(target.transform.position, new Vector3(0,0,1), rotationSpeed * Time.deltaTime);
			transform.position = (transform.position - target.transform.position).normalized * currentRadius + target.transform.position;
		}
	}

	public void reset(){
			transform.position = (initialPosition - target.transform.position).normalized * radius + target.transform.position;
			spiralCounter = 0;
			delayCounter = 0;
			currentRadius = radius;
	}
}
