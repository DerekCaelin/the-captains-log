using UnityEngine;
using System.Collections;

public class earthoverlay : MonoBehaviour {

	GameObject sol;
	Vector3 screenPosB;
	playerscript PlayerSCRIPT;


	// Use this for initialization
	void Start () {
		sol = GameObject.Find ("Sol") as GameObject;
		PlayerSCRIPT = GameObject.Find ("Player").GetComponent<playerscript>();
	
	}
	
	// Update is called once per frame
	void Update () {
		screenPosB = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint (sol.transform.position);
		if (screenPosB [2] > 0) {
			Vector2 pos = new Vector2(screenPosB[0],screenPosB[1]);
			transform.position = pos;
		}
	}

	void OnMouseEnter(){
		PlayerSCRIPT.showEarthDist = true;
	}

	void OnMouseExit(){
		PlayerSCRIPT.showEarthDist = false;
	}
}
