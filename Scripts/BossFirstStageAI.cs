using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirstStageAI : MonoBehaviour {

	public GameObject congratsText;
	public GameObject bossMusic, endMusic, circusMusic;
	public GameObject center;
    public GameObject pivot;
    public GameObject lightAttackWeak, lightAttackStrong, noLight;

    private Animator anim;

    public float finalCounter, finalTimer;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        finalCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
		Debug.Log(finalCounter);
		gameObject.transform.position = pivot.transform.position;
		if(lightAttackWeak.activeSelf || lightAttackStrong.activeSelf)
        {
            anim.SetBool("LightOn", true);
        }
        else if (noLight.activeSelf)
        {
            anim.SetBool("LightOn", false);
		}
		if(finalCounter > finalTimer/3){
			gameObject.GetComponent<BossSecondStageAI>().enabled = true;
			bossMusic.SetActive(false);
			circusMusic.SetActive(true);

			}
		if(finalCounter >= finalTimer){
			congratsText.SetActive(true);
			circusMusic.SetActive(false);
			bossMusic.SetActive(false);
			endMusic.SetActive(true);
			lightAttackStrong.SetActive(false);
			lightAttackWeak.SetActive(false);
			noLight.SetActive(false);
			transform.position = center.gameObject.transform.position;
			gameObject.GetComponent<BossSecondStageAI>().enabled = false;
			Time.timeScale = 0f;
		}
		finalCounter += Time.deltaTime;
    }


	void OnCollisionEnter2D(Collision2D other)
		{
			Debug.Log("Heyyyy thereee");
			if(other.gameObject.tag == "Player")
			{
				pivot.gameObject.GetComponent<BossPivot>().reset();
			}
		}

}
