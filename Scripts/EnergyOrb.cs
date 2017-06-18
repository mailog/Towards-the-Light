using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;

    public float restore;
    public float flashTimer;
    private Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {
        flash();
    }

    void flash()
    {
        float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
    }
}
