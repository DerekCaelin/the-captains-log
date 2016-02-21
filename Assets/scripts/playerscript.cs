using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerscript : MonoBehaviour {
//scripts
	public GameObject gameManager;
	public GameManager gmscript;
	public GameObject sol;
	storybox Storybox;
	solscript SolScript;

	public GoogleAnalyticsV3 googleAnalytics;
	public Canvas canvasThing;
	bool mobile;
	public Button MakeWormHoleButton;
	
	public Transform startMarker;
	Quaternion startRot;
	Quaternion starTargetRot;
	Vector3 starTarget;
	public bool rotated = false;
	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	public Vector3 targetDir;
	private float fracJourney = 0;
	public Transform target;
	public float speedmod = 1;
	public bool moving = false;
	bool wormholejumping;
	public bool approaching = false;
	public Vector3 gameStart;
	TrailRenderer trail;
	public string currentStar;
	public bool reachedSol;
	public bool atObsStar = true;
	public bool showEarthDist = false;
	public int distanceToEarth;

	//Instructions
	bool showTutorial;
	int tutNum = 0;
	Text instructText; //text area that holds game instructions text
	GameObject instructCanvas; //the canvas

	public int jumps;

	//guages
	public GUIText notification;
	string targetResource;
	float targetResourceAmt;
	public bool filling= false;
	
	public float fuelF = 100f;
	public float waterF = 50f;
	public float foodF = 100f;
	public float moraleF = 100f;
	public float observation;
	public float researchF;

	public Slider fuelSlider;
	public Slider waterSlider;
	public Slider foodSlider;
	public Slider wormSlider;
	public Slider researchSlider;

	public Text jumpUI;
	public Text crewUI;

	//fuel
	bool fuelout = false;

	//sounds
	public AudioClip fueloutsound;
	public AudioClip fuelrechargesound;
	public AudioClip lowfuel;
	public AudioClip wormholeenter;	
	public AudioClip researchStart;
	public AudioClip click;
	public bool startReadingEntries = false;
	public bool readinglock = false; //stops you from flipping the bool above
	float pitchtarget;

	public GUIStyle observationStyle;
	public GUIStyle fuelBar;
	public GUIStyle waterBar;
	public GUIStyle foodBar;
	public GUIStyle moraleBar;
	public GUIStyle researchBar;
	public GUIStyle crewJumpScience;
	public GUIStyle tutorial;
	public GUIStyle tutorialText;
	public GUIStyle tutorialOK;

	public Sprite redPlanet;
	public Sprite greenPlanet;
	public Sprite bluePlanet;
	public Sprite pinkPlanet;
	public Sprite tealPlanet;
	public Sprite solImage;

	public Texture infoButton;
	bool showInfoButton = true;
	public Texture researchButton;
	public Texture wormholeButton;
	public Sprite wormImage;
	
	public bool famine;
	public bool thirst;
	float crewF = 77f;
	public int crew = 77;
	int crewComp;
	float counter = 1;
	

	//star
	bool clickedOnStar;
	starscript StarScript;
	Text starNameText;
	public GUIStyle starNameGUIStyle; //White text
	public GUIStyle starNameGUIStyleb; //shadow
	public GUIStyle distanceGUI;
	public GUIStyle starNameGUIStylec; //shadow2

	public string starName = "";
	bool showStarName = true;
	public Color alpha;
	public Color balpha;
	public Color calpha; //earth distance text
	public Color dalpha; //earth distance text shadow
	Vector3 screenPos;
	Vector3 screenPosB;
	public string lastStarVisited = "";
	public Texture TargetTexture;
	public Texture missionChoice;
	public Texture black;
	public Texture[] missionChoices;
	public bool showMissionButton;

	//wormholes
	public GameObject WormholeStart;
	public GameObject WormholeEnd;
	bool nextClickDestroysWormhole;
	bool showWormholeButton = false;

	//crew
	bool crewdead;

	//puslars
	//public int pulsarNo = 0;

	float resourceBonus; //resource from player's bonus;
	float missionResource; //resource from going on a mission;

	//research
	public bool showResearch = false;
	bool showResearchButton = true;
	bool researching;
	string researchTopic;
	float jumpEfficiencyBonus = 0f;
	float foodConsumptionBonus =1f;
	float waterConsumptionBonus = 1f;
	float fuelFindBonus = 1f;
	float waterFindBonus = 1f;
	float foodFindBonus = 1f;
	public float jumpRangeBonus = 1f;
	public string currentResearch;
	string researchToShow = "RESEARCH";
	public Texture[] littleicons;
	string[] researchOptions = new string[]{
		"stardrive efficiency",
		"stardrive range",
		"water consumption rate",
		"water extraction",
		"food consumption rate",
		"foodmatter extraction",
		"???"
	};

	//sound
	public AudioClip[] sfx;
	public AudioSource sound;
	public AudioSource effect;
	bool playedWarpSound = false;

	public soundtrack Soundtracker;

//Fade From Black
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	bool sceneStarting = true;      // Whether or not the scene is still fading in.
	public float theTimeThatEverythingHappens = 17f;
	bool thatTimeBool;

//info 
	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;

//new UI
	Text distanceText;
	public Text Crew;
	public Text Jumps;
	public Text OKButtonThing;
	public Text Text1;
	public Text Text2a;
	public Text Text2b;
	public Text Text2c;
	public Text Text2d;
	public Text Text3b;
	public Text Text13b;
	public Text DecisionText;
	public Text Choice1Text;
	public Text Choice2Text;


//random stuff
	public	AnimationClip clippy; 


	// Use this for initialization
	void Start () {

		Assign ();
		RandomizeResourcesKind();
		ResizeText ();
		CheckPlatform();
		if(mobile){
			Cursor.visible = false;
		}

		MakeWormHoleButton.interactable = false;

	}
	
	// Update is called once per frame
	void Update () {

		if(sceneStarting)
			StartScene();//fade in
		
		/*if (Input.GetKeyDown (KeyCode.A)) {
			observation = 100f;
		}*/

		GUIStuff();
		Controls ();
		Move();
		TrailChanges ();
		Resources ();
		ResourceImpact ();
		Research ();
		EarthMath ();


	}


	void Controls (){
	
		//mouse
			if (( Input.GetMouseButtonDown (0) && moving == false) && reachedSol == false) { 
				startRot = transform.rotation;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit) && moving == false) { //click
					endMarker = hit.transform;
				}

					ClickEffect();
							
				}		
		

		//touch
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began && moving == false && reachedSol == false ) {
				startRot = transform.rotation;
				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit) && moving == false) { //click
					endMarker = hit.transform;
				}

				ClickEffect();
			}              
		}
	}
	
	void ClickEffect(){
		if(lastStarVisited!="") //if just starting out, don't do this
			GameObject.Find(lastStarVisited).GetComponent<starscript>().visited = true; //tell the star you've been at that it has been visited.

		if (endMarker != null && endMarker.name != lastStarVisited) { //clicked on a star
			clickedOnStar = true;
			if(endMarker.name != "Wormhole Entrance"){
				starTarget = endMarker.position + new Vector3(0.0f,5f,0.0f);
			}
			else{
				starTarget = endMarker.position;
			}
			startMarker = transform;
			transform.LookAt(starTarget);
			starTargetRot = transform.rotation;
			transform.rotation = startRot;
			StarScript = endMarker.GetComponent<starscript>();
			targetDir = starTarget - transform.position; 
			journeyLength = Vector3.Distance (startMarker.position, starTarget);
			gmscript.WinkOutAllStars();
			
			jumps+=1;
			Storybox.jumpCount = jumps;
			
			//GameObject.Find("cones").GetComponent<seekcone>().StopSeeking();
			PlayWarp();
			
			
			if (readinglock == false){ //captains log entries will only read after the first jump
				
				readinglock = true;
				showEarthDist = false;
			}
			
			//make thrusters big
			GameObject[] thrusters = GameObject.FindGameObjectsWithTag("Thruster");
			
			//thrusters[0].gameObject.GetComponent<ParticleSystem>().startSize *=1.5f;
			//thrusters[1].gameObject.GetComponent<ParticleSystem>().startSize *=1.5f;
			//thrusters[2].gameObject.GetComponent<ParticleSystem>().startSize *=1.5f;
			
			
		}
	}
	
	void Move(){
		
		if (endMarker != null) {
			moving = true;
			
			if(sound.pitch <=pitchtarget  && approaching != true){
				sound.pitch+=.01f;
			}
			if(sound.pitch >pitchtarget  && approaching != true){
				sound.pitch-=.01f;
			}


			//rotating
			if (rotated == false) {
			
				float step = speed * Time.deltaTime * 2f;
				Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.2F);
				transform.rotation = Quaternion.LookRotation (newDir);
				float angle = Quaternion.Angle (transform.rotation, starTargetRot);

				if (angle <= .12f) {
					startTime = Time.time;
					rotated = true;
				}
			}

			if (rotated == true || approaching == true )
				MoveIt ();

			if(fuelout){
				pitchtarget = .6f;
			}
			else{
				pitchtarget = 1f;
			}

		}
	}

	void FadeInStarName(){
		showStarName = true;
		if (alpha.a < 1) {
			alpha.a += 1 * Time.deltaTime;
			balpha.a += 1 * Time.deltaTime;
		}
	}

	void FadeOutStarName(){
		showStarName = true;
		if (alpha.a > 0) {
			alpha.a -= 1 * Time.deltaTime;
			balpha.a -= 1 * Time.deltaTime;
		}

		if(alpha.a < .1f){ //if alpha has faded out, atObsStar is not true
			atObsStar = false; 
			//distanceGUI.normal.textColor = alpha;
		}
	}

	void Resources(){
				//resource expenditure
				if (foodF > 0 && moving == true && rotated == true) {
						foodF -= .7f * foodConsumptionBonus * Time.deltaTime;
				}
				if (waterF > 0 && moving == true && rotated == true) {
						waterF -= .7f * waterConsumptionBonus * Time.deltaTime;
				}

				//fuel stuff
				if (rotated == true && moving == true && approaching == false && fuelF > 0 && !wormholejumping) {
						fuelF -= ((1-jumpEfficiencyBonus)* 1.3f) * Time.deltaTime;
				}

				if (crewdead)
					fuelF = 100f;
				
				//if fuel goes out
				if (fuelF <= 0 && fuelout != true) {
					fuelout = true;
					
					//play fuel out sound
					AudioSource soundeffect = GameObject.Find("effect").GetComponent<AudioSource>();
					soundeffect.GetComponent<AudioSource>().clip = fueloutsound;
					soundeffect.Play();

					//activate fuel out particle effect
					GameObject.Find("fuel out").GetComponent<ParticleSystem>().Play();

					Storybox.FuelReaction();
				}

				//if you find more fuel after running on empty.
				if (fuelF > 0 && fuelout == true) {
					fuelout = false;
					AudioSource soundeffect = GameObject.Find("effect").GetComponent<AudioSource>();
					soundeffect.GetComponent<AudioSource>().clip = fuelrechargesound;
					soundeffect.Play();

					//deactivate fuel out particle effect
					GameObject.Find("fuel out").GetComponent<ParticleSystem>().Stop();
				}
				

				//morale
				if (moving == true) {
						if (moraleF > 0 && waterF <= 20f || foodF <= 20) {
								//moraleF -= .4f * Time.deltaTime;
						}
				}
				if (moraleF < 0) {
						moraleF = 0f;
				}
				if (moraleF > 100f) {
						moraleF = 100f;
				}

				//impact morale
				if (foodF > 80f) {
						moraleF += .15f * Time.deltaTime;
				}
				if (waterF > 80f) {
						moraleF += .15f * Time.deltaTime;
				}
				if (foodF > 50f && foodF < 80f) {
						moraleF += .15f * Time.deltaTime;
				}
				if (waterF > 80f && waterF < 80f) {
						moraleF += .15f * Time.deltaTime;
				}
				if (foodF > 20f && foodF < 50f) {
						moraleF -= .15f * Time.deltaTime;
				}
				if (waterF > 20f && waterF < 60f) {
						moraleF -= -.15f * Time.deltaTime;
				}
				if (foodF < 20f && foodF > 5f) {
						moraleF -= .30f * Time.deltaTime;
				}
				if (waterF < 20f && waterF > 5f) {
						moraleF -= .30f * Time.deltaTime;
				}
				if (foodF < 5f) {
						moraleF -= .3f * Time.deltaTime;
				}
				if (waterF < 5f) {
						moraleF -= .3f * Time.deltaTime;
				}


				//famine
				if (waterF <= .1f && moving) {
		
						thirst = true;	
						counter -= .03f * Time.deltaTime;
						int counterInt = Mathf.RoundToInt (counter);
						if (1 != counterInt) {
								Death ();
								
						}
				} else
						thirst = false;

				if (foodF <= .1f && moving) {
						famine = true;

						counter -= .03f * Time.deltaTime;
						int counterInt = Mathf.RoundToInt (counter);
						if (1 != counterInt) {
							Death ();
						}
				} else {
						famine = false;
				}
	//	Debug.Log ("Counter Death: " + counter); //shows the death counter

		}

	void Death(){	
			
		if (crew > 1) {
						if (famine == true && thirst != true) {
								string causeOfDeath = "famine";
								Storybox.FamThirstDeath (causeOfDeath);
						}
						if (famine != true && thirst == true) {
								string causeOfDeath = "thirst";
								Storybox.FamThirstDeath (causeOfDeath);
						}

						if (famine == true && thirst == true) {
								string causeOfDeath = "famine and thirst";
								Storybox.FamThirstDeath (causeOfDeath);
						}

				if(crew == 1 && !crewdead){
					//Storybox.CaptainAlone();
					
				}
			}

			counter = 1;
		}


	public void StarInteract(float bonus, string mission){
		//Spawn planets
		resourceBonus = 0;
		resourceBonus = bonus;

		if (StarScript.resourceToGive != 0 || bonus > 0) {
						//check the resource type and amount of the star, set target guage level
						if (StarScript.StarType == "waterF") {
								filling = true;
								int res = Mathf.RoundToInt (StarScript.resourceToGive);
								int restoadd = Mathf.RoundToInt (res+bonus);
								if(mission=="no")
									Notification("+"+restoadd+" water");
								if (mission=="yes")
									Notification("Mission Success! +"+restoadd+" water");
								targetResourceAmt = waterF + StarScript.resourceToGive * waterFindBonus + resourceBonus + missionResource;
								StartCoroutine (FillWaterResource (targetResourceAmt));

						}

						if (StarScript.StarType == "foodF") {
								filling = true;
								int res = Mathf.RoundToInt (StarScript.resourceToGive);
								int restoadd = Mathf.RoundToInt (res+bonus);
								if(mission=="no")
									Notification("+"+restoadd+" foodmatter");
								if (mission=="yes")
									Notification("Mission Success! +"+restoadd+" foodmatter");
								targetResourceAmt = foodF + StarScript.resourceToGive * foodFindBonus + resourceBonus + missionResource;
								StartCoroutine (FillFoodResource (targetResourceAmt));
						}

						if (StarScript.StarType == "fuelF") {
								filling = true;
								int res = Mathf.RoundToInt (StarScript.resourceToGive);
								int restoadd = Mathf.RoundToInt (res+bonus);
								if(mission=="no")				
									Notification("+"+restoadd+" fuelmatter");

								if (mission=="yes")
									Notification("Mission Success! +"+restoadd+" fuelmatter");

								targetResourceAmt = fuelF + StarScript.resourceToGive * fuelFindBonus + resourceBonus + missionResource;
								StartCoroutine (FillFuelResource (targetResourceAmt));
						}

						if (StarScript.StarType == "moraleF") {
								filling = true;
								int res = Mathf.RoundToInt (StarScript.resourceToGive);
								if(mission=="no")	
								Notification("+"+res+" everything");

								if (mission=="yes")
								Notification("Mission Success! +"+res+" everything");

								float f = fuelF + res;
								float w = waterF + res;
								float o = foodF + res;

								StartCoroutine (FillFoodResource (o));
								StartCoroutine (FillWaterResource (w));
								StartCoroutine (FillFuelResource (f));
								
						}
					
						if (StarScript.StarType == "science") {
							filling = true;
							int res = Mathf.RoundToInt (StarScript.resourceToGive);
							int restoadd = Mathf.RoundToInt (res+bonus);
							//if(observation
							targetResourceAmt = observation + StarScript.resourceToGive *20 + resourceBonus + missionResource;
							StartCoroutine (FillObservation (targetResourceAmt));
							if(targetResourceAmt <= 90)
								{Notification("+ Worm Hole Matter");}
							//show the fields
							//SolScript.SetFieldVis(true); This is leftover from the pulsar script.
							

							

						}
				}

		StarScript.ClearValue();
		targetResourceAmt = 0f;
		missionResource = 0f;


	}

	IEnumerator FillWaterResource (float target){

			while(filling ==true){				
				{
					waterF += .6f;
					yield return new WaitForSeconds(.05f);
				}
				
				if(waterF >= 100f){
					waterF = 100f;
					filling = false;
					yield return null;
				}	
			
				if(waterF > target)
				{
					filling = false;
					yield return null;
				}
			}
	}

	IEnumerator FillFoodResource (float target){
		
		
		while(filling ==true){				
			{
				foodF += .6f;
				yield return new WaitForSeconds(.05f);
				
			}
			
			if(foodF >= 100f){
				foodF = 100f;
				filling = false;
				yield return null;
			}	
			
			if(foodF >= target)
			{
				filling = false;
				yield return null;
			}
		}
	}

	IEnumerator FillFuelResource (float target){
		
		
		while(filling ==true){				
			{
				fuelF += .6f;
				yield return new WaitForSeconds(.05f);
				
			}
			
			if(fuelF >= 100f){
				fuelF = 100f;
				filling = false;
				yield return null;
			}	
			
			if(fuelF >= target)
			{
				filling = false;
				yield return null;
			}
		}
	}

	IEnumerator FillMoraleResource (float target){
		
		
		while(filling ==true){				
			{
				moraleF += .6f;
				yield return new WaitForSeconds(.05f);
				
			}
			
			if(moraleF >= 100f){
				moraleF = 100f;
				filling = false;
				yield return null;
			}	
			
			if(moraleF >= target)
			{
				filling = false;
				yield return null;
			}
		}
	}

	IEnumerator FillObservation (float target){
		
		
		while(filling ==true){				
			{
				observation += .6f;
				yield return new WaitForSeconds(.05f);
				
			}
			
			if(observation >= 100f){
				observation = 0;

				showWormholeButton = true;
				MakeWormHoleButton.interactable = true;


				/*GameObject pulsarToActivate;
				pulsarToActivate = SolScript.pulsars[pulsarNo];
				pulsarToActivate.SetActive(true);
				//Debug.Log("should have activated pulsar "+pulsarNo);
				if(pulsarNo<=4)
					pulsarNo += 1;*/ //old pulsar script

				Notification("Ready to Create Worm Hole");
				Storybox.StellarStudyComplete();
				filling = false;
				yield return null;
			}	
			
			if(observation >= target)
			{
				filling = false;
				yield return null;
			}
		}
	}

	void ResourceImpact(){

		//Slow ship if no fuel
		if(fuelF <= 0.1f){
			speed = .125f * speedmod;
		}
		if(fuelF > 0.1f){
			speed = .25f * speedmod;
		}

	}

	void TrailChanges(){
		GameObject cam = GameObject.Find ("Main Camera");
		trail.endWidth = Vector3.Distance (cam.transform.position, gameStart) / 220f + .5f;
	
	}

	void MoveIt(){

		//stop playing turn sound
	if (!wormholejumping) {
		float distCovered = (Time.time - startTime) * speed;
		fracJourney = distCovered / journeyLength;
		if(startMarker != null && starTarget != null)
			transform.position = Vector3.Lerp (startMarker.position, starTarget, fracJourney);

		if (moving && transform.GetComponent<Rigidbody>().velocity.x <= .1f && transform.GetComponent<Rigidbody>().velocity.y <= .1f && transform.GetComponent<Rigidbody>().velocity.z <= .1f){
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, starTarget, step);
		}
	}

	if (wormholejumping) {
			starName = "";
			transform.Translate(Vector3.forward * Time.deltaTime * 200);
			transform.LookAt(starTarget);
			Destroy(GameObject.Find("Wormhole Entrace"));
			FadeInStarName();
			lastStarVisited = "Wormhole Exit";

			if (moving && transform.GetComponent<Rigidbody>().velocity.x <= .1f && transform.GetComponent<Rigidbody>().velocity.y <= .1f && transform.GetComponent<Rigidbody>().velocity.z <= .1f){
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, starTarget, step);
			}
	}
			
			//when first jumping out
			if(Vector3.Distance(starTarget, transform.position)>10) 
			{	
				if(approaching!= true){
					FadeOutStarName();
				}
			}
			
			//approaching target star
			if(Vector3.Distance(starTarget, transform.position)<=15) 
			{
			//stuff that happens onece
			if(approaching != true){
				approaching = true;
				speedmod = 7f;
				startTime = Time.time;
				
				//bleh. these are all the same;
				starName = StarScript.StarName;
				currentStar = endMarker.gameObject.GetComponent<starscript>().StarName;
				lastStarVisited = currentStar;	
				
					if(starName == "Mara"){
						MaraGain(100f);//Everything becomes awesome 
					}

				//tell storybox to tell a story
				if(starName!= "Sol"){
					if(starName != "Wormhole Exit" && starName != "Wormhole Entrance"){
						StarInteract(0,"no");
						int rand = Random.Range(1,3);
						if (rand == 1){
							Storybox.ArriveAtStarStory();
						}
					}

					StarScript.AddAllPlanets ();
				}
				
				if(StarScript.StarType == "science"){
					atObsStar = true;
				}
				
				if(starName == "Sol"){
					reachedSol = true;
					gmscript.reachedSol = true;
					GameEnd();
					
				}

	
				
			}

				//stuff that happens continuously
				FadeInStarName();
				if(wormholejumping == true){
					wormholejumping = false;
					
					startMarker = transform;
					journeyLength = Vector3.Distance (startMarker.position, starTarget);
				}
				rotated = false;
				screenPos = Camera.main.WorldToScreenPoint(starTarget);
				targetDir = new Vector3 (targetDir.x, 0, targetDir.z);
				
				//sound
				sound.pitch -= .01f;
			}

			if (moving && rotated)
				gmscript.moving = true;

			else
				gmscript.moving = false;
			
			//arrived at target star
			if(Vector3.Distance(starTarget, transform.position)<=.25f) 
			{	
				transform.position = starTarget;	
				approaching = false;
				rotated = false;
				endMarker = null;
				moving = false;
				clickedOnStar = false;

				speedmod = 1f;
				StarScript.currentstar = true;

				//stop warp sound
				sound.pitch = 1;
				StartCoroutine( SoundStopNoPop() );
				playedWarpSound = false;

				//report type of star
				//googleAnalytics.LogEvent ("Player", "Planet", StarScript.StarType, 1);


				if(starName == "Sol"){
					GameObject.Find("Sol").GetComponent<solscript>().EndGame();
					
				}

				if(starName == "Wormhole Entrance"){
					JumpToWormOut();
					
				}

				if(starName == "Wormhole Exit"){
					atObsStar = true;
					nextClickDestroysWormhole = true;
				}
				
				//get rid of stars far away from the player, check to see if there are new stars, make new star markers
				gmscript.DestroyNonProximateStars(starTarget);	
				gmscript.CountStars();
				gmscript.MakeStarMarkers();

				//make thrusters small;
				GameObject[] thrusters = GameObject.FindGameObjectsWithTag("Thruster");
				//thrusters[0].gameObject.GetComponent<ParticleSystem>().startSize *=(.66666666f);
				//thrusters[1].gameObject.GetComponent<ParticleSystem>().startSize *=(.66666666f);
				//thrusters[2].gameObject.GetComponent<ParticleSystem>().startSize *=(.66666666f);

				//show mission text
			if(Random.Range(1,5) == 2 && StarScript.StarType != "moraleF" && StarScript.StarType != "science" && reachedSol == false && !crewdead && !StarScript.Wormhole && !StarScript.visited){ //for now, no missions on morale worlds.
					Storybox.Decision("mission decision");
				}

				//this is a failsafe to wink in the stars that... haven't winked. fixes a bug that prevented winkage.
				GameObject[] StarsToWinkIn = GameObject.FindGameObjectsWithTag("Star");
				for(int i=0; i < StarsToWinkIn.Length ; i++){


				starscript thing = StarsToWinkIn[i].GetComponent<starscript>();
				if(thing.currentstar != true && reachedSol != true)
				
				{

					StarsToWinkIn[i].GetComponent<Renderer>().enabled = true;
					thing.winked = true;}
					
				}
				
			}
		}

	void MissionButton(){
	
				
	}

	public void Research(){
		if (researching == true && moving == true) {
			researchF += 1f * Time.deltaTime; // slowly increase research bar
		}


		if (researchF > 100) {
			researchF = 0f;
			if(moving == true){
				if (currentResearch == "jump efficiency")		
				{
					jumpEfficiencyBonus += .1f;
					Notification("+10% star drive efficiency");
				}
			if (currentResearch == 	"jump range")
				{
					jumpRangeBonus *= 1.2f;
					gmscript.rangeMod = jumpRangeBonus;
					Notification("+20% star drive range");
				}
			if (currentResearch == "water efficiency")
				{waterConsumptionBonus *= .95f;
					Notification("+20% water efficiency");
				}
			if (currentResearch == "water extraction")
				{waterFindBonus *= 1.20f;
					Notification("+20% water extraction");
				}
			if (currentResearch == "foodmatter efficiency")
				{foodConsumptionBonus *= .8f;
					Notification("-20% foodmatter efficiency");
				}
			if (currentResearch == "foodmatter extraction")
				{foodFindBonus *= 1.20f;
					Notification("+20% foodmater extraction");
				}
			if (currentResearch == "fuel extraction"){
					fuelFindBonus *= 1.20f;
					Notification("+20% fuelmatter extraction");
			}
				if (currentResearch == "jump speed"){
					speed *= 1.10f;
					Notification("+10% faster jumps");
				}
			
			

				Storybox.ResearchComplete(currentResearch);
				currentResearch = "";
				researching = false;
				StartCoroutine(SetResearchDelayed());
			}
		}
	
	}

	IEnumerator SetResearchDelayed(){
		yield return new WaitForSeconds (5);
		showResearchButton = true;
	}


	public void SetResearch(string whatResearch){
		//play sound
		AudioSource soundeffect = GameObject.Find("effect").GetComponent<AudioSource>();
		soundeffect.GetComponent<AudioSource>().clip = researchStart;
		soundeffect.Play();
		googleAnalytics.LogEvent ("Player", "Research", whatResearch, 1);
		//Debug.Log ("played thing");


		showResearch = false;
		showResearchButton = false;
		researching = true;
		currentResearch = whatResearch;
		researchToShow = currentResearch.ToUpper ();
	}

	void SelectEngineerWhim()
	{
		}

	void PlayWarp(){



		if (playedWarpSound == false) {
			playedWarpSound = true;
			sound.pitch = 0;
			sound.clip = sfx[1];
			if (wormholejumping) {
				sound.clip = sfx[2];
			}
			sound.loop = true;
			sound.Play();
		}
	}
	

	void GameEnd(){
		gameObject.GetComponent<AudioSource>().volume = 0;
		GameObject.Find("Main Camera").GetComponent<Animation>().Play("endmovement");
		GameObject.Find ("Crew2").SetActive (false);
		GameObject.Find ("Jumps2").SetActive (false);
		Soundtracker.PlayATrack();
		gmscript.WinkOutAllStars();
		//google analytics
		googleAnalytics.LogEvent ("Event", "Occured", "End", 1);
		Storybox.Credits ();
	}

	public void Notification (string whatToSay)
	{
		if(!reachedSol && !crewdead){
			GameObject startpoint = GameObject.Find ("thing");
			GUIText popup = Instantiate(notification) as GUIText;
			popup.GetComponent<notificationScript>().go(whatToSay);
			effect.clip = sfx [3];
			effect.Play ();
		}
	}

	void MaraGain(float gain){
		FillMoraleResource (gain);
		FillWaterResource (gain);
		FillFoodResource (gain);
		FillFuelResource (gain);

		if(gain == 100f)
			//googleAnalytics.LogEvent ("Star", "Visited", "Mara", 1);
			googleAnalytics.LogEvent(new EventHitBuilder().SetEventAction("Visited Mara World"));
		else
			googleAnalytics.LogEvent ("Star", "Visited", "A Wonderful Place", 1);
	}

	void EarthMath(){
		if(Time.timeSinceLevelLoad > theTimeThatEverythingHappens && thatTimeBool ==false){
			thatTimeBool = true;
			showTutorial = true;
			showResearch = false;
			NewInstructions();
			showEarthDist = true;
			atObsStar = true;
			gmscript.FirstWink();
			Cursor.visible = true;
			//Jumps.enabled = true;
			//Crew.enabled = true;
			canvasThing.enabled = true;
		}

		screenPosB = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint (sol.transform.position);
		distanceToEarth = Mathf.RoundToInt(Vector3.Distance (transform.position, sol.transform.position) * 5.5f);
	}

	public void MakeWormHole(){
		Destroy(GameObject.Find("Wormhole Exit"));
		Destroy(GameObject.Find("Wormhole Entrance"));
		PlaceWormStart ();
		PlaceWormFinish ();
		MakeWormHoleButton.interactable = false;
	}

	void PlaceWormStart(){
		Vector3 wormSpawnLoc = transform.position + new Vector3 (15, 0, 0);
		Instantiate (WormholeStart, wormSpawnLoc, transform.rotation);
	}

	void PlaceWormFinish (){
		Debug.Log ("Placed");
		float jumplength = Random.Range (12000f, 18000f);
		float jump = jumplength/distanceToEarth;
		if (jump > 1) {
			jump = .5f;		
		}
		Vector3 Worm2SpawnLoc = Vector3.Lerp (transform.position, sol.transform.position, jump);
		Instantiate (WormholeEnd, Worm2SpawnLoc, transform.rotation);
	}

	void JumpToWormOut(){
		endMarker = GameObject.Find ("Wormhole Exit").transform;
		starTarget = endMarker.position + new Vector3(0.0f,5f,0.0f);
		startMarker = transform;
		transform.LookAt(starTarget);
		starTargetRot = transform.rotation;
		rotated = false;
		startRot = transform.rotation;
		StarScript = endMarker.GetComponent<starscript>();
		targetDir = starTarget - transform.position; 
		journeyLength = Vector3.Distance (startMarker.position, starTarget);
		gmscript.WinkOutAllStars();
		showMissionButton = false;
		//GameObject.Find("cones").GetComponent<seekcone>().StopSeeking();
		wormholejumping = true;

		//play initial jump sound
		AudioSource soundeffect = GameObject.Find("effect").GetComponent<AudioSource>();
		soundeffect.GetComponent<AudioSource>().clip = wormholeenter;
		soundeffect.Play ();
		
		//start playing turn sound	
		PlayWarp();
	}



	void OnGUI(){
				//Show Earth
				if (screenPosB [2] > 0 && Time.time > theTimeThatEverythingHappens && reachedSol == false) {
					distanceText.text = "\n"+distanceToEarth+" Lightyears to Home.";
				 	distanceText.transform.position = new Vector3(screenPosB[0], screenPosB[1],0);        
				}


				if (reachedSol == false && Time.time > theTimeThatEverythingHappens) {

						//showstarname
						if (showStarName == true) {
								starNameText.color = alpha;
								starNameText.text = starName;
								starNameText.transform.position = new Vector3(screenPos[0], screenPos[1]+70,0 );
						}
					if(Time.timeSinceLevelLoad >= theTimeThatEverythingHappens && !crewdead){
						//Crew
						//GUI.Box (new Rect (10, 15, 30, 30), "Crew: " + crew, crewJumpScience);
						crewUI.text = "Crew: "+crew;

						//fuel bar
						fuelSlider.value = fuelF/100f;
		
						//water bar
						waterSlider.value = waterF/100f;
						
						//food bar
						foodSlider.value = foodF/100f;
						

						//wormhole bar
						wormSlider.value = observation/100f;
						
						//research bar
						researchSlider.value = researchF/100f;
						

						//info button
						/*if(infoButton == true && showInfoButton){
							if (GUI.Button (new Rect (10, 200, 30, 30), infoButton, crewJumpScience)) {
									showTutorial = true;
									showResearch = false;
									NewInstructions();

							} 
						}*/

						//wormhole button
						/*if(showWormholeButton == true){
							if (GUI.Button (new Rect (50, 200, 30, 30), wormholeButton, crewJumpScience)) {
								MakeWormHole();
								showWormholeButton = false;

								
							}
						} */
			}
			if(Time.timeSinceLevelLoad >= theTimeThatEverythingHappens)
				jumpUI.text = "Jumps: "+jumps;


				}
			
		}
	IEnumerator SoundStopNoPop(){

		float b = sound.volume;

		for (float i = b; i <= 0; i -= .01f) {
			sound.volume = i;		
		}
		sound.Stop ();
		sound.volume = 1f;
		yield return null;
	}

	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(GetComponent<GUITexture>().color.a >= 0.95f)
			// ... reload the level.
			Application.LoadLevel(0);
	}

	//Tie gameobjects, scripts and variables in the scene to the approrpriate 
	public void Assign() {
		gameObject.AddComponent<GUITexture> ();
		GetComponent<GUITexture>().texture = black;
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);// black screen over everything to start
		gameStart = transform.position;
		trail = gameObject.GetComponent<TrailRenderer>();
		Storybox = gameManager.GetComponent<storybox> ();
		gmscript = gameManager.GetComponent<GameManager>();
		sol = GameObject.Find("Sol") as GameObject;
		SolScript = sol.GetComponent<solscript> ();
		GameObject.Find("fuel out").GetComponent<ParticleSystem>().Stop();
		gmscript.rangeMod = jumpRangeBonus;
		distanceText = GameObject.Find ("Distance").GetComponent<Text> ();
		starNameText = GameObject.Find ("Star Name").GetComponent<Text> ();
		//gmscript.WinkOutAllStars();

	}

	void RandomizeResourcesKind(){
		fuelF = Random.Range(75f, 100.1f);
		foodF = Random.Range(75f, 100.1f);
		waterF = Random.Range(75f, 100.1f);
	}


	public void ResizeText(){

		//instructions
		instructCanvas = GameObject.Find ("Instructions Canvas");
		instructCanvas.GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("Title").GetComponent<Text> ().fontSize = Screen.width / 45;
		instructText = GameObject.Find ("InstructionsText").GetComponent<Text> ();
		instructText.fontSize = Screen.width / 50;
		distanceText.fontSize = Screen.width / 45;
		starNameText.fontSize = Screen.width / 40;
		OKButtonThing.fontSize = Screen.width / 40;
		Text1.fontSize = Screen.width / 45;
		Text2a.fontSize = Screen.width / 45;
		Text2b.fontSize = Screen.width / 45;
		Text2c.fontSize = Screen.width / 45;
		Text2d.fontSize = Screen.width / 45;
		Text3b.fontSize = Screen.width / 45;
		Text13b.fontSize = Screen.width / 45;
		DecisionText.fontSize = Screen.width / 40;
		Choice1Text.fontSize = Screen.width / 35;
		Choice2Text.fontSize = Screen.width / 35;
	}

	public void NewInstructions(){

		AudioSource soundeffect = GameObject.Find("effect").GetComponent<AudioSource>();
		soundeffect.GetComponent<AudioSource>().clip = click;
		soundeffect.Play ();

		if (tutNum == 0) {
			instructCanvas.GetComponent<Canvas>().enabled = true;
			showInfoButton = false;
			showEarthDist = false;
			Time.timeScale = 0;

		}


		tutNum += 1;
		SetInstPanel ();

		if (tutNum == 1) {
			instructText.text = "You are a starship captain stranded in the vastness of space. You must find your way home and obtain the resources you need to keep your crew alive.";		
			//This is home image and text
			return;
		
		}
		if (tutNum == 2) {
			instructText.text = "On your way you will stop in many star systems. Planets in each system have resources you can gather to aid you on your journey.";
				//Planets and their values
			return;
				
		}
		if (tutNum == 3) {
			instructText.text = "Your journey home can be shortened by gathering matter to create a wormhole.";		
				//wormhole
		}/*
		if (tutNum == 4) {
			instructText.text = "Keep your guages up. Your resources will effect your crew's morale and your ship's abilities.";		
			//wormhole
		}*/

		if (tutNum == 4) {
			tutNum = 0;
			instructCanvas.GetComponent<Canvas>().enabled = false;
			showInfoButton = true;
			Time.timeScale = 1;
			return;
		}

	}

	void SetInstPanel(){
		if (tutNum == 1) {
			Panel1.SetActive(true);
			Panel2.SetActive(false);
			Panel3.SetActive(false);
		}
		if (tutNum == 2) {
			Panel1.SetActive(false);
			Panel2.SetActive(true);
			Panel3.SetActive(false);
		}
		if (tutNum == 3) {
			Panel1.SetActive(false);
			Panel2.SetActive(false);
			Panel3.SetActive(true);
		}
		/*if (tutNum == 4) {
			Panel1.SetActive(false);
			Panel2.SetActive(false);
			Panel3.SetActive(true);
		}*/
	}

	void GUIStuff(){

		//brighten, dim earth text
		if (showEarthDist == true) {

			if(calpha.a < 1){
				calpha.a += 1 * Time.deltaTime;
				dalpha.a += 1 * Time.deltaTime;
				
			}
		}
		if (showEarthDist == false || reachedSol == true)
		{
			if(calpha.a > 0){
				calpha.a -= 1 * Time.deltaTime;
				dalpha.a -= 1 * Time.deltaTime;
			}
		}

		distanceText.color = calpha;
	}

	void CheckPlatform(){
		if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			mobile = true;
	}

	
}
