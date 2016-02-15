using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetScript : MonoBehaviour {
	
	//planet qualities
	public Texture2D[] surfaces;
	public Material[] planets;
	public int RotationSpeed;
	int a;
	int b;
	Color alpha;
	bool fadingIn;
	bool fadingOut;

	//Location
	public int randomX;
	public int randomZ;
	public int planetNum;
	bool receivedParent;

	Transform star;
		
	//Revolution Speed
	public int Rev;


		
	
	//celestial bodies
	public GameObject OrbitCenter;
	
		

	// Use this for initialization
	void Start () 
	{			
		alpha = GetComponent<Renderer>().material.color;
		alpha.a = 0;

				//make a random size
				float randomScale = Random.Range(.05f,.25f);
				gameObject.transform.localScale=new Vector3 (randomScale,randomScale,randomScale);
				
				
				//add a random material
				int randomMat = Random.Range(0,27);
				gameObject.GetComponent<Renderer>().material = planets[randomMat];

		//.renderer.sortingLayerName = "Default";
		//particleSystem.renderer.sortingOrder=4;
	}
	
	// Update is called once per frame
	void Update () {
		if(fadingIn == true)
			FadeIn();

		if (fadingOut == true)
			FadeOut ();

		if (receivedParent == true) {
			//revolve at a random speed
			transform.RotateAround (star.position, Vector3.up, Rev * Time.deltaTime);

			//rotate
			transform.Rotate(Vector3.up * Time.deltaTime * 150);
		}
	}

	public void ParentTo(Transform parentStar, int num){
		 
		int odds = Random.Range (1, 3);

		if (odds == 1) {
			a = (num)*2;
			b = 5;
		}

		if (odds == 2) {
			a = (-num)*2;
			b = -5;
		}

		if (odds == 3) {
			a = 5;
			b = (num)*2;
		}

		if (odds == 2) {
			a = -5;
			b = (-num)*3;
		}

		Vector3 offset = parentStar.position + new Vector3 (a, 0, b);
		Rev = (30/(num))^3;
		transform.localPosition = offset;
		transform.parent = parentStar;
		star = parentStar;
		receivedParent = true;
		fadingIn = true;
		//SelectLocation ();
	}

	void FadeIn(){
		fadingOut = false;
		alpha.a += .3f * Time.deltaTime;
		GetComponent<Renderer>().material.color = alpha;
		if (alpha.a > 1)
			fadingIn = false;
	}

	public void FadeOut(){
		//print ("faaaaade");
		fadingIn = false;
		fadingOut = true;
		alpha.a -= .3f * Time.deltaTime;
		GetComponent<Renderer>().material.color = alpha;
		if (alpha.a < 0)
			fadingOut = false;
	}

	void SelectLocation(){

	}


	
}
