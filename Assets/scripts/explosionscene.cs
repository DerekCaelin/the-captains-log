using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class explosionscene : MonoBehaviour {
 

	public GameObject imageToTurnOn;
	public Color a;
	public Image b;

	// Use this for initialization
	void Start () {
		Cursor.visible= false;
		//StartCoroutine (PlayClip());

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel(3);
		}

		//Camera.main.fieldOfView += 2f * Time.deltaTime;

		if (Time.timeSinceLevelLoad > 4.4f) {
						b.color = a;
						
		}

		if (Time.timeSinceLevelLoad > 5.5f) {
			b.color = a;
			a.r -= .75f * Time.deltaTime;
			a.g -= .75f * Time.deltaTime;
			a.b -= .75f * Time.deltaTime;
		}
		if (Time.timeSinceLevelLoad >= 8f)
			Application.LoadLevel (2);


	
	}

	IEnumerator PlayClip(){

		yield return new WaitForSeconds(2);
		AudioSource audio = GetComponent<AudioSource>();
		
		audio.Play();

	}

}
