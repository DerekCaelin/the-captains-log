using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;


public class GameManager : MonoBehaviour {

	public ParticleSystem starforge;
	// Use this for initialization
	public GUIStyle timeStyle;

	//cursor
	//public Texture2D cursorTexture;
	//CursorMode cursorMode = CursorMode.Auto;
	//Vector2 hotSpot = new Vector2 (5,5);


	public GoogleAnalyticsV3 googleAnalytics;

	private bool countedStars = false;
	private bool knowStarsPos = false;
	public GameObject player;
	public GameObject stars;
//	public ParticleSystem starforge;
	public playerscript Playerscript;
	public GameObject StarMarker;
	public GameObject []ProximateStars;
	public GameObject []AllStars;
	public ArrayList StarMarkersToLose;

	public bool reachedSol;
	ParticleSystem.Particle [] theStars;
	starscript StarScript;
	public float rangeMod;
	public GameObject Sol;
	public bool endMarker;

	public bool moving;
	
	//time
	DateTime startDate = new DateTime (2342, 01, 31);
	DateTime thisMoment = new DateTime(2342,01,31);
	public string timeNow;
	public double timePassed;

	void Start () {
		//Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
		timeNow = thisMoment.ToLongDateString();
		// Use this for initialization
		starforge = stars.GetComponent<ParticleSystem>();
		Playerscript =  player.GetComponent<playerscript> ();


		}
		
		// Update is called once per frame
		void Update () {



			PassageOfTime ();

			if (countedStars == false ){
				CountStars();
			}
		
			if(countedStars == true && knowStarsPos == false){
				MakeStarMarkers();
			}


		}

		public void CountStars(){//get all the star particles into an array. find proximate starmarkers
			theStars = new ParticleSystem.Particle[starforge.particleCount];

			starforge.GetParticles(theStars);
				if (starforge.particleCount > 0 ) {
					countedStars = true;
				}
			}
			
		public void MakeStarMarkers(){
				//rangeMod = Playerscript.jumpRangeBonus;
				if (reachedSol != true) {
						for (int i=0; i < theStars.Length; i++) { 
			
								if (Vector3.Distance (player.transform.position, theStars [i].position) < (200 * rangeMod)) { //make starmarkers within 250 units of the player
										Instantiate (StarMarker, theStars [i].position, Quaternion.identity);
								}
						}

						SecondWink (); //wink in stars
						knowStarsPos = true;
				}
		}

	public void SecondWink(){
		ProximateStars = GameObject.FindGameObjectsWithTag("Star");
		if (!reachedSol) {
						for (int b = 0; b < ProximateStars.Length; b++) {
								//Debug.Log ("proximate stars "+ ProximateStars.Length);
								StarScript = ProximateStars [b].GetComponent<starscript> ();
								if (Playerscript.endMarker != null && Playerscript.endMarker.position != ProximateStars [b].transform.position) {
										StarScript.Winkin ();
								}
						}
				}
	}

	public void FirstWink(){ //turns on stars at the start of the game
		ProximateStars = GameObject.FindGameObjectsWithTag("Star");
		if (!reachedSol) {
			for (int b = 0; b < ProximateStars.Length; b++) {
				//Debug.Log ("proximate stars "+ ProximateStars.Length);
				StarScript = ProximateStars [b].GetComponent<starscript> ();
				StarScript.SpecialWinkin ();
			}
		}
	}


		public void DestroyNonProximateStars(Vector3 starTarget){
			rangeMod = Playerscript.jumpRangeBonus;
			ProximateStars = GameObject.FindGameObjectsWithTag("Star");
			for (int i=0; i < ProximateStars.Length; i++) { 
			if (Vector3.Distance (starTarget, ProximateStars [i].transform.position) > (200 * rangeMod)) {
							starscript other = ProximateStars[i].GetComponent<starscript>();
							other.DestroySelf();
						}
				
			}
			
		}

	public void WinkOutAllStars(){
				AllStars = GameObject.FindGameObjectsWithTag ("Star");
				for (int i=0; i < AllStars.Length; i++) {
					starscript other = AllStars [i].GetComponent<starscript> ();
					other.Winkout ();
				}

				if (Playerscript.reachedSol == true) {
						for (int i=0; i < AllStars.Length; i++) { 
								starscript other = AllStars [i].GetComponent<starscript> ();
								if (other.Sol == false) {
										AllStars [i].GetComponent<Renderer>().enabled = false;
								}
						}
				}
		}



	void OnGUI(){

	
	}

	//void OnMouseEnter(){
	//	Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	//}

	//void OnMouseExit () {
	//	Cursor.SetCursor(null, Vector2.zero, cursorMode);
	//}

	void PassageOfTime(){
		if(Playerscript == null)
			player.GetComponent<playerscript> ();

		if (moving) {

			thisMoment = thisMoment.AddMinutes(30);		
			timeNow = thisMoment.ToLongDateString();
			timePassed = (thisMoment - startDate).TotalDays;

		}
	}


		
}


