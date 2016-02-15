using UnityEngine;
using System.Collections;

public class soundtrack : MonoBehaviour {

	playerscript Playerscript;
	public AudioClip[] tracks;
	int selection;
	int minutes;

	// Use this for initialization
	void Start () {
		Playerscript = GameObject.Find ("Player").GetComponent<playerscript> ();
	
		PlayOpeningSong ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayATrack(){

		CheckTheMood ();
		HowLong ();
		StartCoroutine(WaitPlayQueue (selection));

	}

	public void PlayOpeningSong(){

		HowLong ();
		StartCoroutine(WaitPlayQueue (12));
		
	}

	void CheckTheMood(){
		//check the morale of the crew, select randomly from the appropriate mood

		float morale = (Playerscript.foodF + Playerscript.waterF) /2; 
		
		if (morale >= 70f) {
			selection = Random.Range(0,4);
		}
		
		if (morale > 30f && morale < 70f) {
			selection = Random.Range(4,8);
		}
		
		if (morale <= 30) {
			selection = Random.Range(8,12);
		}
	}

	void HowLong(){

		minutes = Random.Range (5, 8);
	}

	IEnumerator WaitPlayQueue(int track){

		AudioClip song = tracks [track];
		GetComponent<AudioSource>().clip = song;
		GetComponent<AudioSource>().Play ();
		yield return new WaitForSeconds(minutes * 60);
		PlayATrack ();

	}
}
