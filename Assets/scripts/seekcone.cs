using UnityEngine;
using System.Collections;

public class seekcone : MonoBehaviour {

	public Transform target;
	public GameObject[] emitters;
	float degree;


	// Use this for initialization
	void Start () {

		for (int i = 0; i<emitters.Length; i++) {
			emitters[i].GetComponent<Renderer>().sortingLayerName = "seekcone";
			emitters[i].GetComponent<Renderer>().sortingOrder=2;
		}


	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void resize(){
		//look at target (sphere, ring, two points, one point)
		transform.LookAt (target.position);

		float distance = Vector3.Distance (transform.position, target.position);
		float targetRadius = 3f * target.localScale.x;

		degree =  Mathf.Rad2Deg*Mathf.Atan(targetRadius/distance);

		Debug.Log ("distance = "+distance+", targetRadius = "+targetRadius+", degree is calculated to be "+degree);

		ActivateTheRightEmitter ();


	}

	void ActivateTheRightEmitter(){

		//turn off all other emitters
		for (int i = 0; i<emitters.Length; i++) {
			emitters[i].SetActive(false);		
		}

		//turn on corrent emitter
		if (degree == 0) {
			emitters[0].SetActive(true);
		}
		if (degree > 0 && degree <= 10) {
			emitters[1].SetActive(true);
		}
		if (degree > 10 && degree <= 20) {
			emitters[2].SetActive(true);
		}
		if (degree > 20 && degree <= 30) {
			emitters[3].SetActive(true);
		}
		if (degree > 30 && degree <= 40) {
			emitters[4].SetActive(true);
		}
		if (degree > 40 && degree <= 50) {
			emitters[5].SetActive(true);
		}
		if (degree > 50 && degree <= 60) {
			emitters[6].SetActive(true);
		}
		if (degree > 60 && degree <= 70) {
			emitters[7].SetActive(true);
		}
		if (degree > 70 && degree <= 80) {
			emitters[8].SetActive(true);
		}
		if (degree > 80 && degree <= 90) {
			emitters[9].SetActive(true);
		}
		if (degree >= 90) {
			emitters[9].SetActive(true);
		}
	}

	public void StopSeeking(){
		//turn off all other emitters
		for (int i = 0; i<emitters.Length; i++) {
			emitters[i].SetActive(false);		
		}
	}
}
