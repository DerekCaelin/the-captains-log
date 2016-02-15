using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class travelAccross : MonoBehaviour {
	int jumpCount;
	Vector3 loc;
	Vector3 startLoc;

	public ParticleSystem.Particle [] theStars;
	public ParticleSystem starforge;
	public GameObject stars;
	public GameObject[] starGOs;
	public List<Vector3> starPositions;

	public GameObject SpawnThing;

	bool checkedStars = false;

	int[] jumpX = {-300, -200,200,300};
	int[] jumpY = {-300, -200, 200, 300};
	// Use this for initialization
	void Start () {
	
		//starforge = stars.GetComponent<ParticleSystem>();
		//theStars = new ParticleSystem.Particle[starforge.particleCount];
		//StartCoroutine(checkStars());

				starGOs = GameObject.FindGameObjectsWithTag("OpeningStar");

						
				foreach (GameObject star in starGOs){
						starPositions.Add (star.transform.position);
				}

				starPositions.Sort((a,b) => a.z.CompareTo(b.z));

	}
	
	// Update is called once per frame
	void Update () {
			
				if(jumpCount< starPositions.Count){
					transform.position = starPositions[jumpCount];
					jumpCount += 1;
				}


		/*if(checkedStars){
			//gameObject.transform.position = theStars[1].position + new Vector3(0,5,0);



		}

		if (jumpCount < 80){

			float randX = transform.position.x + jumpX[Random.Range(0,jumpX.Length)];
			float randY = transform.position.y + jumpY[Random.Range(0,jumpY.Length)];
			float Zfor  = transform.position.z + 700;
			if(randX < -120)
				randX += 170;
			if(randX > 120)
				randX -= 170;
			if(randY < 2350)
				randY += 100;
			if(randY > 2250)
				randY -= 100;
			Vector3 blah = new Vector3(randX, randY, Zfor);
			//Vector3 placeToGo = new Vector3(400000,400000,40000);

			
			Instantiate(SpawnThing, blah-new Vector3(0,5,0),Quaternion.identity);
			transform.position = blah;

			//Debug.Log(transform.position);
			jumpCount +=1;*/



		}






	IEnumerator checkStars(){
		yield return new WaitForSeconds(.5f);
	
	}

}
