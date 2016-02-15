using UnityEngine;
using System.Collections;

public class solscript : MonoBehaviour {
	public GameObject[] pulsars;
	public bool fadeToBlack;
	GUITexture creditsScreen;
	public Color alpha;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(){

	}

	public void EndGame(){
		GetComponent<Renderer>().enabled = false;
	}
}
