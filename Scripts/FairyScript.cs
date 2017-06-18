using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyScript : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;
    public Color lowEnergyEnd;

    public float energy, energyMax;
    public float regen;
    public float strongDuration;

    public bool dead;

    public float flashTimer;

    public GameObject tmpNoLight;
    private GameObject followTarget;
    public GameObject lightAttackWeak;
    public GameObject lightAttackStrong;
    public GameObject NoLight;
    private Vector3 targetPos;
    public float xOffset, yOffset;
    public float moveSpeed;

    public Slider energySlider;

    private Renderer rend;
    private PolygonCollider2D light;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        light = GetComponent<PolygonCollider2D>();
        energySlider.maxValue = energyMax;
        energy = energyMax;
        gameObject.GetComponents<AudioSource>()[3].Pause();
        gameObject.GetComponents<AudioSource>()[5].Pause();
        gameObject.GetComponents<AudioSource>()[6].Pause();
    }
	
	// Update is called once per frame
	void Update () {
        targetPos = new Vector3(followTarget.transform.position.x + xOffset, followTarget.transform.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if (!dead)
        {
            //turn on if no light
            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse2)) && (!lightAttackWeak.activeSelf && !lightAttackStrong.activeSelf))
            {
                lightAttackWeak.SetActive(true);
                lightAttackStrong.SetActive(false);
                NoLight.SetActive(false);
                gameObject.GetComponents<AudioSource>()[0].Play();
            }
            //turn off light
            else if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                lightAttackWeak.SetActive(false);
                lightAttackStrong.SetActive(false);
                NoLight.SetActive(true);
                gameObject.GetComponents<AudioSource>()[1].Play();
            }
            //switch between strong and weak
            else if (Input.GetKeyDown(KeyCode.Mouse0) && energy > 0)
            {
                lightAttackWeak.SetActive(!lightAttackWeak.activeSelf);
                lightAttackStrong.SetActive(!lightAttackStrong.activeSelf);
                NoLight.SetActive(false);
                gameObject.GetComponents<AudioSource>()[2].Play();
            }
            //lose energy
            if (lightAttackStrong.activeSelf)
            {
                energy -= (energyMax / strongDuration) * Time.deltaTime;
                gameObject.GetComponents<AudioSource>()[4].UnPause();
            }
            else
            {
                gameObject.GetComponents<AudioSource>()[4].Pause();
            }
            //turn off strong
            if (energy <= 0 && lightAttackStrong.activeSelf)
            {
                lightAttackWeak.SetActive(true);
                lightAttackStrong.SetActive(false);
                NoLight.SetActive(false);
            }
            if (energy > 0)
            {
                float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
                rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
            }
            if (energy <= 0)
            {
                float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
                rend.material.color = Color.Lerp(colorStart, lowEnergyEnd, lerp);
                if ((Input.GetKeyDown(KeyCode.Mouse0)))
                {
                    gameObject.GetComponents<AudioSource>()[3].Play();
                }
            }
            if (NoLight.activeSelf && energy < energyMax)
            {
                energy += regen * Time.deltaTime;
                if (energy > energyMax)
                    energy = energyMax;
                gameObject.GetComponents<AudioSource>()[6].UnPause();
            }
            else
            {
                gameObject.GetComponents<AudioSource>()[6].Pause();
            }
            if(energy < energyMax / 10)
            {
                gameObject.GetComponents<AudioSource>()[5].UnPause();
            }
            else
            {
                gameObject.GetComponents<AudioSource>()[5].Pause();
            }
            energySlider.value = energy;
        }
        else
        {
            lightAttackWeak.SetActive(false);
            lightAttackStrong.SetActive(false);
            NoLight.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject.tag == "Player" && followTarget == null){
    		followTarget = other.gameObject;
    		NoLight.SetActive(true);
    		tmpNoLight.SetActive(false);
    	}
    }
}
