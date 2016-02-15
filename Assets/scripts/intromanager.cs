using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class intromanager : MonoBehaviour {

	public Text textSpace;
	string TypeWriterString;
	public Image blackground;
	public Color blackgroundcolor = Color.black;
	public float opacity;
	public GameObject CameraM;
	public Vector3 cameraStart = new Vector3 (0,0,0);


	public float cameraZ;
	public float cameraX;

	bool blah;

	// Use this for initialization
	void Start () {
		GameObject.Find("Intro Text (1)").GetComponent<Text>().fontSize = Screen.width/30;
		GameObject.Find("Intro Text").GetComponent<Text>().fontSize = Screen.width/30;
		//gameObject.GetComponent<Animation>().Play("introanim"); 

	}

	void Update() {
		if(!blah){
			Vector3 mod = new Vector3 (cameraX,CameraM.transform.position.y,cameraZ);
			CameraM.transform.position = cameraStart + mod;
		}
		blackgroundcolor.a = opacity;
		blackground.color = blackgroundcolor;

		if(Input.GetKeyDown(KeyCode.Escape)){
			NextLevel();
		}
	}

	public void Line1(){
				
				
		StartCoroutine(TypeText ("Captain's Log: January 31, 2342"));
			
	}

	public void Line2(){	
		StartCoroutine(TypeText ("The Far Jump has failed."));
	}
	
	public void Line3(){
			StartCoroutine(TypeText("We are lost in space."));
	}
	
	public void Line4(){
		textSpace = GameObject.Find("Intro Text (1)").GetComponent<Text>();
				StartCoroutine(TypeText ("I must find us a way home."));
	}

	public void StartBob(){
		blah = true;
		Vector3 mod = new Vector3 (0,12.75f,0);
		GameObject.Find("Main Camera").transform.localEulerAngles = mod;
		GameObject.Find("Main Camera").GetComponent<Animation>().Play("camerabob21");

	}
	IEnumerator TypeText (string message) {
			
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				textSpace.text = message;
			}
		else
			{
				foreach (char letter in message) {
				AudioSource audio = GetComponent<AudioSource>();
				audio.Play();
				TypeWriterString += letter;
				textSpace.text = TypeWriterString;
				yield return 0;
				if(TypeWriterString.EndsWith(".") || TypeWriterString.EndsWith(":") ){
					yield return new WaitForSeconds (.3f);
				}
			
				float deltaTime = 0.0f;
				deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
				yield return new WaitForSeconds (.01f*(65/(1.0f / deltaTime)));
				}
			}
	}

	public void NextLevel(){

		Application.LoadLevel(Application.loadedLevel+1);
	}
	 

	void Clear(){
		textSpace.text = "";
		TypeWriterString = "";
	}


}
