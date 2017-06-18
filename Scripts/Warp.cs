using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {


	public GameObject[] allEnemies;
	public GameObject warpTarget;
    public GameObject fairy;
    public float xOffset, yOffset;
    public int toRoom;
    public bool isLocked, enemyRoom, bossRoom;

    public GameObject[] activators;

    void Update(){
    	if(enemyRoom)
    	{
    		bool tmp = false;
    		for(int i = 0; i < allEnemies.Length; i++){
				if(allEnemies[i] != null || !(allEnemies[i] as UnityEngine.GameObject).Equals(null)){
    				tmp = true;
    			}
    		}
    		if(!tmp)
    			unlock();
    	}

    }

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if(!isLocked){
	        if (other.gameObject.tag == "Player")
	        {
	            gameObject.GetComponents<AudioSource>()[0].Play();
	            ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader> ();
	
	            yield return StartCoroutine (sf.FadeToBlack ());
				if(fairy != null)
	            	fairy.gameObject.transform.position = new Vector2(warpTarget.transform.position.x + xOffset, warpTarget.transform.position.y + yOffset);

	            other.gameObject.transform.position = new Vector2(warpTarget.transform.position.x + xOffset, warpTarget.transform.position.y + yOffset);
	            
	            yield return StartCoroutine (sf.FadeToClear ());
	            other.gameObject.GetComponent<PlayerController>().currRoom = toRoom;
			}
			if(bossRoom){
	    		for(int i = 0; i < activators.Length; i++){
	    			activators[i].SetActive(!activators[i].activeSelf);
	    		}
	    		other.gameObject.GetComponent<PlayerController>().GeneralMusic = activators[2];
	    	}
	    }
	}
	
	public void unlock(){
		isLocked = false;
	    gameObject.GetComponents<AudioSource>()[1].Play();
	}
}
