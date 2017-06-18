using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(camera.transform.position.x, camera.transform.position.y);
	}
}
