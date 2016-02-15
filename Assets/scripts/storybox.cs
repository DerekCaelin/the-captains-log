using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class storybox : MonoBehaviour {
	GameManager Gamemanager;
	playerscript Playerscript;
	Transform player;
	
	public GoogleAnalyticsV3 googleAnalytics;
	
	public int width = 500;
	public int height = 100;
	public GUIStyle style;
	public List<string> storyline;
	public List<string> creditsline;
	public List<string> deceasedlist;
	public GUIText notification;
	
	public bool fadingin = false;
	public bool waitingForNewLine = false;
	public float letterPause = 0.001f;
	public AudioClip sound;
	public string theStringOfTheMoment;
	string TypeWriterString;
	string storyStringToAdd;
	public AudioSource Beep;
	
	public Color alpha;
	public int vignetteNo = -1;
	public int creditNo = -1;
	
	//ShipVariables
	string shipName = "The Laconia";
	public int jumpCount = 0;
	public GUIStyle jumpStyle;
	
	//Crew Variables
	public int crew = 77;
	public int crewNo;
	public List<CrewMember> crewmembers = new List<CrewMember>();
	string NameToUse;
	string altName;
	string possessiveToUse;
	string pronounToUse;
	string diroToUse;
	int crewMemberNumber;

	string crew1;
	int crew1no;
	string crew1full; //full name and rank
	string crew1pos;
	string crew1proun;
	string crew1diro;
	string crew2;
	int crew2no;
	string crew2full;
	string crew2pos;
	string crew2pron;
	string crew2diro;
	string crew3;
	int crew3no;
	string crew3full;
	string crew3pos;
	string crew3pron;
	string crew3diro;
	
	//story
	string part1;
	string part2;
	string part3;
	string[] part3statemnts;
	
	//planet statements
	int whichPlanet;
	string whichPlanetWord;
	string addstndth;
	string waddstndth;
	string currentStar;
	int planets;
	
	//playerstats
	string lowestResource;
	float morale;
	float fuel;
	float water;
	float food;
	bool famine;
	bool thirst;
	bool reachedSol;
	public bool creditsRoll;
	bool creditsEnd;
	string resourceOfTheMoment;
	
	//time
	System.DateTime startDate = new System.DateTime (2342,01,31);
	System.DateTime thisMoment = new System.DateTime(2342,01,31);
	string timeNow;
	double timePassed;
	
	//story
	string[] campaign;
	int campaignInt;
	public bool ShowCampaignWindow;
	public bool campaignWindow;
	public GUIStyle campaignStyle;
	public GUIStyle campaignStyle2;
	public GUIStyle menuButton;
	starscript StarScript;
	public string decisionType;
	public GameObject capsule;
	Transform startingLocation;
	Transform endingLocation;
	float portionOfJourneyComplete;
	string journeyStatus;
	
	//random details
	int booksno;
	string[] activities = {
		"a poetry contest",
		"an arm wrestling tournament",
		"a relay race around the ship",
		"a chess tournament",
		"a music performance",
		"a performance of Shakespeare",
		"a tug of war in the cargo bay",
		"a checkers game",
		"a movie night",
		"a board game night",
		"a extra rations feast",
		"an art expo featuring the works made on the journey so far"
	};
	
	string[] vistas = {
		"blue sky","a canyon at sunset", "an ocean", "a skyline - perhaps New York City", "a green forest","a wandering country road","a prarie",
		"the inside of an old church", "the scottish moors", "the deck of a porch somewhere","billowing clouds","a lake with geese","space. As if we didn't see enough of it out the window",
		"the inside of a library","a bunch of stick figures in various lewd positions. That was probably Crewman Daniels",
		"the faces of family members",
		"a scene with small children","a dog park","a winding road","an arboreum","an arboretum","a single pink flower","a smiling child. I wonder whose",
		"a desert vista"
	};
	
	string[] timeOfDay = {"morning","afternoon","evening"};
	
	string[] books = {
		"Pride and Prejudice","A Hitchhiker's Guide to the Galaxy","The Lord of the Rings","The Hobbit","The Martian Chronicles",
		"Rhoald Dahl","Red Mars","The Divine Comedy","The Tempest","Sherlock Homes","The Three Musketeers","The Lost World","Don Quioxte",
		"Jules Verne's 'The Time Machine","Treasure Island","Horatio Hornblower","Great Expectations","Julius Ceasar","Dracula","Paradise Lost",
		"A Tale of Two Cities","On The Orgin of Species","the Quran", "Shel Silverstein poems","Ray Bradbury","Alexandre Dumas","Diaspora",
		"The Great Gatsby","To Kill A Mockingbird", "Jane Eyre", "The Odyssey", "Lord of the Flies","Of Mice and Men","Frankenstein","Huckleberry Finn",
		"A Tale of Two Cities", "Great Expectations","Farenheit 451","Harry Potter","Brave New World","Northanger Abbey","Mansfield Park","Emma",
		"The Three Musketeers", "The Lion, The Witch, and the Wardrobe","The Silver Chair","The Time Machine","The Call of the Wild","The Giver",
		"A Clockwork Organge","The Princecess Bridge","Sherlock Holmes", "The Wind in the Willows","The Magician's Nephew","Dune","Ender's Game",
		"Foundation", "The Pit and the Pendulum", "2001: A Space Odyssey","The Perfect Storm", "The Dark Tower", "The Martian","The Jefferson Bible",
		"Neuromancer","The Forever War","Longitude"
	};
	
	string []LadyNames = {"Mara","Kathryn","Rebecca","Anne","Elizabeth","Anna","Phoebe","Angela","Zainab","Nora", "Afrah",
		"Sally", "Sandra", "Alexis","Fleur","Paige","Sagan","Dora","Esther","Sandy", "Ariel", "Zoe", "Jessie","Nancy","Alma","Flora",
		"Janice", "Rachel","Hannah","Angela","Emily","Tori","Barbarella","Polyxena", "Andrea", "Leah", "Emma","Eleanor","Maryanne","Arwen",
		"Fanny","Julie","Maria","Suzanne","Kelly","Helga","Gabrielle","Dianna","Jennifer",
		"Susan","Margaret","Lisa","Karen","Betty","Hellen","Sandra","Donna","Carol","Ruth","Michelle","Laura","Sarah","Kimberly","Deborah",
		"Jessica","Cynthia","Amy","Kathleen","Stephanie","Janice","Nicole","Rose","Lori","Andrea","Ruby","Tina","Peggy",
		"Leah",};
	
	string []DudeNames = {"Simon","Feonor, high prince of the Noldor","Patrick","Michael","Elijah","Khalid","Mustafa",
		"Marcus", "Ken", "Max", "Arthur", "Gavin", "Derek", "Eric", "Robert","Brian","Chris","Andrew","Peter",
		"Steve","James","Bret","William","John","Reginold","Benjamin","Draco","Harry","Jack","Alden","Liam","Lucas",
		"Jacob","Jack","Logan","Ian","Brody","Mohammad","Hakim","Colin","Jason","Lincoln","Aaron","Alan","Jordan",
		"Owen","Luke","Ryan","Alexander","Matthew","Carter","Max","Samwell","Hunter","Isaac","Wyatt","Ethan",
		"Charlie","Austin","Zachary","Christopher","Tyler","Jed","Noah","Dylan","Henry","Greg","Neil","Tycho","Curt",
		"Cory","Cole","Tim","Harvey","Casey","Noah","Simon","Casey"};
	
	string []LastNames = {"Smith","Jones","O'Brian","Galleck","Brown","Williams","Davis","Miller","LaForge","Wilson","Rodriguez","Lee","Walker",
		"Hall","Allen","King","Wright","Young","Scott","Mitchel","Perez","Campbell","Parker","Evans","Edwards","Sanchez",
		"Moris","Cox","Murphy","Rivera","Cooper","Howard","Ward","Torres","Peterson","Gray","Ramirez","James","Watson","Brooks","Sanders","Price",
		"Wood","Bennet","Barnes","Ross","Henderson","Coleman","Jenkins","Perry","Powell","Alexander","Russel","Griffin","Diaz","Hayes","Myers","Ford",
		"Hamilton","Graham","Sullivan","Wallace","Woods","Cole","West","Jordan","Owens","Reynolds","Fisher","Ellis","Harrison","Gibson","Cruz","Marshall",
		"Cruz","Marshall","Murray","Ortiz","Gomez","Murray","Freeman","Wells","Stevens","Tucker","Hunter","Porter","Hicks","Crawford",
		"Boyd","Mason","Morales","Kennedy","Warren","Dixon","Ramos","Rayes","Burns","Gordon","Shaw","Holmes","Rice","Robertson","Hunt","Black","Potter",
		"Knight","Daniels","Rose","Furguson","Stone","Hawkens","Dunn","Perkins","Spencer","Gardiner","Payne","Pierce","Olson","Ruiz","Hart","Harper","Lane",
		"Armstrong","Carpenter","Weaver","Greene","Lawrence","Elliott","Chavez","Franklin","Lawson","Fields","Ryan","Schmidt","Flemming","Dernavich","Dauphin",
		"Gildea","Flood","Leavitt","Hughes","French","Daily","Watts"
	};
	
	string[] bands = {
		"Pink Floyd","The Beatles","Alt-J","Beethovan","Bach","Michael Jackson","ACDC","Dave Brubeck","Bob Dillan","Miles Davis"
	};
	
	string[] timePeriod = {
		"the military","elementary school","the same neighborhood growing up","grade school","the academy","college","university","summer camp as children"
	};
	
	
	//misc
	string whoHasTheDog;
	
	//menu
	bool menu; 
	GUIStyle menustyle;
	public AudioClip menubuttonsound;
	
	//new ui
	Text StorySpace;
	public GameObject ExitMenu;
	
	//choices
	public GameObject DecisionPanel;
	public Text choiceTextPrompt;
	public Text choice1;
	string choice1string;
	public Text choice2;
	string choice2string;
	string actualchoice;
	int choiceOption;
	public AudioSource decisionSound;
	
	
	//Research Options
	string[] ResearchOptions = new string[]{
		"the Star Drive's efficiency",           //0
		"the jump range for the Star Drive",    //1
		"our fuel extaction rate",              //2
		"our water extraction rate",            //3
		"our water consumption efficiency",     //4
		"our foodmatter extraction rate",       //5
		"the foodmatter consumption efficiency", //6
		"our ship's jump speed"
	};
	
	string[] ResearchOptionsButton = new string[]{
		"jump efficiency",         //0
		"jump range",              //1
		"fuel extraction",         //2
		"water extraction",        //3
		"water efficiency",        //4
		"foodmatter extraction",   //5
		"foodmatter efficiency",    //6
		"jump speed"
	};
	
	bool researching = false;
	
	
	// Use this for initialization
	void Start () {
		
		// ();
		Gamemanager = GameObject.Find ("gamemanager").GetComponent<GameManager> ();
		Playerscript = GameObject.Find ("Player").GetComponent<playerscript> ();
		player = GameObject.Find ("Player").transform;
		crew = Playerscript.crew;
		CreateCrew ();
		InvokeRepeating("Events",2, 30F);
		GetComponent<Animation> ().Play ("openinglines");
		
		
		StorySpace = GameObject.Find ("Story Text").GetComponent<Text>();
		StorySpace.fontSize = Screen.width / 40;
		
		ExitMenu.SetActive(false);
		
		notification.fontSize = Screen.width / 50;
		
		//choose random deets
		ChooseRandomDetail();
		DisableDecisionMenu();
		
	}
	
	void Update () {
		
		FadeWords ();
		UpdatePlayerStats ();
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			BringUpMenu();
		}
	}
	
	public class CrewMember {
		public int number;
		public string rank;
		public string firstName;
		public string lastName;
		public string gender;
		public int age;
		public string possessive;
		public string pronoun;
		public string diro;
	}
	
	void Events(){
		
		DetermineJourneyCompletage();
		
		if (campaignWindow != true && Playerscript.readinglock == true && reachedSol != true) {
			
			//Events that transpire aboard the ship. Not necessarily tied to star jumps.
			int whichStory = Random.Range (1, 11);
			if (whichStory >= 1 && whichStory <4 && crew>1) { 
				ReactToResources ();
			}
			if ( whichStory == 4 || whichStory == 5 || whichStory == 6) {
				
				if(journeyStatus == "starting")
					RandomVignetteStart();
				if(journeyStatus == "middling")
					RandomVignetteA ();
				if(journeyStatus == "nearing end")
					NearlyThereVignette();
				if(journeyStatus == "ending")
					EndingVignette();
			}
			if (whichStory == 7)
				ReactToCrewNumbers();
			if (whichStory >= 8 && researching!=true && crewmembers.Count > 1)
			{
				Decision("research generic");}
		}

		DecideNameToUse();
	}
	
	void CheckStoryVignette(){//if there is another line, play it. If not, enter waiting state.
		if (vignetteNo + 1 > storyline.Count) {
			waitingForNewLine = true;		
		} else
			waitingForNewLine = false;
		
		if (waitingForNewLine == false && alpha.a <= 0) {
			theStringOfTheMoment = storyline[vignetteNo];
			fadingin = true;
			StartCoroutine(TypeText ());
		}
	}
	
	public void AddALine(string NewString){
		storyline.Add (timeNow+"\n"+NewString);
		CheckStoryVignette();
		part1 = part2 = part3 = storyStringToAdd = "";
		ClearParts ();
	}
	
	public void SAddALine(string NewString){//play this for the opening lines.
		storyline.Add (NewString);
		CheckStoryVignette();
		ClearParts ();
	}
	
	void FadeWords(){
		
		StorySpace.color = alpha;
		
		if (fadingin == true) 
			FadeIn();
		
		if(fadingin == false)
			FadeOut();
		
	}
	
	void FadeIn(){
		
		if (waitingForNewLine == false) {
			if (alpha.a < 1) {
				alpha.a += .5f * Time.deltaTime;
			}
			
			if (alpha.a > 1) {
				alpha.a = 1;
				//fadingin = false;
			}
		}
	}
	
	void FadeOut(){
		if (alpha.a > 0) {
			alpha.a -= .5f * Time.deltaTime;
		}
		
		if (alpha.a <0){
			alpha.a = 0;
			TypeWriterString = "";
			vignetteNo += 1;
			CheckStoryVignette ();
			
		}
	}
	
	
	
	void CreateCrew(){
		
		for (int i = 0; i < crew; i++) {
			//Debug.Log(i);
			int numbera = 0;
			int agea = 99;
			string gendera = "female";
			string firstNamea = "Jane";
			string lastNamea = "Doe";
			string ranka = "Captain";
			string possessivea = "her";
			string pronouna = "she";
			string diroa = "her";
			
			numbera = i;
			
			//age
			agea = Random.Range(22, 51);// change for rank appropriateness
			
			//Pick Gender
			int g = Random.Range (1,3);
			if(g == 1){
				gendera = "female";
				possessivea = "her";
				pronouna = "she";
				diroa = "her";
				
			}
			
			if (g == 2){
				gendera = "male";
				possessivea = "his";
				pronouna = "he";
				diroa = "him";
			}
			
			//Pick name
			if (gendera == "female"){
				
				
				firstNamea = LadyNames[Random.Range(0, LadyNames.Length)];
			}
			
			if (gendera == "male"){
				
				
				firstNamea = DudeNames[Random.Range(0, DudeNames.Length)];
			}
			
			
			lastNamea = LastNames[Random.Range(0, LastNames.Length)];
			
			//Pick rank
			if(i==0)//captain
			{agea = Random.Range(35, 50);
				ranka = "Captain";}
			
			if(i==1)//Commander
			{ranka = "Commander";}
			
			if(i>1 && i<=4)
			{ranka = "Lieutenant Commander";}
			
			if(i>4 && i<=20)
			{ranka = "Lieutenant";}
			
			if(i>20 && i<=35)
			{ranka = "Sub-Lieutenant";}
			
			if(i>35)
			{ranka = "Ensign";}
			
			//Class Crewmember
			CrewMember thing = new CrewMember();
			thing.age = agea;
			thing.rank = ranka;
			thing.firstName = firstNamea;
			thing.lastName = lastNamea;
			thing.number = numbera;
			thing.diro = diroa;
			thing.gender = gendera;
			thing.pronoun = pronouna;
			thing.possessive = possessivea;
			//Debug.Log("thing = "+thing.age);
			crewmembers.Add(thing);
			//Debug.Log (thing.firstName +" "+thing.possessive);	
		}

	}
	
	void DecidePlanet(){
		whichPlanet = Random.Range (1, planets );
		wordNumberify(whichPlanet);
		StNdTh (whichPlanet);
		WordStNdTh (whichPlanet);
	}
	
	void DecideResource(){
		
		if (StarScript.StarType == "waterF")
			resourceOfTheMoment ="water";
		
		if (StarScript.StarType == "foodF")
			resourceOfTheMoment ="food";
		
		if (StarScript.StarType == "moraleF")
			resourceOfTheMoment ="morale";
		
		if (StarScript.StarType == "fuelF")
			resourceOfTheMoment ="fuel";
		
		if (StarScript.StarType == "science")
			resourceOfTheMoment ="wormhole matter";
	}
	
	void DecideNameToUse(){
		
		//run this fewer times when you run out of crew members to prevent duplicates.
		int runs = 3;
		if (crewmembers.Count >= 4)
			runs = 3;
		if (crewmembers.Count == 3)
			runs = 2;
		if (crewmembers.Count == 2)
			runs = 1;
		if (crewmembers.Count == 1)
			runs = 0;
		
		for(int q = 1; q <= runs ; q++){
			
			if (timePassed <= 200) {// Totally Formal Designation: Rank, First name, LAst Name
				crewMemberNumber = Random.Range (1, crewmembers.Count);//exclude the captain
				NameToUse = crewmembers[crewMemberNumber].rank +" "+ crewmembers[crewMemberNumber].firstName+" "+crewmembers[crewMemberNumber].lastName;
				altName   = crewmembers[crewMemberNumber].rank +" "+ crewmembers[crewMemberNumber].firstName+" "+crewmembers[crewMemberNumber].lastName;
				pronounToUse = crewmembers[crewMemberNumber].pronoun;
				possessiveToUse = crewmembers[crewMemberNumber].possessive;
				diroToUse = crewmembers[crewMemberNumber].diro;
				crewNo = crewmembers[crewMemberNumber].number;
			}
			
			if (timePassed >=200 && timePassed < 400) {// Somewhat Formal Designation:  Firstname, LAst Name
				crewMemberNumber = Random.Range (1, crewmembers.Count);//exclude the captain
				NameToUse = crewmembers[crewMemberNumber].firstName+" "+crewmembers[crewMemberNumber].lastName;
				altName = crewmembers[crewMemberNumber].rank +" "+ crewmembers[crewMemberNumber].firstName+" "+crewmembers[crewMemberNumber].lastName;
				pronounToUse = crewmembers[crewMemberNumber].pronoun;
				possessiveToUse = crewmembers[crewMemberNumber].possessive;
				diroToUse = crewmembers[crewMemberNumber].diro;
				crewNo = crewmembers[crewMemberNumber].number;
				
			}
			
			if (timePassed >= 400) {// Somewhat informal Designation:  LAst Name
				crewMemberNumber = Random.Range (1, crewmembers.Count);//exclude the captain
				NameToUse = crewmembers[crewMemberNumber].firstName;
				altName = crewmembers[crewMemberNumber].rank +" "+ crewmembers[crewMemberNumber].firstName+" "+crewmembers[crewMemberNumber].lastName;
				pronounToUse = crewmembers[crewMemberNumber].pronoun;
				possessiveToUse = crewmembers[crewMemberNumber].possessive;
				diroToUse = crewmembers[crewMemberNumber].diro;
				crewNo = crewmembers[crewMemberNumber].number;
			}
			
			
			
			if(q==1){
				crew1 = NameToUse;
				crew1no = crewMemberNumber; //crewmembernumber is the entry number in the existing list, not the crewnumber expressed in the class
				crew1full = altName; 
				crew1pos = possessiveToUse;
				crew1diro = diroToUse;
				crew1proun = pronounToUse;
			}
			
			if(q==2){
				crew2 = NameToUse;
				crew2no = crewMemberNumber;
				crew2full = altName;
				crew2pos = possessiveToUse;
				crew2diro = diroToUse;
				crew2pron = pronounToUse;
				
			}
			if(q==3){
				crew3 = NameToUse;
				crew3no = crewMemberNumber;
				crew3full = altName;
				crew3pos = possessiveToUse;
				crew3diro = diroToUse;
				crew3pron = pronounToUse;
			}
		}
		
		if(crewmembers.Count >= 4 && // if there is a repeat in names, and there are more than 3 crew members alive besides the captain...
		   (crew1 == crew2 ||
		 crew1 == crew3 ||
		 crew2 == crew3))
		{DecideNameToUse();}
		
		if(crewmembers.Count >= 3 && // if there is a repeat in names, and there are more than 3 crew members alive besides the captain...
		   (crew1 == crew2))
		{DecideNameToUse();}
		
	}
	
	public void Credits(){
		string credline;
		
		creditsline.Add ("You've made it home.");
		creditsline.Add ("");
		
		timePassed += .5;
		int a = (int) timePassed;
		credline = "Your journey of " + (jumpCount) + " jumps took " + a + " days.";
		creditsline.Add(credline);
		creditsline.Add ("");
		
		
		credline = "Crew lost on the way: " + deceasedlist.Count;
		creditsline.Add (credline); 
		creditsline.Add ("");
		
		if (deceasedlist.Count > 0) {
			credline = "In Memory Of:";
			creditsline.Add (credline);
		}
		
		for (int i = 0; i < deceasedlist.Count; i++) {
			credline = deceasedlist[i];
			creditsline.Add (credline);
		}
		
		creditsline.Add ("");
		creditsline.Add ("Music:");
		creditsline.Add ("Lee Rosevere");
		creditsline.Add ("Derek Caelin");
		creditsline.Add ("DJ Cruzer");
		creditsline.Add ("Chris Zabriske");
		creditsline.Add ("");
		
		creditsline.Add ("Sounds:");
		creditsline.Add ("nathanshadow");
		creditsline.Add ("CosmicD");
		creditsline.Add ("stewdio2003");
		creditsline.Add ("cubicApocalypse");
		creditsline.Add ("peridactyloptrix");
		creditsline.Add ("Sclolex");
		creditsline.Add ("Corsica_S");
		creditsline.Add ("oceanictrancer");
		creditsline.Add ("Niedec");
		creditsline.Add ("sandyrb");
		creditsline.Add ("thunderclap");
		creditsline.Add ("");
		
		creditsline.Add ("Playtesters:");
		creditsline.Add ("/u/TheMoonIsFurious");
		creditsline.Add ("/u/Skwink");
		creditsline.Add ("/u/Birddude1230");
		creditsline.Add ("/u/gustavvonperiwinkle");
		creditsline.Add ("");
		
		creditsline.Add ("Special Thanks to Eric and Casey \n for their guidance and support.");
		creditsline.Add ("");
		creditsline.Add ("Thank you Mara for your encouragement,  \n for your patience, \n for all the ideas (including the name), \n and most of all \n for marrying me.");
		
		InvokeRepeating ( "CreditScroll", 3f, 2f);	
	}
	
	void CreditScroll(){
		
		creditNo += 1;
		
		//Debug.Log (creditsline [creditNo]);
		if (creditsline.Count > creditNo) {
			GUIText popup = Instantiate (notification) as GUIText;
			popup.GetComponent<notificationScript> ().GoLong (creditsline [creditNo]);
		}
		
		if (creditsline.Count <= creditNo) {
			if(GameObject.Find("notification(Clone)") == null)
			{creditsEnd = true;}				
		}
	}
	
	void BringUpMenu(){
		
		if (menu == false) {
			menu = true;
			ExitMenu.SetActive(true);
			Cursor.visible = true;
			Time.timeScale = 0f;
			return;
		};
		
		if (menu == true) {
			menu = false; 
			ExitMenu.SetActive(false);
			Time.timeScale = 1f;
			if (Time.time <= Playerscript.theTimeThatEverythingHappens){
				Cursor.visible = false;
			}
			return;
		};
		
	}
	
	void OnGUI(){
		
		if(creditsEnd)
		{
			if (GUI.Button (new Rect (.75f*Screen.width, Screen.height/2 + 10, 50, 40), "End.", campaignStyle2))
			{
				Application.LoadLevel(0);
			}
		}
	}
	
	
	//STORIES
	//
	//
	public void Quit(){
		
		
		//play sound
		AudioSource soundeffect = GameObject.Find("effect").GetComponent<AudioSource>();
		soundeffect.GetComponent<AudioSource>().clip = menubuttonsound;
		soundeffect.Play ();
		
		
		int gameseconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);		
		Playerscript.googleAnalytics.LogEvent("Button", "Selected", "Quit", gameseconds);
		googleAnalytics.LogEvent ("Player", "Activity", "Jumps", jumpCount);
		
		Application.Quit();
		
	}
	
	
	
	public void ArriveAtStarStory(){
		
		StarScript = GameObject.Find (Playerscript.currentStar).GetComponent<starscript> ();
		
		if (Playerscript.currentStar == "Mara") {
			MaraStory ();		
		} 
		
		if(Playerscript.currentStar != "Wormhole Entrance"){
			planets = StarScript.numplanets;
			string type = StarScript.StarType;
			currentStar = StarScript.name;
			
			//if the captain has 2 or more companions
			if(crewmembers.Count >= 3){
				int whichStory = Random.Range (1, 10);
				if (whichStory >= 1 && whichStory < 4) {
					TravelStory1 (Playerscript.currentStar, planets, type); //enter, react, extract. Structured
				}
				
				if (whichStory >= 4 && whichStory <= 9) {
					TravelStory3 (Playerscript.currentStar, type); // reaction to star. One sentence.
				}
				
				if (whichStory == 10) {
					TravelStory4 (Playerscript.currentStar); // reminder of earth
				}
			}
		}
		
		//if captain has a single companion
		if(crewmembers.Count == 2)
			CaptainAndCompanionVisit(planets);
		
		//if captain visits alone
		if(crewmembers.Count == 1)
			CaptainAloneVisit();
		
		ClearParts ();
		
	}
	
	void wordNumberify(int which){
		
		if (which == 1) { whichPlanetWord = "One";  }
		if (which == 2) { whichPlanetWord = "Two";  }
		if (which == 3) { whichPlanetWord = "Three";}
		if (which == 4) { whichPlanetWord = "Four"; }
		if (which == 5) { whichPlanetWord = "Five";	}
		if (which == 6) { whichPlanetWord = "Six";	}
		if (which == 7) { whichPlanetWord = "Seven";}
		if (which == 8) { whichPlanetWord = "Eight";}
		if (which == 9) { whichPlanetWord = "Nine";	}
		if (which == 10) { whichPlanetWord = "Ten";	}
	}
	
	void TravelStory1(string theCurrentStar, int thePlanets, string theStarType){
		
		whichPlanet = Random.Range (1, thePlanets);
		StNdTh (whichPlanet);
		WordStNdTh (whichPlanet);
		DecideNameToUse ();
		
		//part 1 (entering the system)
		string[] part1statemnts = new string[]{
			"We came to a system we called " + theCurrentStar + ". ",
			"The next jump took us to the " + theCurrentStar + " system. ",
			"We entered the " + theCurrentStar + " system today. ",
			shipName + " came to the " + theCurrentStar + " system. "
		};
		part1 = part1statemnts [Random.Range (0, part1statemnts.Length)];
		
		//part 2 (reaction to it)
		
		string[] part2statemnts = new string[]{
			" There were " + thePlanets + " planets in orbit around the primary star. ",
			" We lost count of the number of rocks and asteroids that circled the star. ",
			" The system must have been young. Gas and dust is still collapsing into a protostar. The star should ignite soon - any millennia now. ",
			"  ",
			thePlanets + "  planets orbited the star. ",
			theCurrentStar + " comprises " + thePlanets + " worlds. ",
			thePlanets + " worlds. ",
			thePlanets + " planets. ",
			thePlanets + " planets to study before we go. ",		
			theCurrentStar+ " seemed somehow strange. The " + waddstndth+ " planet's axis was completely perpendicular to its orbit. "
		};
		
		part2 = part2statemnts [Random.Range (0, part2statemnts.Length)];
		
		//part 3, (resource extraction)
		
		
		if (theStarType == "waterF") {
			
			part3statemnts = new string[]{
				"We found ice in the polar caps of " + theCurrentStar + " " + whichPlanetWord + ". ",
				"We melted down ice from " + theCurrentStar + " " + whichPlanetWord + " for water. ",
				"We extracted ice from a few comets that were passing through the system. ",
				"We found water in liquid form on the "+waddstndth+" planet."
			};
			part3 = part3statemnts [Random.Range (0, part3statemnts.Length)];
		};
		
		if (theStarType == "foodF") {
			DecideNameToUse();
			
			part3statemnts = new string[]{
				"Planet " + whichPlanetWord + " had resources we could process into food. ",
				crew1 + " collected food matter from the "+waddstndth+" planet.",
				StarScript.StarName+" "+whichPlanetWord+" had minerals that we could use in food processors.",
				crew1 + " found several key elements that could be used to generate rations. "
			};
			
			part3 = part3statemnts [Random.Range (0, part3statemnts.Length)];
		};
		
		if (theStarType == "fuelF") {
			
			part3statemnts = new string[]{
				"Planet " + whichPlanetWord + " had resources we could process into fuel. ",
				"We gathered fuel matter from planet "+whichPlanetWord+". ",
				"We harvested fuel and moved on.",
				"Beyond the fuel matter we extracted from the " +waddstndth +" planet we had no reason to stay.",
				"The crew extracted fuel matter from the pole of planet "+whichPlanet+"."
				
			};
			
			//add supply dependent variables here.
			part3 = part3statemnts [Random.Range (0, part3statemnts.Length)];
			
		};
		
		if (theStarType == "moraleF") {
			
			part3statemnts = new string[]{
				"Planet " + whichPlanetWord + " was roughly Earth mass - the crew landed on the surface and explored for a few days.",
				"It seemed like a place where we could rest for a bit.",
				"I granted some shore leave to the crew that wanted to explore the moon of "+StarScript.StarName+" "+whichPlanetWord+". ",
				"There's no place like home. But something about the stars here made us willing to stop for a day and rest.",
				crew1 +" and " + crew2 + " explored a canyon on "+StarScript.StarName+" "+ whichPlanetWord+ " and brought back a few images for the rest of us to see.",
				"Planet " + whichPlanetWord + " had liquid oceans, and beaches worthy of a shore leave. ",
				"We didn't want to leave "+StarScript.StarName+". With such a dangerous universe out there, and so many lost already, the desire was strong to stay where we were.",
				"It was hard to force ourselves to leave"+StarScript.StarName+". Who knows when we might encounter such a pleasant world again.",
				"One forgets how confining a ship is until one steps outside, in a breathable atmosphere, and sees a star rising over the horizon.",
				"Landing on "+currentStar+" reminded me of how good it was to have an entire atmosphere, rather than just a bulkhead, in between oneself and the stars.",
				"I let the crew enjoy an excursion on "+currentStar+" without me. It's good to let them have space from their captain. Best to let them breathe a while.",
				"With a sun that reminded me of Sol, it was difficult to give the order to move on from "+currentStar+" once the time came. "
			};
			
			if (theStarType == "science") {
				
				part3statemnts = new string[]{
					currentStar +"'s star radiated a special energy source that we captured and stored away for a wormhole jump.",
					"Deep beneath the surface of "+currentStar+" "+whichPlanetWord+" we detected a high-energy element that could be used to create a wormhole.",
					"Donning protective equipment, "+crew1+" and "+crew2+" harvested wormhole matter from an energy fissure on "+currentStar+" "+whichPlanetWord+".",
					"High energy elements crucial to generating a worm hole were extracted from the rings of "+currentStar+" "+whichPlanetWord+".",
					"We gathered wormhole matter from the star - the engineer was pleased.",
					"Trace elements of wormhole matter were detected on the third planet."
				};
				
				part3 = part3statemnts [Random.Range (0, part3statemnts.Length)];
				
			};
			
			storyStringToAdd = part1 + part2 + part3;
			
			AddALine (storyStringToAdd);
			ClearParts ();
		}
	}
	
	void TravelStory3(string theCurrentStar, string theType){
		//needs CurrentStar, # of Planets
		
		DecidePlanet();
		string[] sentences = new string[]{
			"The crew rested in the " + theCurrentStar + " system for a spell.",
			"Plenty of scientific anomalies in the " + theCurrentStar + " system to keep the crew busy.",
			theCurrentStar+". A system like any other.",
			theCurrentStar+" isn't home.",
			theCurrentStar+" is cold and practically empty. ",
			"After gathering resources, there was nothing to keep us in the "+theCurrentStar+" system. We kept moving.",
			"We called the "+waddstndth+" planet of the "+theCurrentStar+" sysystem '"+crewmembers[crew1no].firstName+"' after "+crewmembers[crew1no].rank+" "+crewmembers[crew1no].lastName+" who first detected it on our scans.",
			theCurrentStar+" was bright on our viewscreen.",
			theCurrentStar+" seemed old. About ready to collapse.",
			theCurrentStar+ " was young and vibrant.", 
			theCurrentStar+ " was a giant of a star.",
			theCurrentStar+ " was a brown dwarf, casting a dim light that barely illuminated even the inner most planets of the system.",
			theCurrentStar+ " grew large in the viewscreen.", 
			theCurrentStar+ " was a name the crew picked at random. I think it's a bit odd.",
			theCurrentStar+ " seems an odd name to give a star. But there are a lot of stars out here to name. They can't all be impressive.",
			theCurrentStar+ " made me uneasy. We finished our business as quickly as we could before leaving.",
			theCurrentStar+ " made me think of Sol."
			
		}; 
		
		part2 = sentences[Random.Range(0,sentences.Length)];
		
		//if it's a morale star
		if (theType == "moralef") {
			string[] moraleSen = new string[]{
				theCurrentStar+ " shone brightly; a welcome resting point after a long jump.",
				theCurrentStar+ " gave the crew a small feeling of hope.",
				theCurrentStar+ " seemed inviting. I allowed the crew to explore some of the anomolies of the system."
			}; 
			part3 = moraleSen[Random.Range(0,moraleSen.Length)];
		}
		
		storyStringToAdd = part2 + part3;
		AddALine (storyStringToAdd);
		ClearParts ();
		
	}
	
	void TravelStory4(string theCurrentStar){
		//part 1
		part1 = "The star of the " + theCurrentStar + " system";
		
		//part 2
		string[] part2strings = new string[]{
			" reminded me of Sol, a little.", 
			" makes me think of the Sun back home."
			
		};
		part2 = part2strings [Random.Range (0, part2strings.Length)];
		
		//part 3
		string [] part3strings = new string[]{" "," I’m not sure why. ",
			" It’s almost the same size. ",
			" I don’t know. I think this a lot. Maybe I’m just homesick. ",
			" It's odd. ",
			" Not that our star and " +theCurrentStar+ "are even the same size. ",
			" Except that " +theCurrentStar+ " burns about twice as hot. "
		};
		part3 = part3strings [Random.Range (0, part3strings.Length)];
		
		storyStringToAdd = part1 + part2 + part3;
		ClearParts ();
		AddALine (storyStringToAdd);
	}
	
	void CaptainAndCompanionVisit(int Planets){
		//part 1 (entering the system)
		string[] part1statemnts = new string[]{
			crew1+" and I came to a system called " + Playerscript.currentStar + ". ",
			crew1+" and I came to a system we called " + Playerscript.currentStar + ". ",
			"Our jump took us to the " + Playerscript.currentStar + " system. ",
			"We entered the " + Playerscript.currentStar + " system today. ",
			shipName + " came to the " + Playerscript.currentStar + " system. "
		};
		part1 = part1statemnts [Random.Range (0, part1statemnts.Length)];
		
		//part 2
		string[] part2strings = new string[]{
			" There were " +  Planets + " planets in orbit around the primary star. ",
			" We lost count of the number of rocks and asteroids that circled the star. ",
			" The system must have been young. Gas and dust is still collapsing into a protostar. The star should ignite soon - any millennia now. ",
			"  ",
			Planets + "  planets orbited the star. ",
			Playerscript.currentStar + " comprises " + Planets + " worlds. ",
			Planets + " worlds. ",
			Planets + " planets. ",
			Planets + " planets to study before we go. ",		
			Playerscript.currentStar+ " seemed somehow strange. The " + waddstndth+ " planet's axis was completely perpendicular to its orbit. "
		};
		part2 = part2strings [Random.Range (0, part2strings.Length)];
		
		//part 3
		string [] part3strings = new string[]{
			"Although the bridge was empty aside from we two, we performed the standard procedures before leaving.",
			"We relied on automated systems to perform many of the necessary tasks to gather resources.",
			"Being the last two people onboard, the chain of command had long since been discarded. I shared in the task of gathering supplies.",
			"We didn't say much as we gathered resources.",
			"We went through the motions of gathering resources.",
			"As the last members of the crew, we needed to use the ship's automated systems to gather supplies."
		};
		part3 = part3strings [Random.Range (0, part3strings.Length)];
		
		storyStringToAdd = part1 + part2 + part3;
		ClearParts ();
		AddALine (storyStringToAdd);
	}
	
	void CaptainAloneVisit(){
		//part 1 (entering the system)
		string[] part1statemnts = new string[]{
			"I came to a system called " + Playerscript.currentStar + ". ",
			"When woke up today, the autopilot had carried me to the " + Playerscript.currentStar + " system. ",
			"I entered the " + Playerscript.currentStar + " system today. ",
			shipName + " came to the " + Playerscript.currentStar + " system. ",
			shipName + " drifted into " + Playerscript.currentStar + " system. ",
			"I brought the ship to " + Playerscript.currentStar + ". "
		};
		part1 = part1statemnts [Random.Range (0, part1statemnts.Length)];
		
		//part 2
		string[] part2strings = new string[]{
			"Out of habit, I took scans of the planets. ", 
			"On a whim, I spent several hours on the surface of planet "+Random.Range(0,4) +" to observe the geology. ",
			"I set the autopilot to orbit world "+Random.Range(0,4) +". ",
			"There was nothing to do, really. ",
			"Most activities that came to mind seemed pointless. ",
			"I had nothing important to do. ",
			"There was nothing to do. "	
		};
		part2 = part2strings [Random.Range (0, part2strings.Length)];
		
		//part 3
		string [] part3strings = new string[]{
			"After a time, I readied the caluclations for the next jump, and moved on.",
			Random.Range(4,11)+" hours later, "+shipName+" had left the system.",
			"Within "+Random.Range(4,11) +" hours I had moved on.",
			"After hours of looking out the viewscreen, I roused myself to plan the next jump. ",
			"I readied the computer to prepare the next jump.",
			"I ordered the computer to move on, and went to sleep."
		};
		part3 = part3strings [Random.Range (0, part3strings.Length)];
		
		storyStringToAdd = part1 + part2 + part3;
		ClearParts ();
		AddALine (storyStringToAdd);
		
	}
	
	void ClearParts(){
		part1 = " ";
		part2 = " ";
		part3 = " ";
		
		crewMemberNumber = crewmembers.Count;
		NameToUse = "";
		pronounToUse = "";
		possessiveToUse = "";
		diroToUse = "";
		crewNo =  crewmembers.Count;
	}
	
	void StNdTh(int planetno){
		if (planetno == 1)
			addstndth = "st";
		if (planetno == 2)
			addstndth = "nd";
		if (planetno == 3)
			addstndth = "rd";
		if (planetno >= 5)
			addstndth = "th";
	}
	
	void WordStNdTh(int planetno){
		if (planetno == 1)
			waddstndth = "first";
		if (planetno == 2)
			addstndth = "second";
		if (planetno == 3)
			addstndth = "third";
		if (planetno == 4)
			addstndth = "fourth";
		if (planetno == 5)
			addstndth = "fith";
		if (planetno == 6)
			addstndth = "sixth";
		if (planetno == 7)
			addstndth = "seventh";
		if (planetno == 8)
			addstndth = "eigth";
		if (planetno == 9)
			addstndth = "nineth";
		if (planetno == 10)
			addstndth = "tenth";
		if (planetno == 11)
			addstndth = "eleventh";
		if (planetno == 12)
			addstndth = "twelfth";
		if (planetno == 13)
			addstndth = "thirteenth";
		if (planetno == 14)
			addstndth = "fourteenth";
		if (planetno == 15)
			addstndth = "fifteenth";
		
	}
	
	void UpdatePlayerStats(){
		fuel = Playerscript.fuelF;
		water = Playerscript.waterF;
		food = Playerscript.foodF;
		morale = Playerscript.moraleF;
		famine = Playerscript.famine;
		thirst = Playerscript.thirst;
		//crew = Playerscript.crew; //no
		reachedSol = Playerscript.reachedSol;
		timeNow = Gamemanager.timeNow;
		timePassed = Gamemanager.timePassed;
	}
	
	//EVENTS
	
	public void FamThirstDeath(string cause){
		DecideNameToUse();
		Playerscript.Notification ("-1 crew: " + crew1full);



		//part 1 Declare Death
		if(cause == "famine")
		{
			//part 1 "we lost due to
			string[] starveStories = new string[]{
				"We lost "+ crew1+ " to famine today. ",
				crew1 + " has starved to death. ",
				crew1  + " has starved. "
			};
			
			part1 = starveStories [Random.Range (0, starveStories.Length)];
			
		}
		
		if(cause == "thirst")
		{
			//part 1 "we lost due to
			string[] thirstStories = new string[]{
				"We lost "+ crew1 + " today. We couldn't give " + crew1diro + " the water "+crew1proun+" needed. ",
				crew1  + " died from lack of water. ",
				crew1  + " was lost today. We have no water. "
			};
			
			part1 = thirstStories [Random.Range (0, thirstStories.Length)];
			
		}
		
		if(cause == "famine and thirst")
		{
			//part 1 "we lost due to
			string[] famthirstStories = new string[]{
				"We lost "+ crew1 + " today. We have no food or water. ",
				"Supplies ran out days ago. "+ crew1  + " died. ",
				"After days without food or water "+ crew1  + " died. "
			};
			
			part1 = famthirstStories [Random.Range (0, famthirstStories.Length)];
			
		}

		//part 2, react.
		
		//part 1 "we lost due to
		string[] reaction = new string[]{
			"I let " + crew1diro + " down. ",
			crew-1 + " of us remain. ",
			crew-1 + " of us remain. ",
			crew-1 + " of us remain. ",
			"I'm so sorry. ",
			"I've failed "+ crew1diro+". ",
			"And " + crew1proun + " was a good member of the crew. ",
			"And " + crew1proun + " was an honest soul. ",
			"And " + crew1proun + " was a bully. But it is a loss. ",
			"How many more of us will go? ",
			"Will I lose another? ",
		};
		
		part2 = reaction[Random.Range (0, reaction.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);

		deceasedlist.Add (crew1full);
		RemoveCrew (crew1no);

		if (crew == 1)
		{CaptainAlone();
			Debug.Log("whaaaaaaaaaaaaa");
		}
	}
	
	void ReactToResources(){
		
		
		int which = Random.Range (1, 5);
		
		if (which == 1) 
			FoodReaction ();
		
		if (which == 2)
			WaterReaction ();
		
		if (which == 3)
			MoraleReaction ();
		
		if (which == 4)
			FuelReaction ();
		
		//}
	}
	
	public void FuelReaction(){
		
		
		if (fuel > 80f) {
			string[] fuelS = new string[]{
				"Our fuel levels are high.",
				"We have enough fuel to power the core for a good many star jumps. ",
				"With our core running at this efficiency, we'll be able to make a decent number of jumps before refueling. ",
				"The Star Drive is well supplied. ",
				"Fuel levels are good. ",
				"I'm pleased that fuel levels are as high as they are. ",
				"Thank goodness. We have enough fuelmatter to keep the Star Drive running for a time. "
				
			};	
			part1 = fuelS[Random.Range(0,fuelS.Length)];
		}
		
		if (fuel > 50f && fuel <80f) {
			string[] fuelS = new string[]{
				"We have a moderate level of fuel. ",
				"Our current fuel wont last forever, but for now I'm not worried. ",
				"Fuel levels aren't bad. ",
				"I've told the team in stellar cartography to notify me if a fuel world is detected. ",
				"Engineering believes that our fuel reserves will last us quite a few jumps. My first reaction is always to be cautious. "
				
			};	
			part1 = fuelS[Random.Range(0,fuelS.Length)];
		}
		
		if (fuel > 20f && fuel <50f) {
			string[] fuelS = new string[]{
				"Fuel levels are starting to trouble me. ",
				"I'm beginning to get worried about our jump matter levels. ",
				"We're going to need to find a fuel world pretty soon. ",
				"I'm on the lookout for a fuel world. ",
				"We need fuel. Very soon.",
				"Fuel is beginning to become a concern. ",
				"I'm starting to get nervous about our fuel levels. "
				
			};	
			part1 = fuelS[Random.Range(0,fuelS.Length)];
		}
		
		if (fuel > 1f && fuel <20f) {
			string[] fuelS = new string[]{
				"Fuel levels are dangerously low. ",
				"Stellar cartography has been told to notify me immediately if a fuel world is detected. ",
				"Without fuel, our star jumps will slow to a crawl. ",
				"I'm very concerned about fuel. ",
				"We need to find fuel. Now.",
				"It is imperative that we find fuel as soon as possible. "
				
			};	
			part1 = fuelS[Random.Range(0,fuelS.Length+1)];
		}
		
		if (fuel < 1f) {
			string[] fuelS = new string[]{
				"We're out of fuel. Star jumps are using emergency power - very inefficient.",
				"We've lost all jump matter to fuel our drive. ",
				"Without fuel to power our Star Drive, it's going to take us even longer to reach supply worlds for food and water. ",
				"We're out of fuel. ",
				"We've got nothing to run the Star Drive on. ",
				"We need to find a fuel world, now. "
			};	
			part1 = fuelS[Random.Range(0,fuelS.Length)];
		}
		
		
		
		AddALine (part1);
		
	}
	
	
	void MoraleReaction(){
		DecideNameToUse ();
		if (morale > 80f) {
			string[] moraleS = new string[]{
				"The stars are beautiful. I could get used to this.",
				"The crew organized a game of football in the main hangar bay. It was a hard day’s labor moving the food and supply crates to the side to make stands, but it was worth it.",
				"I heard singing in the mess today."
				
			};	
			part1 = moraleS[Random.Range(0,moraleS.Length)];
		}
		
		if (morale < 5f){
			bool alt = false;
			if(Random.Range(1,5) == 1){
				alt = true;
				DepartureTale();
			}
			if(Random.Range(1,5) == 2){
				alt = true;
				Suicide();
			}
			if(alt == false){
				string[] moraleS = new string[]{
					"The halls are silent. The crew hangs their heads. ",
					"I think we will all die out here. ",
					crew1  + " slumps at "+crew1pos+" post on the bridge. ",
					"I'm not sure what will happen. ",
					"We're lost. "
				};
				part1 = moraleS[Random.Range(0,moraleS.Length)];
			}
			
			
			AddALine (part1);
		}
	}
	
	void FoodReaction(){
		DecideNameToUse ();
		if(food >80f){
			string[] resolution = new string[]{
				"A food fight broke out in the mess hall today. The instigators " +crew1 +" and "+crew2+" received extra duty.",
				"Extra food rations have made the crew feel more free to exercise. The cargo bay is in use as a football court. ",
				crew1  + " insists that " + crew1proun+ "  can find a way to make the synthetic food taste better. I've granted " + crew1diro + " a few extra rations to experiment with.",
				"Food supplies are excellent.",
				"I'm pleased with how much food we have at the moment.",
				"With food supplies at these levels I think we'll be in good shape for a while."
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		//moderate need
		if(food >50f && food <80f){
			string[] resolution = new string[]{
				"Food supplies are good. ",
				"I'm satisfied with where food supplies are at. But we'll need to keep an eye out for supply stars. ",
				"'Moderation in everything' is the motto where our provisions are concerned. ",
				"The crew is in decent shape. We might want more provisions at some point, but for now I'm not too worried."
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		//moderate need
		if(food >20f && food <50f){
			string[] resolution = new string[]{
				"Food supplies aren't terrible. But they aren't good. We need to find more. ",
				"We should find more food soon. ",
				"Stellar cartography has been instructed to keep an eye open for stars with food resources nearby. ",
				"There is a debate between the crew over whether it is better to eat all one's ration now or save it for a moment of extreme hunger."
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		//desparate need
		if (food <20f){
			string[] resolution = new string[]{
				"We've got to find more food soon. ",
				"We need to find more food. ",
				"Stellar cartography has been instructed to make finding a supply star their highest priority. "
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		AddALine (part1);
		ClearParts ();
	}
	
	
	void WaterReaction(){
		DecideNameToUse ();
		//Excellent supply
		if(water >80f){
			string[] resolution = new string[]{
				"I'm pleased with how much water we've been able to harvest and store. We should be well off for some time now. ",
				"The crew raised a toast this evening with our most precious resource - a clear glass of water. ",
				crew1  + " somehow has created a squirt gun out of materials from the tech lab. It has been confiscated, but not before a few of the officers tried it out. ",
				"We've collected plenty of water in recent days. It makes me feel more at ease.",
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		//decent supply
		if(water >50f && water<80f){
			string[] resolution = new string[]{
				"Water supplies seem good for now. ",
				"We have enough spare water for crew members to keep plants in their quarters. ",
				crew1  + " has requested special use of water for a bath on "+ crew1pos+ " birthday. I've allowed it. "
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		//moderate need 
		if(water >20f && water<50f){
			string[] resolution = new string[]{
				"I've ordered more moderate consumption of water. .75 ration until we bolster our supplies. ",
				"Water supplies are low. Not terrible yet. ",
				"Water supplies are not as high as I'd like them to be. ",
				"I've noticed members of the crew storing their water ration for later. ",
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		//desperate need
		if (water <20f) {
			string[] resolution = new string[]{
				"We've got to find more water soon. ",
				"Finding water is our heighest priority. ",
				"We are scanning for water signatures at all times. "
			};
			
			part1 = resolution[Random.Range (0, resolution.Length)];
		}
		
		
		
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	
	void DepartureTale(){
		DecideNameToUse ();
		string theCurrentStar = Playerscript.lastStarVisited;
		
		//somebody is leaving
		string[] departureS = new string[]{
			crew1 +" announced that "+crew1proun+" was going to disembark the ship to stay on "+theCurrentStar+" "+whichPlanetWord+".",
			crew1 +" convinced some of the crew that it was better to leave the ship and live on a new world, even a barren world.",
			crew1 +" has left the ship. ",
			crew1 +" appeared on the bridge with a survival pack, and "+pronounToUse+"'s decided to leave and stay on "+theCurrentStar+" "+whichPlanetWord+"."
		};
		part1 = departureS [Random.Range (0, departureS.Length+1)];
		
		//who is leaving
		string[] departureS2 = new string[]{
			"And "+pronounToUse+" is taking two others with "+crew1diro+", "+crew2+" and "+crew3+". ",
			"We managed to talk most of "+crew1diro+" sympathizers out of it, but "+crew2+" and "+crew3+" are leaving too.",
			crew1proun+" and two others have departed the ship.",
			crew2+" is joining "+crew1diro+" along with "+crew3+"."
			
		};
		part2 = departureS2 [Random.Range(0,departureS2.Length+1)];
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
		//reaction
		string[] departureS3 = new string[]{
			"It's not as though they can start a colony. There are two few. They'll simply live on the "+waddstndth+" planet of the "+theCurrentStar+" system until they die. And the last one will die alone",
			"I told them that they would be giving up their chance for home. They would never return to Earth. "+crew1+" replied, 'neither will you.",
			"I doubt they'll survive beyond the oxygen supply they take with them. Maybe two weeks.",
			"How miserable they must have been to prefer exile on a barren, hospitable world over life aboard "+shipName+"."
		};
		part3 = departureS3 [Random.Range(0,departureS3.Length+1)];
		AddALine (part3);
		
		deceasedlist.Add (crew1full);
		deceasedlist.Add (crew2full);
		deceasedlist.Add (crew3full);
		
		RemoveCrew (crew1no);
		RemoveCrew (crew2no);
		RemoveCrew (crew3no);
		
		ClearParts ();
	}
	
	void Suicide(){
		
	}
	
	void RemoveCrew(int crewNum){
		crewmembers.Remove (crewmembers[crewNum]);
		crew = Playerscript.crew = crewmembers.Count;

	
		for (int i = 0; i <= crewmembers.Count; i++) {
			
		}
		
		
		//EJECT coffin
		GameObject.Find ("eject").GetComponent<ParticleSystem>().Play ();
		GameObject.Find ("eject").GetComponent<AudioSource>().Play ();
		
		Transform eject = GameObject.Find ("eject").transform;
		Vector3 spawnplace = GameObject.Find ("eject").transform.position;
		Quaternion spawnquat = GameObject.Find ("eject").transform.rotation;
		
		GameObject coffin;
		coffin = GameObject.Instantiate(capsule, spawnplace, spawnquat) as GameObject;

		
		
		
		
		
	}
	
	void RandomVignetteStart(){
		DecideNameToUse ();
		
		string[] VignetteA = new string[]{
			"The crew is adjusting to the reality that we will not see home again for years.",
			"The Far Jump was supposed have taken us to a neighboring system, only a few hundred light-years away. Now here we are, "+Playerscript.distanceToEarth+" lightyears from home.",
			"We're going to go through our rations from Earth pretty quickly. We need to replenish our supplies with resources found out here in the void.",
			"I wonder. How long will it take before people back home believe us to have been lost? Less than a year, probably.",
			"In strange circumstances, discipline is paramount. Everyone must perform their duties efficiently and effectively if we're going to be able to survive out here.",
			"We rely on the structure of routine duty to help us deal with the strange new reality we find ourselves facing.",
			"We feel as though we are on a life raft on the open ocean. Except we will have to devise our own rescue, out here.",
			"I think the stars are somehow brighter in this region of space.",
			"I earned my commission as captain only a few months ago. This is a hell of a first command.",
			"The Far Jump was supposed to have taken us to a remote testing vessl - one that had traveled the better part of fifty years using conventional drives into space. We appear to have overshot slightly.", //homage to homeworld
			"We're further from Earth than anyone has ever been before. Who knows what we might discover.",
			"I've scoured over the star charts, seeking - without success - a map of this region of space. Apart from naming the odd star, humanity simply hasn't observed this distant part of the galaxy.",
			shipName+" is a vessel fresh out of spacedock - an experimental ship with an experimental drive designed to endure long journies in space. We'll put the design to the test.",
			"Fortunately "+shipName+" is well stocked with a library of "+books.Length+" books. We should have plenty to read for the journey home."
		};
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
		
		
	}
	
	void RandomVignetteA(){
		DecideNameToUse ();
		
		string[] VignetteA = new string[]{
			"Sometimes I think about how strange it is to be out here. We're so far from home and yet the daily routine of life - ship maintenance, resource management, crew matters, continue on. I suppose they have to.",
			"I passed "+crew1+" and "+crew2+" in the hall. I wonder if anything is going on between those two.",
			"It's hard to believe we're really out here, sometimes. ",
			"I keep expecting to encounter aliens, or at least some sign of life other than our own out here in the space, but there is nothing. The stars are silent.",
			"After his ship sank in the Antartic, Shackleton kept his crew of twenty-eight alive for a year with little more than seal meat and stubbornness. I think about him sometimes. ",
			crew1+" always hums to "+crew1pos+"self in the hallways.",
			"The ship must be kept running smoothly. Without a viable port in which to obtain repairs every year or so, we must ensure all maintenance is done by us, and done well.",
			"Romance on "+shipName+" is everyone's business, whether we like it or not. I think "+crew1+" and "+crew2+" in particular find it hard.",
			"There exactly "+booksno+" books onboard. The crew meets weekly to exchange and swap.",
			"We've never encountered life out here, or any signs of it. Star Trek made it seem like we'd be constantly encountering life forms - instead, we do a lot of thinking.",
			"Each day I wake up at 0500 and jog the length of the ship four times. Sometimes I encounter "+crew1+", who is also an early riser. We never speak - time enough for that after the morning's 'coffee'.",
			"I spend much of my time thinking of how to keep the crew occupied. Sitting and thinking for too long wear on the mind. Today I'll try to organize "+activities[Random.Range(0,activities.Length+1)]+".",
			crew1+" and "+crew2+" have clearly been arguing again. I wonder if I'll need to revise the duty roster.",
			"Someone has painted the bulkhead on deck "+Random.Range(2,13)+" with the image of "+vistas[Random.Range(0, vistas.Length)]+".",
			"I need something new to read. This copy of "+books[Random.Range(0,books.Length)]+" is getting pretty worn.",
			"It's been "+(int) timePassed+" days since my last, real, hot, shower.",
			"To pass the time, "+crew1+" has taken up cataloguing the stars. This far from home, only a few have been named.",
			"As captain I'm only witness to some of the events happening onboard the ship. I live amongst the crew but I'm isolated from their thoughts, their jokes, their relationships.",
			"The days pass slowly. The stars stream by.",
			"We've visited "+Playerscript.jumps+" worlds so far on our journey. Our road continues before us.",
			"Time passes slowly. We do our duty, and we count the hours. ",
			"I wonder if "+ DudeNames[Random.Range(0,DudeNames.Length)]+" has read the book I loaned him.",
			"I wonder what "+ LadyNames[Random.Range(0,LadyNames.Length)]+" is doing back on Earth.",
			"I have always wanted to learn the cello. We printed a plastic chasis and extruded a crude set of strings - a bow was made by experimenting with various fabrics. And late at night, I strum away.",
			"I sometimes wonder if my pet dog is still alive. Just before "+shipName+" disembarked, I left him with "+whoHasTheDog+" and said I'd be back to pick him up in two weeks. I seem to have missed that deadline.",
			"Members of the crew, myself included, would kill to expand our music library. The "+bands[Random.Range(0,bands.Length)]+" album has gotten old.",
			"Despite the titanic error in her Far Jump Drive, the crew have come to love "+shipName+". She is a home to them, a sanctuary in the wilderness."
		};
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	
	void NearlyThereVignette(){
		DecideNameToUse ();
		
		string[] VignetteA = new string[]{
			"Sol grows bright before us. "+crew1+" informs me that Sol is just "+Playerscript.distanceToEarth+" lightyears away.",
			"Deep space sensors crackle. The first whispers of radio signals broadcast from Earth can be heard.",
			"The crew begin to ponder the reality that they might make it home. After "+timePassed+" days in space, we might actually see our friends and family again.",
			"Sol grows closer.",
			""+shipName+" inches her way closer to home.",
			""+shipName+" continues to thread her way from star system to star system. All the while a sense of anticipation grows. We are "+Playerscript.distanceToEarth+" lightyears away from home.",
			"Passing through the corridors, occasionally a crewmember will stop and look out the porthole towards the star growing brighter in the distance. Earth is getting closer.",
			deceasedlist.Count+" have left us. Our numbers have decreased. But those of us who remain are closer for all that has happened.",
			"My mind wanders to thoughts of home. After the Star Drive failed, what did people believe happened to us? Do they think we were killed in the accident? Do they know were are lost in space?",
			"I think of home. Is 'home' actually there? We've been missing for "+timePassed+" days without a trace, or a word. Likely that they've held their funerals, 'burried us', and moved on. Are they in for a surprise.",
			"Sol is "+Playerscript.distanceToEarth+" lightyears away.",
			"We've agreed - someone needs to start generating new material. "+crew1+" has a measure of talent at writing and has been 'volunteered' to write a new set of short stories for the crew to read. Anything would be better than this decayed copy of "+books[Random.Range(0,books.Length)]+"."
		};
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	
	void EndingVignette(){
		DecideNameToUse ();
		
		string[] VignetteA = new string[]{
			"We've made it. We've made it.",
			"So much sacrificed to make it here. So many lives. But we're nearly home.",
			crew1+", "+crew2+" and I stare out the viewscreen. Sol shines brightly.",
			"Radio signals from Earth are now audible.",
			"I've tossed aside this completely dog-eared copy of "+books[Random.Range(0,books.Length)]+" and stare at the viewscreen.",
			"Earth - home. It's been so long since we've seen it.",
			"A small impromptu celebration is thrown the crew. A long-forgotten bottle of champagne was opened, each crew member taking a tiny sip. We have seen hard times since we first set out, but we have grown closer.",
			crew1+" and I stood in astrometrics and traced our long over the past years and months. Silently, we shook hands.",
			"Today I made what I expect will be my last monthly inspection of "+shipName+". The halls are quieter than they once were - crewmen carrying out their duties with a subdued sense of hope.",
			"This log seems almost pointless, now, with the end so close in sight.",
			"The crew barely sleeps on this final leg of the voyage.",
			"We have lost "+deceasedlist.Count+" on this voyage. Men and women who should have seen this day.",
			"Home sweet home, nearly.",
			"I think back on the people we have lost. "+deceasedlist[Random.Range(0,deceasedlist.Count)]+" should have been here.",
			"In light of recent events, nobody had much interest in participating in the "+activities[Random.Range(0, activities.Length)]+" that somehow has always been on the ship calendar. It has been postponed.",
			"Incredible. Who would have believed that we'd have made it."
		};
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	
	void SetCampaign(){
		campaign = new string[]{
			//"As we passed through the wormhole the ship suffered extreme damage. It took the crew two days to return function to the engines and sensor equipment.",
			//"When the bridge viewscreen flickered to life, we looked out and didn't recognize the stars. We don't know where we are.",
			"Welcome to The Captain's Log alpha. In this game you play as a starship captain whose crew is lost in space and must find a way home.",
			"To maintain your crew you must constantly be on the lookout for resources.",
			"Pink stars have worlds around them with fuel for the Star Drive. Blue worlds have water, Green worlds have food, and red worlds are happy places to rest and boost morale.",
			"Most importantantly, light blue stars are 'observation stars' where the crew will set up telescopes to seek out Earth.", 
			"Over time you will locate pulsars - flashing stars, visible from Earth, that provide clues to the location of home.",
			"Seek out where the visibility fields of the pulsars intersect. Once you have found 4, you'll know exactly where Earth is.",
			"Research technology to improve your ship.",
			"It's lonely out there in the void - do your best to keep them all safe. Good luck!",
			"(Music, by the way, is Chantiers Navals 412 by LJ Kruzer)"
		};
		
	}
	
	public void Mission(string starType){
		
		DecideNameToUse ();
		
		int odds = Random.Range (1, 3);
		
		if (odds ==1 ) {//success
			if (starType == "waterF") {
				WaterMissionSuccess ();
				Playerscript.StarInteract (30f,"yes"); //player gets extra resources
			}
			
			if (starType == "foodF") {
				FoodMissionSuccess ();
				Playerscript.StarInteract (30f,"yes");
			}
			
			if (starType == "fuelF") {
				FuelMissionSuccess ();
				Playerscript.StarInteract (30f,"yes");
			}
			
			/*
						if (starType == "moraleF") {
							MoraleMissionSuccess ();
							//Playerscript.missionChoice = Playerscript.missionChoices [5];
							Playerscript.StarInteract (30f,"yes");
						}*/
		}
		
		if (odds != 1) {//failure
			
			if (starType == "waterF") {
				WaterMissionFailure ();
			}
			
			if (starType == "foodF") {		
				FoodMissionFailure ();
			}
			
			if (starType == "fuelF") {
				FuelMissionFailure ();
			}
			
			if(crewmembers.Count >= 3 && deceasedlist.Count <= 77)
			{
				deceasedlist.Add(crew1full);
				deceasedlist.Add(crew2full);
				RemoveCrew (crew1no);
				RemoveCrew (crew2no);
				Playerscript.Notification("-2 crew: "+crew1+" and "+crew2);
			}
			
			if(crewmembers.Count == 2)
			{
				deceasedlist.Add(crew1full);
				RemoveCrew (crew1no);
				Playerscript.Notification("-1 crew: "+crew1);
			}
			
			
		}
	}
	
	void WaterMissionSuccess(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			""
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			crew1+" and "+crew2+" ran a mission to extract the resource and bring extra supplies home to "+shipName+". ",
			"After a hours of effort we were able harvest the supplies we needed. The extra water will be invaluable. ",
			"A dangerous away mission succeeded in gathering the water we needed. "
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
		
	}
	
	void FoodMissionSuccess(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			""
			
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			"At great risk "+crew1+" and "+crew2+" managed to bring back a supply of food for the ship. Our stores are much improved. ",
			crew1+" and "+crew2+" were able to return with much needed foodmatter supplies. Rationing has been relaxed. ",
			"We gathered enough from the mission to ease the concerns of my bridge crew about our depeleted food supplies. "
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
	}
	
	void MoraleMissionSuccess(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			
			
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
	}
	
	void FuelMissionSuccess(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			"At great risk "+crew1+" and "+crew2+" managed to bring home fuel for the Star Drive. ",
			crew1+" and "+crew2+" were able to return with much needed fuel supplies. The engineers breath a sigh of relief. ",
			"We gathered enough from the mission to ease the concerns of my bridge crew about our fuel, for the present. ",
			"The reactor can run at full power for some time now, thanks to the resources the team brought home. "
			
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			""
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
	}
	
	void WaterMissionFailure(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			"We detected signatures of ice deep beneath the surface of the planet. ",
			"A glacier on the northern continent of the planet posed an opportunity for us to harvest more water. ",
			"We found ice on the planet's moon. "
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			crew1+" and "+crew2+" attempted to extract the resource we needed and bring it home. They were killed when the ice around them shifted. ",
			"We lost two crew members in the attempt to bring the supplies home. ",
			"We lost two trying to bring the ice home. ",
			"The ice collapsed on the extraction team. We lost "+crew1+". "+crew2+" tried to save "+crew2diro+" and was also killed. "
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
	}
	
	void FoodMissionFailure(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			"Scans of the "+waddstndth+" planet revealed food matter we could harvest for additional supplies. I dispatched two crewmemebers to the surface to take a hand at extracting it. ",
			"There was a vein of foodmatter on deep in a mountain on "+currentStar+" "+whichPlanetWord+". ",
			"We needed the foodmatter on "+currentStar+" "+whichPlanetWord+". "
			
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			crew1+" and "+crew2+" were killed when the ground shifted and buried them alive. ",
			"Two crew members, "+crew1+" and "+crew2+", were killed attempting to recover the resources. ",
			"We lost "+crew1+" and "+crew2+" when the transportation equipment malfunctioned. "			
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		//Part2
		string[] story3= new string[]{
			"Their sacrifice to feed us wont be forgotten. ",
			"Their loss leaves their posts vacant, for the moment. And empty seats at the mess table. ",
			"Their names have been entered into the registry of the fallen. ",
			"I must speak at their funeral service tomorrow. "
		}; 
		part3 = story3[Random.Range(0,story3.Length)];
		
		storyStringToAdd = part1 + part2 + part3;
		AddALine (storyStringToAdd);
		
	}
	
	void MoraleMissionFailure(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			
			
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
	}
	
	void FuelMissionFailure(){
		DecideNameToUse ();
		//Part1
		string[] story1 = new string[]{
			crew3 +" stood in my office, stiffly recounting the failed fuel mission on "+currentStar+" "+whichPlanetWord+". ",
			crew3 +" and I sat and reviewed the failed fuel mission. ",
			"We had high hopes for the mission on "+currentStar+" "+whichPlanetWord+", which would have brought in much needed fuel supplies. Something went wrong, however. ",
			"Disaster stuck the fuel mission team today. "
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		string[] story2= new string[]{
			"Somehow the resource collector tanks sprung a leak on the last run back to "+shipName+". There was a fire. ",
			"Weather conditions buffeted the collector team. The collector crashed en route to the extraction site. ",
			"An equipment malfunction, a torn suit. "+crew1+" suffocated and "+crew2+" died trying to save "+crew2diro+". ",
			"Strong winds lifted up the team and carried them miles away from the landing site. By the time we found them, it was too late. "
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		storyStringToAdd = part1 + part2;
		AddALine (storyStringToAdd);
		
	}
	
	public void StartResearchStory(string currentResearch){
		
		DecideNameToUse();
		DecidePlanet();
		
		if (currentResearch == "jump efficiency")		
		{
			string[] reaction= new string[]{
				"The engineers have completed research on a new component to the Star Drive. Our jumps are 20% more fuel efficient.",
				"We've implemented a modification to the Star Drive. Jumps are 20% more fuel efficient.",
				"With the new Star Drive operating at 20% greater efficiency, we can travel further without needing to find a fuel world.",
				""+crew1full+" is to be commended. "+crew1proun+" had a breakthrough last night which allowed us to modify the Star Drive to run more efficiently.",
				"By enfusing the Star Drive with a rare element we extracted from "+Playerscript.lastStarVisited+" "+addstndth+", we increased the efficiency of the jump reaction by 20%.",
				"The engineering team worked all night to develop a modification to the Star Drive - improving performance by 20%."
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
		if (currentResearch == 	"jump range")
		{
			string[] reaction= new string[]{
				"At 23:40 last night "+crew1+" finished a modification to our sensor array. We can jump 20% further than we could before.",
				"Upgrades complete. The Star Drive jump range has increased by 20%.",
				crew1 + " just completed an important upgrade to the Star Drive. We can jump further than ever, now.",
				"At the helm, "+crew1+" exults over the modified Star Drive. An enhanced range means more opportunities to find resources."
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
		
		if (currentResearch == "water efficiency")
		{
			string[] reaction= new string[]{
				"Various inefficiencies in our water processing systems have been eliminated. Our water reserves will last a bit longer, now.",
				crew1+" brought a glass of water from the improved purifier to the morning briefing. As I held the cup in my hands, I thought about how inventions like these will keep the crew alive.",
				"Every drop of water is precious. We've overhauled our systems to reduce waste - efficiency here could save lives.",
				"A newly enhanced water purification system came online today. As a reward for "+crew1proun+" work designing the system, "+crew1+" was authorized to take a shower.",
				"Efficiency is king. Our water purification system has been enhanced to process water even more effectively."
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
		if (currentResearch == "water extraction")
		{
			string[] reaction= new string[]{
				"Water extraction teams have modified the equipment on our resourcers. We pull more water from each glacier, comet, and lake we encounter.",
				"Today I inspected the engineering team's modifications to the resource craft. We should pull more water with each extraction effort.",
				crew1+" tells me that water extraction will be 20% more efficient now.",
				"The crew is excited to test our upgraded water extraction equipment.",
				"Engineering reports that the enhancements made to the water processing equipment are in place. More water means a safer, healthier crew."
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
		if (currentResearch == "foodmatter efficiency")
		{
			string[] reaction= new string[]{
				"Working with the mess hall, engineers have increased the efficiency of our ration production equipment. We can make resources last 20% longer.",
				crew1+" brought me one of the new rations to sample. It was thicker and slightly chewier than the standard. The engineers tell me that with the new formula our rations can last 20% longer than normal.",
				"The new ration formula should last us longer than before.",
				"Our food has never been delicious. But after this new research it should last us longer.",
				"The food units have been overhauled. We can now generate more food with less impact on our supplies."
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
		if (currentResearch == "foodmatter extraction")
		{
			string[] reaction= new string[]{
				"Resource collectors should now be more effective in gathering the raw elements needed to run our food generation units.",
				"Each resourcing trip should now bring in more of the materials we need to feed the crew.",
				"Engineering reports a new process to collect raw materials for food. We can gather 20% more per landing.",
				"Food is always a concern - but "+crew1full+"'s new upgrade to our food collection units should ease our worries."
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
		if (currentResearch == "fuel extraction"){
			string[] reaction= new string[]{
				"Engineers have successfully overhauled our fuel extraction systems. We should get 20% more from every supply run.",
				""+crew1full+" has developed a process to dramatically enhance our fuel extraction systems. I've entered "+crew1pos+"name into the list of commendations.",
				"Fuel extraction systems have been overhauled - with every fuel world we should bring home even more raw materials to power the Star Drive.",
				"Research complete. Our Star Drive fuel extraction systems have been enhanced by 20%.",
			}; 
			
			storyStringToAdd = reaction[Random.Range(0,reaction.Length)];
			AddALine (storyStringToAdd);
		}
	}
	
	public void ResearchComplete(string thingResearched)
	{
		researching = false;
		StartResearchStory(thingResearched);
	}
	
	public void StellarStudyComplete(){
		Debug.Log ("Unlock the next step of the player finding their way home!");
	}
	
	void MaraStory(){
		AddALine ("We arrived in the Mara system. We found a world with literally billions of cats. Cats. Snuggling, mewing. The crew demands a two week shore leave.");
		AddALine ("I must grant it or face mutiny. But at least this gives me the time to give Colonel Fitzkitten the cuddles he deserves.");
	}
	
	public void CaptainAlone(){
		
		//Part1
		string[] story1 = new string[]{
			"I am alone now. "
		}; 
		part1 = story1[Random.Range(0,story1.Length)];
		
		//Part2
		int rand = Random.Range (1,deceasedlist.Count-1);
		string Name = deceasedlist [rand];
		string Name2 = deceasedlist [rand+1];
		
		string[] story2= new string[]{
			"The halls are empty. Every station is vacant. ",
			"I wander the halls thinking of my failure. Here, "+Name+" once worked, cracking jokes. "+Name2+" wrote poetry in a corner. ",
			"I'd give anything to hear a human voice that didn't come from my dreams. ",
			"I have to carry on as best I can. "
				
		}; 
		part2 = story2[Random.Range(0,story2.Length)];
		
		string[] story3= new string[]{
			"Emergency rations in the Captain's quarters will be enough to see me home. ",
			"With only one mouth to feed, emergency supplies should be enough to sustain me. ",
			"I sometimes think the ship is haunted by the people I've failed. "	
		}; 
		part3 = story3[Random.Range(0,story3.Length)];
		
		storyStringToAdd = part1 + part2 + part3;
		AddALine (storyStringToAdd);
		
	}
	
	void ReactToCrewNumbers(){
		if(crewmembers.Count == 77)
			FullCrew();
		if(crewmembers.Count >= 70 && crewmembers.Count <76)
			NearlyFullCrew();
		if(crewmembers.Count < 70 && crewmembers.Count >= 50)
			LostSomeCrew();
		if(crewmembers.Count < 50 && crewmembers.Count > 20)
			LostManyCrew();
		if(crewmembers.Count < 20 && crewmembers.Count > 7)
			LostMostCrew();
		if(crewmembers.Count <= 7 && crewmembers.Count > 1)
			LastCrewMembers();
		if(crewmembers.Count == 1)
			SolitaryCaptainEntry();
	}
	
	void FullCrew(){
		DecideNameToUse ();
		string[] VignetteA = new string[]{};
		if(morale>30){
			VignetteA = new string[]{
				"Despite everything, we've managed to keep everyone alive. The each member of the crew draws on the other for support.",
				"We've managed to keep everyone alive out here. So far.",
				"The journey has had its ups and downs - but nobody has been lost so far. Of that, we are all proud.",
				"Another day, another trial. But everyone is alive and well.",
				"It was an exhausting day aboard "+shipName+" - but not a single bunk lies empty.",
				"We're all here.",
				"The mission on "+Playerscript.lastStarVisited+" was a close one, but everyone is still here to tell the tale."
			};
		}
		
		if(morale<=30){
			string dirooooo = crew1diro.ToUpper();
			string possssss = crew1pos.ToUpper ();
			string pronnnnn = crew1proun.ToUpper ();
			VignetteA = new string[]{
				"We're all here, for now. But things are starting to look a bit grim. I have urged the stellar cartography team to find new ways to locate resources.",
				"As supplies dwindle, the tension amongst the crew grows. So far we've managed managed to scrape by on our reserves and some good luck. But I may be forced to order some dangerous resourcing missions, if things don't improve.",
				"I've ordered a review of our ration policies. If we aren't able to find more food and water out here, it's hard to see how we're all going to stay alive.",
				"The ship's medical center reports cases of crewmembers coming in with instances of severe anxiety. Although we have yet to lose anyone, our luck may not hold forever.",
				"Day by day, we struggle to keep going. The voyage has yet to claim any lives. We all struggle to ensure it stays that way.",
				"Today I encountered by first officer turning down "+crew1+" for wasting food. "+dirooooo+" looked completely crestfallen, but if we are to continue to keep everyone, discipline must be tight.",
				"I made the rounds today and visited "+crew1+" in "+crew1pos+" quarters. "+pronnnnn+" is struggling with the new reality we all face. 'Nobody has died yet,' I told "+crew1diro+". 'And I'm going to make sure it stays that way.'",
			};
		}
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	
	void NearlyFullCrew(){
		DecideNameToUse ();
		string[] VignetteA = new string[]{};
		string dead = deceasedlist.Count.ToString();
		string dirooooo = crew1diro.ToUpper();
		string possssss = crew1pos.ToUpper ();
		string pronnnnn = crew1proun.ToUpper ();
		string deadCaps = dead.ToUpper(); 
		if(morale>40){
			VignetteA = new string[]{
				"We've lost a few good hands - "+deceasedlist[0]+", "+deceasedlist[Random.Range(1,deceasedlist.Count)]+". We all hope that we have learned a few lessons from their loss, and that theirs will be the last coffins we need to prepare.",
				deceasedlist[0]+" was the first to have died under my care. For me, the lesson has been driven home: adapt, or lose more of the people that have been entrusted to my care.",
				"We've lost "+dead+" in the journey so far. But we've optimized our systems, exercised rigid structures. We will not lose another soul if there's anything I can do about it.",
				"We hope we've put the time of greatest trial behind us now. A few souls have been lost on this journey, but they were the last... at least, this is what we tell ourselves.",
				"Life continues aboard "+shipName+". A few empty bunks, a few open chairs in the mess hall remind us of the danger if we fail to keep our stores stocked.",
				"When we first came to this region of space, we had to adjust to a new standard of life. It took us time to get used to the new systems, but we have resolved not to add a single name to the roster of the fallen.",
				crew1+" has created a shrine to the fallen in the aft cargo bay. The names of "+dead+" crewmembers have been carved into the bulkhead.",
				"We are all resolved not to add a single name to the shrine to the fallen.",
				"Every week we observe a moment of silence for those we have lost."
			};
		}
		
		if(morale<=40){
			
			VignetteA = new string[]{
				"The list of names carved into the bulkhead as a shrine to the fallen has reached "+dead+".",
				"We have lost "+dead+" so far. "+deceasedlist[0]+", "+deceasedlist[Random.Range(1,deceasedlist.Count)]+". We struggle to ensure that we do not lose more.",
				"Today I sat with "+crew1+", who had been particularly close to "+deceasedlist[0]+". The two had known each other since they were in "+timePeriod[Random.Range(0,timePeriod.Length)]+". "+pronnnnn+" is struggling with the loss. We all are.",
				"A heavy weight in on us. Each week we hold a moment of silence for a small but growing list of fallen comrades.",
				"We struggle to find the resources we need to keep the crew alive. We have lost "+dead+". I fear we will lose more.",
				"Hunger, thirst, and dangerous missions have carried off "+dead+" brave members of my crew.",
				deadCaps+" have died under my command of this vessel. I struggle with the responsibility - what decisions should I have made differently?",
				"My decisions have gotten us this far. But they have also resulted in the loss of "+deceasedlist[0]+", "+deceasedlist[Random.Range(1,deceasedlist.Count)]+", and, I fear, many more to come."
			};
		}
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	void LostSomeCrew(){
		DecideNameToUse ();
		string[] VignetteA = new string[]{};
		string dead = deceasedlist.Count.ToString();
		string dirooooo = crew1diro.ToUpper();
		string possssss = crew1pos.ToUpper ();
		string pronnnnn = crew1proun.ToUpper ();
		string deadCaps = dead.ToUpper(); 
		
		VignetteA = new string[]{
			"When we first began to lose crewmembers, I worried that I might lose as many as a dozen before the journey was over. So far, I have lost "+dead+", and the number continues to grow.",
			"Hunger, thirst, and dangerous missions have carried off "+dead+" brave members of my crew.",
			"We struggle to find the resources we need to keep the crew alive. We have lost "+dead+". I fear we will lose even more.",
			deadCaps+" have died under my command of this vessel. I struggle with the responsibility - clearly, something has gone terribly wrong. What decisions should I have made differently?",
			"The shrine to the fallen has reached "+dead+" names, so far.",
			"I visited the shrine to the fallen this "+timeOfDay[Random.Range(0,timeOfDay.Length)]+". I thought about the sacrifices made by my crew to keep the rest of us alive.",
			"The list of the fallen grows.",
			"We feel the loss of the crew who have left us. "+deceasedlist[Random.Range(0,deceasedlist.Count)]+" always had a joke ready, even at inappropriate moments.",
			"I think often about those who have been lost performing dangerous missions. It was my decision to send them, because I thought it was necessary for the survival of the crew. But looking back, I wonder if I could have done something to prevent there being a need to send them in the first place.",
			"We've lost "+dead+" on this journey. Each of them weighs upon my conscience."
			
		};
		
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	void LostManyCrew(){
		DecideNameToUse ();
		string[] VignetteA = new string[]{};
		string dead = deceasedlist.Count.ToString();
		string dirooooo = crew1diro.ToUpper();
		string possssss = crew1pos.ToUpper ();
		string pronnnnn = crew1proun.ToUpper ();
		string deadCaps = dead.ToUpper(); 
		
		VignetteA = new string[]{
			"So many lives lost on this journey. Bunks are left empty. Work stations are vacant.",
			"One whole corridor in the living quarters lies vacant, unused. The crew avoid that part of the ship as if it were haunted. Perhaps it is.",
			deadCaps+" have died. How is it possible that we've lost so many?",
			"The list of names on the shrine to the fallen spans the wall.",
			"I never imagined it was possible. So far, I have lost "+dead+", and the number continues to grow.",
			"Hunger, thirst, and dangerous missions have carried off "+dead+" brave members of my crew.",
			"We struggle to find the resources we need to keep the crew alive. We have lost "+dead+". I fear we will lose even more.",
			deadCaps+" have died under my command of this vessel. I struggle with the responsibility - clearly, something has gone terribly wrong. What decisions should I have made differently?",
			"The shrine to the fallen has reached "+dead+" names, so far.",
			"My first officer tells me that the duty roster must be redesigned, to account for the growing number of people who have died.",
			"Late at night I visited the shrine to the fallen and stood for at least an hour reading the names. I'm having trouble sleeping.",
			crew1+" has fallen into a deep depression. "+pronnnnn+" had grown very close to "+deceasedlist[Random.Range(0,deceasedlist.Count)]+" before... I believe the two had been a couple.",
			"The shrine to the fallen has spilled over to cover multiple bulkhead panels. Names carved into metal by various hands are all that remain of the dead."
			
		};
		
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	void LostMostCrew(){
		DecideNameToUse ();
		string[] VignetteA = new string[]{};
		string dead = deceasedlist.Count.ToString();
		string dirooooo = crew1diro.ToUpper();
		string possssss = crew1pos.ToUpper ();
		string pronnnnn = crew1proun.ToUpper ();
		string deadCaps = dead.ToUpper(); 
		
		VignetteA = new string[]{
			"Less than twenty of us remain of a crew that was once seventy seven.",
			"Most of the ship is now empty. We run with a skeleton crew, everyone striving to keep things in order.",
			"The shrine to the fallen spans an entire wall, now. "+deadCaps+" names are on the bulkhead, more than twice the number of living crewmembers.",
			"After my first officer died, I selected "+crew1+" to fill the position. "+pronnnnn+" strives to do what is needed, despite the numbers we've lost.",
			"So many of us have been lost. And yet without exception, the crew has proven willing to go on the danerous missions needed to keep us going.",
			"Last night I did not sleep. I stood in my quarters, reviewing the list of the fallen and the causes of dead. 'Hunger'. 'Thirst'. And of course, my orders to descend to a dangerous planet.",
			"I found myself this "+timeOfDay[Random.Range(0,timeOfDay.Length)]+" standing in front of the shrine to the fallen. I'm not sure how I ended up there.",
			deceasedlist[0]+", "+deceasedlist[1]+", all the way to "+deceasedlist[deceasedlist.Count]+". Each of them were lost under my command. I struggle to find a way to keep the few of us who remain alive."
		};
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	void LastCrewMembers(){
		DecideNameToUse ();
		string[] VignetteA = new string[]{};
		string dead = deceasedlist.Count.ToString();
		string dirooooo = crew1diro.ToUpper();
		string possssss = crew1pos.ToUpper ();
		string pronnnnn = crew1proun.ToUpper ();
		string deadCaps = dead.ToUpper(); 
		string crewwwww = crewmembers.Count.ToString();
		
		VignetteA = new string[]{
			"Most of the ship's systems are run through automated processes now - systems cobbled together to account for the loss of "+dead+" crewmembers.",
			"The remaining "+crewwwww+ " crewmembers all bunk in three neighboring rooms. I remain in my own quarters.",
			"Each night the remaining "+crewwwww+" gather together in the common area. We don't have much to say, but the company helps.",
			"With all but "+crewwwww+" gone, the crew and I feel a bit like pebbles rattling around in a barrel aboard "+shipName+".",
			"So many lost. And all, my fault.",
			"The shrine to the fallen fills an entire room. "+deadCaps+" names cover the walls. I carved the last few names in myself, almost as a penance.",
			"Almost everyone is gone.",
			"With the ship on autopilot, I spend much of the time off the bridge. I am avoiding the crew, to be honest. I often end up in the shrine to the fallen.",
			deadCaps +" have died under my command. We few remaining feel like ghosts haunting these halls.",
			"With nearly everyone dead, the command structure of the ship has changed. There's no need for a first officer, little need for a captain, even.",
			"To pass the time, the crew has revived the custom of hosting "+activities[Random.Range(0,activities.Length)]+ " every week. It's a shadow of what it was when we were seventy-seven - now, just "+crewwwww+" of us are here, going through the motions.",
			"We are so few.",
			"More and more, the ship runs itself. All that's left for the few of us that remains is to maintain the systems, periodically cleaning and repairing the ship's systems.",
			"There are days when I barely see anyone. The crew often retreat into themselves - seeking distractions from what has become an increasingly private hell.",
			"Each day I wake up resolve to hold on to the remaining crew. These few, from "+crew1+" to "+crew2+", must survive.",
			crew1+"'s gaze rests upon me during the morning briefing. It holds neither anger nor sadness. We've been together for "+timePassed+" days. "+pronnnnn+" has seen all of my decisions, and yet, somehow, "+pronounToUse+" still has confidence in me.",
			"The crew, small as it is, still marshalls the will to keep going another day.",
			"I must get this tiny remaineder of my crew home. We only have "+Playerscript.distanceToEarth+" lightyears to go."
		};
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
	}
	
	void SolitaryCaptainEntry(){
		string[] VignetteA = new string[]{};
		string dead = deceasedlist.Count.ToString();
		string dirooooo = crew1diro.ToUpper();
		string possssss = crew1pos.ToUpper ();
		string pronnnnn = crew1proun.ToUpper ();
		string deadCaps = dead.ToUpper(); 
		
		VignetteA = new string[]{
			"The crew is long since gone.",
			"A single person can survive on practically nothing. I subsist on a few small rations, which will sustain me for the remainder of the journey.",
			"I spent all of today at the shrine of the fallen. I set myself to the task of remembering something about each of the "+dead+" who have died.",
			"Last night I dreamed that everyone was still alive. I wandered the halls, greeting a crewman here, collecting a ship's status report from another. I woke up, the sound of the crew's voices still echoing in my ears.",
			"I only need a few rations per day. I can endure... as long as it takes to get this ship home.",
			"Some days I do nothing - ships systems maintaining themselve as "+shipName+" barrels through space. At other times I am awake for twenty hours at a time, repairing broken systems and ironing out kinks in automated sysems. I prefer the busy days - it gives me less time to think.",
			"I have never wanted a drink more.",
			"I wander the halls, looking in to the crew's old sleeping quarters. Sometimes I read.",
			"I've programmed a very simple conversation program into the computer - it gives me at least the semblance of talking to a person, which I find I still need, after the crew is gone.",
			"I will never forget the look "+deceasedlist[deceasedlist.Count]+" gave me before the end. It was a farewell gesture. Then came the death, and I was alone.",
			"I wonder what the people on Earth will think of me when I come home, ship intact but crew long since dead.",
			"Can I make it home, when I'm completely alone.",
			deadCaps+" funerals I have presided over. In a strange way, it's a comfort that there will be no more. If I die, the ship will carry on through space... probably forever."
		};
		
		part1 = VignetteA [Random.Range (0, VignetteA.Length)];
		storyStringToAdd = part1;
		AddALine (storyStringToAdd);
		ClearParts ();
		
	}
	
	IEnumerator TypeText () {
		foreach (char letter in theStringOfTheMoment.ToCharArray()) {
			TypeWriterString += letter;
			StorySpace.text = TypeWriterString;
			
			
			yield return 0;
			if(TypeWriterString.EndsWith(".") || TypeWriterString.EndsWith(":") ){
				yield return new WaitForSeconds (.2f);
			}
						if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
						{
								yield return new WaitForSeconds	(.01f);			
						}
						else
						{
								float deltaTime = 0.0f;
								deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
								yield return new WaitForSeconds (.01f*(65/(1.0f / deltaTime)));
						}
		
			if(TypeWriterString.Length >= theStringOfTheMoment.Length){
				yield return new WaitForSeconds (1.1f);
				fadingin = false;
			}
		}      
	}



public void Line1(){
	//AddALine ("\n"+"The Far Jump has failed.");
	SAddALine (timeNow+":"+"\n"+"\n"+"The Far Jump has failed.");
}

public void Line2(){
	SAddALine("We are lost in space.");
}

public void Line3(){
	SAddALine ("I must find us a way home.");
}

void ChooseRandomDetail(){
	
	//not random, but where did we start?
	startingLocation = GameObject.Find ("Spawn Point").transform;
	endingLocation = GameObject.Find ("Sol").transform;
	
	//how many books are there?
	booksno = Random.Range (30, 90);
	
	//who has the dog?
	int g = Random.Range (1,3);
	if(g == 1){
		whoHasTheDog = LadyNames[Random.Range(0, LadyNames.Length)];
	}
	if (g == 2){
		whoHasTheDog = DudeNames[Random.Range(0, DudeNames.Length)];
	}
	whoHasTheDog +=" "+LastNames[Random.Range(0, LastNames.Length)];
}

void EnableDecisionMenu(){
	DecisionPanel.SetActive(true);
	decisionSound.Play();
	Time.timeScale = 0f;
	GameObject.Find ("Ship5").GetComponent<AudioSource>().Pause();
}

void DisableDecisionMenu(){
	DecisionPanel.SetActive(false);
	ClearDecisionVars();
	Time.timeScale = 1f;
	GameObject.Find ("Ship5").GetComponent<AudioSource>().UnPause();
}

public void Decision(string decision){
	EnableDecisionMenu();
	
	decisionType = decision;
	//types include: generic, research, research generic
	if(decisionType == "research generic"){
		ResearchDecision();
	}
	
	if(decisionType == "mission decision"){
		MissionDecision();
	}
}

void ClearDecisionVars(){
	
}

void ResearchDecision(){
	
	researching = true;
	
	//Determine the first choice
	int researchSelection = Random.Range(0,ResearchOptions.Length);
	choice1string = ResearchOptions[researchSelection];
	choice1.text = ResearchOptionsButton[researchSelection];
	
	//Subtract the first choice from the list of options. Determine the second choice
	int[] leftoverChoices = new int[ResearchOptions.Length-1];;
	for(int i=0; i<ResearchOptions.Length;i++){
		if(i<researchSelection)
		{
			leftoverChoices[i] = i;
		}
		if(i>researchSelection)
		{
			leftoverChoices[i-1]= i;
		}
	}
	int researchSelection2 = leftoverChoices[Random.Range(0,leftoverChoices.Length)];
	choice2string = ResearchOptions[researchSelection2];
	choice2.text = ResearchOptionsButton[researchSelection2];
	
	DecideNameToUse();
	
	string[] genericChoices = new string[]{
		"The engineering team handed me a brief this morning. We have the resources to improve "+choice1string+" or enhance "+choice2string+".",
		"My first officer reports that engineering is ready to launch a research project to improve the ship. I have the option of improving "+choice1string+" or "+choice2string+".",
		"Engineering reports that they're prepared to initiate a research project. We can improve "+choice1string+" or "+choice2string+".",
		"Should we enhance "+choice1string+" or "+choice2string+"? The engineering team has the resources to experiment on ways to improve the ship.",
		crew1full+" and I discussed research options to assign to the engineering team. Should I improve "+choice1string+" or enchance "+choice2string+"?",
		crew1full+" and "+crew2full+" were debating how best to make use of the research team's resources. I have the option to improve "+choice1string+" or "+choice2string+"."
	};
	
	choiceTextPrompt.text = genericChoices[Random.Range(0,genericChoices.Length)];
	
}

void MissionDecision(){
	StarScript = GameObject.Find (Playerscript.currentStar).GetComponent<starscript> ();
	DecidePlanet();
	DecideResource();
	DecideNameToUse();
	
	
	if(resourceOfTheMoment=="water")
		WaterMission();
	
	if(resourceOfTheMoment=="food")
		FoodMission();
	
	if(resourceOfTheMoment=="fuel")
		FuelMission();
	
	choice1.text = "Launch Mission\n(High Risk)";
	choice1string = "missionyes";
	choice2.text = "Leave";
	choice2string = "missionno";
}

void WaterMission(){
	
	int depth = Random.Range(50,201);
	
	string[] prompt1 = new string[]{
		"We detected signatures of water on a comet passing through the "+StarScript.StarName+" system. ",
		"A glacier on the northern continent of the planet posed an opportunity for us to harvest more water. ",
		"A glacier on the southern continent of the "+addstndth+" planet had a high concentration of liquid water.",
		"Storms on "+StarScript.StarName+" "+whichPlanetWord+" were a rare chance to collect water in high concentration.",
		"We found ice on "+StarScript.StarName+" "+whichPlanetWord+"'s moon, which the crew has named "+crewmembers[crew1no].firstName+" after "+crewmembers[crew1no].rank+" "+crewmembers[crew1no].lastName+", whose birthday is today.",
		"Scans found ice trapped in a reservoir "+depth+" meters beneath the surface.",
		"Water - liquid water - was detected on "+StarScript.StarName+" "+whichPlanetWord+"'s surface, ready for us to extract it. ",
		"'Ice!' said "+crew1+". 'Captain, I'm picking up ice on World "+whichPlanetWord+" of this system.'",
		"'Captain, our scans are picking up liquid water about "+depth+" meters under the surface of "+StarScript.StarName+" "+whichPlanetWord+",' said "+crew1+"."
	};
	
	string[] prompt2 = new string[]{
		"\n \nThis is an opportunity to fill your water reserves.",
		"\n \nThis is a chance to gather more water supplies.",
		"\n \nThis is an opportunity to obtain more water for your crew.",
		"\n \nYou could gather more water for your crew.",
		"\n \nThis is an opportunity to gather more water for your reserves."
	};
	
	choiceTextPrompt.text = prompt1[Random.Range(0,prompt1.Length)] + prompt2[Random.Range(0,prompt2.Length)];
	
}

void FoodMission(){
	string[] prompt1 = new string[]{
		"Scans of the "+waddstndth+" planet revealed foodmatter we could harvest for additional supplies.",
		"Scans of the "+waddstndth+" planet revealed evidence of nitrogen, phospohorus, and potassium, which can help to power our food synthesizers.",
		"There was a vein of foodmatter on deep in a mountain on "+currentStar+" "+whichPlanetWord+". ",
		"We detected signs of foodmatter on "+currentStar+" "+whichPlanetWord+". "
	};
	
	string[] prompt2 = new string[]{
		"\n \nThis is an opportunity to fill your food reserves.",
		"\n \nThis is a chance to gather more food supplies.",
		"\n \nThis is an opportunity to obtain more food for your crew.",
		"\n \nYou could gather more food rations for your crew.",
		"\n \nThis is an opportunity to gather more food for your reserves."
	};
	
	choiceTextPrompt.text = prompt1[Random.Range(0,prompt1.Length)] + prompt2[Random.Range(0,prompt2.Length)];
}

void MoraleMission(){}

void FuelMission(){
	
	int depth = Random.Range(50,201);
	
	string[] prompt1 = new string[]{
		"We detected ore we could process into fuel on the "+waddstndth+" planet of the "+StarScript.StarName+" system. ",
		"Beneath the southern continent of the planet we detected a vein of heavy elements. This posed an opportunity for us to harvest more fuel. ",
		"We found fuelmatter on the "+StarScript.StarName+" "+whichPlanetWord+"'s moon, named "+crewmembers[crew1no].firstName+" after "+crewmembers[crew1no].rank+" "+crewmembers[crew1no].lastName+", whose birthday is today.",
		"Scans found fuelmatter trapped in a reservoir "+depth+" meters beneath the surface.",
		"'Captain, you should see this.' said "+crew1+". 'I'm detecteing fuelmatter on planet "+whichPlanetWord+" of this system.'",
		"'Captain, sensors are picking up liquid fuelmatter about "+depth+" meters under the surface of world "+whichPlanetWord+",' said "+crew1+"." 
	};
	string[] prompt2 = new string[]{
		"\n \nThis is an opportunity to fill your fuel reserves.",
		"\n \nThis is a chance to gather more fuel supplies.",
		"\n \nThis is an opportunity to obtain more fuel for your Star Drive.",
		"\n \nYou could gather more fuel matter to power your Star Drive.",
		"\n \nThis is an opportunity to gather more fuel for your reserves."
	};
	
	choiceTextPrompt.text = prompt1[Random.Range(0,prompt1.Length)] + prompt2[Random.Range(0,prompt2.Length)];
	
}

void WormholeMission(){}

public void Choice(int choiceButton){
	DisableDecisionMenu();
	if(choiceButton ==1){actualchoice = choice1.text;}
	if(choiceButton ==2){actualchoice = choice2.text;}
	
	
	if(decisionType == "research generic"){
		Playerscript.SetResearch(actualchoice);
	}
	
	if(decisionType == "mission decision"){
		if(choiceButton == 1)
			Mission (StarScript.StarType);
	}
}

void DetermineJourneyCompletage(){
	portionOfJourneyComplete = Vector3.Distance(endingLocation.position, player.position) / Vector3.Distance(endingLocation.position,startingLocation.position);
	//Debug.Log (Vector3.Distance(endingLocation.position, player.position)+" / "+ Vector3.Distance(endingLocation.position,startingLocation.position)+" = "+portionOfJourneyComplete);
	if(portionOfJourneyComplete >.85)
		journeyStatus = "starting";
	if(portionOfJourneyComplete >.2 && portionOfJourneyComplete <=.85)
		journeyStatus = "middling";
	if(portionOfJourneyComplete >.05 && portionOfJourneyComplete <=.2)
		journeyStatus = "nearing end";
	if(portionOfJourneyComplete <=.05)
		journeyStatus = "ending";
}




}


