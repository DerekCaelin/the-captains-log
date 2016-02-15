using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class starscript : MonoBehaviour {
	public string StarName;
	public string StarType;
	public int StarTypeInt;
	public int numplanets;
	public float resourceToGive;
	public GameObject planet;
	public GameObject[] go;
	public playerscript Playerscript;
	bool createdPlanets;
	public bool Sol;
	public bool Wormhole;
	public bool winked;
	public bool currentstar;
	public Transform player;
	public bool visited;

	public Material[] starColors;

	// Use this for initialization
	void Start () {
		Playerscript = GameObject.Find ("Player").GetComponent<playerscript> ();
		player = GameObject.Find("Player").transform;


		if (Sol != true && Wormhole != true) {
				CheckForOverlap ();
				DesignStar ();

			if (Playerscript.reachedSol != true) {	
				GetComponent<Animation>().Play ("expan");
			}
		}
		if(Sol)
			GetComponent<Collider>().enabled = false;
		if(Wormhole){
			gameObject.name = StarName;
		}

	//	Winkout ();


	}
	
	// Update is called once per frame
	void Update () {
		if (Sol != true) {
			transform.Rotate (Vector3.up * Time.deltaTime * 5);
		}
	
		if (Sol==true) {
			transform.LookAt(player);

			if(Vector3.Distance(player.position, transform.position)<300 && GetComponent<Collider>().enabled != true)
			{
				GetComponent<Collider>().enabled = true;

			}
		}
	}


	public void Winkin(){
		if (Sol != true && Wormhole != true && currentstar != true) {
			if (Playerscript.reachedSol != true) {	
				GetComponent<Animation>().Play ("expan");
				winked = true;
				GetComponent<Renderer>().enabled = true;
			}
		}
	}

	public void SpecialWinkin(){
		GetComponent<Animation>().Play ("expan");
		winked = true;
		GetComponent<Renderer>().enabled = true;
	}

	public void Winkout(){
		currentstar = false;
		if (!Sol && !Wormhole) {
			if (StarName != Playerscript.starName && StarName != "Wormhole") {
				GetComponent<Animation>().Play ("shrink");
				winked = false;
			}
		}
	}

	public void DestroySelf(){
		if(Sol!= true && Wormhole != true)
		Destroy (gameObject);

	}

		void DesignStar(){
			
			//Select Star Type
			StarTypeInt = Random.Range(1,100); //randomly select star

			if(StarTypeInt >=1 && StarTypeInt <= 20){
				StarType = "waterF";
				GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
				resourceToGive = Random.Range (5f, 10f);
			}
			if(StarTypeInt >=21 && StarTypeInt <= 40){
				StarType = "foodF";
				GetComponent<Renderer>().material.SetColor("_Color", Color.green);
				resourceToGive = Random.Range (5f, 10f);
			}
			if(StarTypeInt >=41 && StarTypeInt <= 50){
				StarType = "fuelF";
				GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);	
				resourceToGive = Random.Range (25f, 35f);
			}
			if(StarTypeInt >=51 && StarTypeInt <= 55){
				StarType = "moraleF";
				GetComponent<Renderer>().material.SetColor("_Color", Color.red);
				//resourceToGive = Random.Range (20f, 30f);
			}

			if(StarTypeInt >=56 && StarTypeInt <= 100){
				StarType = "science";
				GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
				resourceToGive = 1f;
			}

		SwapColor (StarType);

			NameStar ();
			CountPlanets ();
		}

	public void ClearValue(){
	
		resourceToGive = 0;
	}

	void NameStar()//Thanks to Eric for the code!
	{
		int syllables = Random.Range (1, 4);

		string[] syllableSounds = {"ba","da","ca","ta","tra","ka","sa","sta","ra","wa","cha","fa","la","ma","na","ga","pa",
		             "be","de","ce","te","tre","ke","se","ste","re","we","che","fe","le","me","ne","ge","pe",
		             "bi","di","ci","ti","tri","ki","si","sti","ri","wi","chi","fi","li","mi","ni","gi","pi",
		             "bo","do","co","to","tro","ko","so","sto","ro","wo","cho","fo","lo","mo","no","go","po",
		             "bu","du","cu","tu","tru","ku","su","stu","ru","wu","chu","fu","lu","mu","nu","gu","pu",
					"by","dy","cy","ty","try","ky","sy","sty","ry","wy","chy","fy","ly","my","ny","gy","py", "siri","fala"};
		StarName = "";

		for(int i = 0; i < syllables; i++) {
			string vr = syllableSounds[Random.Range(0,syllableSounds.Length)];
			StarName += vr;
			if(i == (syllables-1) ) {
				StarName += "s";
			}
		}

		StarName= char.ToUpper(StarName[0]) + StarName.Substring(1); //makethe first letter uppercase
		if (Sol) {
						StarName = "Sol";
				}

		if (Wormhole) {
			StarName = "Wormhole";		
		}

		if (StarType == "moraleF" && Random.Range (0,100) < 100 ) { 
			//StarName = "A Wonderful Place";
			resourceToGive = 10f;
		}

		if (StarType == "moraleF" && Random.Range (0,100) == 77 ) { //1 in 100 chance to make the star world
			StarName = "Mara";
			resourceToGive = 100f;
		}


		gameObject.name = StarName;
	}

	void CheckForOverlap(){
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6);
		for (int i = 0; i < hitColliders.Length; i++) {

						if (hitColliders[i] != gameObject.GetComponent<Collider>()) {
								DestroySelf ();		
						}
				}
	}

	void CountPlanets(){
		numplanets = Random.Range (1, 9);
		if (Sol) {
			numplanets = 9;

		}
		if (Wormhole) {
			numplanets = 0;
			
		}

		//create list and add the planets to it
	
		//AddAllPlanets();
	}

	public void AddAllPlanets()
	{ 
		if (createdPlanets == false) {
						createdPlanets = true;
						for (int y = 1; y <= numplanets; y++) {
								GameObject planetA;
								planetA = Instantiate (planet, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
								PlanetScript pscript = planetA.GetComponent<PlanetScript> ();
								pscript.ParentTo (transform, y);
						}

						go = GameObject.FindGameObjectsWithTag ("Planet");   
				}
	}

	void HiddenBig(){
		GetComponent<Renderer>().enabled = false;
	}

	public void FadeOutPlanets(){
		for (int i = 0; i<go.Length; i++) {
			PlanetScript scriptP = go[i].gameObject.GetComponent<PlanetScript>();
			scriptP.FadeOut();
		}
	}

	void SwapColor(string startype){


		ParticleRenderer[] rends;
		rends = GetComponentsInChildren<ParticleRenderer>();
		foreach (ParticleRenderer rendy in rends){
			
			int blah;
			
			if (startype == "moraleF")
			{	blah = 0;
				rendy.material = starColors[blah];
				gameObject.GetComponent<Light>().color = Color.red;
			}
			if (startype == "foodF")
			{	blah = 2;
				rendy.material = starColors[blah];
				gameObject.GetComponent<Light>().color = Color.green;
			}
			if (startype == "waterF")
			{	blah = 1;
				rendy.material = starColors[blah];
				gameObject.GetComponent<Light>().color = Color.blue;
			}
			if (startype == "fuelF")
			{	blah = 3;
				rendy.material = starColors[blah];
				gameObject.GetComponent<Light>().color = Color.magenta;
			}
			if (startype == "science")
			{	blah = 4;
				rendy.material = starColors[blah];
				gameObject.GetComponent<Light>().color = Color.cyan;
			}



		}

	}


}

