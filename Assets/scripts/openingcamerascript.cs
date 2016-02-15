using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class openingcamerascript : MonoBehaviour {

	public GUITexture black;
	public Texture logo;
	public Color alpha;
	public GUISkin OpeningSkin;
	//public GUIStyle Style;
//	public GUIStyle Style2;
	public GoogleAnalyticsV3 googleAnalytics;
	public Texture black2;
	public AudioSource startsound;
	bool ending;
	public Text BeginButton;
	public Text CaptainThing;


	//Fade From Black
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	bool sceneStarting = true;      // Whether or not the scene is still fading in.


	// Use this for initialization
	void Start () {
		//alpha.a = 100; 
		//googleAnalytics.StartSession ();
		//googleAnalytics.LogScreen("Start Screen");
		//Style.fontSize = Screen.width/20;
		//Style2.fontSize = Screen.width/20;
		BeginButton.fontSize = Screen.width/20;
		CaptainThing.fontSize = Screen.width/20;

		//if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android){
		Cursor.visible = false;
			//Cursor.lockState = CursorLockMode.Locked;
		//}
	}
	
	// Update is called once per frame
	void Update () {

		Fader ();
		Vector3 pos = transform.position;
		pos.z += 10;
		transform.position = pos;

		if (transform.position.z > 30000) {
			alpha.a += .1f * Time.deltaTime;		
			black.color = alpha;

			if(black.color.a >= .6)
			{
				pos.z = -30000;
				transform.position = pos;

			}
		}

		if (transform.position.z < 30000 && alpha.a >= 0) {
			alpha.a -= .2f * Time.deltaTime;		
			black.color = alpha;
		}
		/*
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began ) {

				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				RaycastHit hit;
			}              
		}*/

	}

	void OnGUI(){

		//GUI.Box (new Rect (.75f*Screen.width, Screen.height*.6f, 200, 100), "Begin", Style2);
		//if (GUI.Button(new Rect (.75f*Screen.width, Screen.height*.6f, 200, 100), "Begin", Style))


		//GUI.Box (new Rect (.25f*Screen.width, Screen.height*.6f, 400, 100), "The Captain's Log", Style2);
	//	GUI.Box (new Rect (.25f*Screen.width, Screen.height*.6f, 400, 100), "The Captain's Log", Style);

//		GUI.DrawTexture(new Rect (.10f*Screen.width, Screen.height*.6f, 450,100), logo);
		 	
	}

	public void Fader(){

		if (ending) {
			alpha.a += .4f * Time.deltaTime;
			black.color = alpha;
			//Debug.Log (alpha.a);
		}
		if (ending && alpha.a >= .5)

			Application.LoadLevel(1);
	}

	public void BeginClick()
	{
		//googleAnalytics.LogEvent ("Event", "Occured", "Begun", 1);
		ending = true;
		startsound.Play();
		Cursor.visible = false;
	}


}
