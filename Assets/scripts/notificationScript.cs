using UnityEngine;
using System.Collections;

public class notificationScript : MonoBehaviour {
	bool moving;
	bool fadingIn;
	bool fadingOut;
	public Color alpha;
	int timeToWait;
	float rate;

	// Use this for initialization
	void Start () {
	
		gameObject.GetComponent<GUIText>().fontSize = Screen.width / 50;

	}
	
	// Update is called once per frame
	void Update () {
		if (moving == true) {

			//move
			float y = transform.position.y;
			y += rate*Time.deltaTime*40;
			Vector3 thing = new Vector3(transform.position.x, y,transform.position.z);
			transform.position = thing;

			//fadein
			if(fadingIn == true){
				if (alpha.a < 1) {
					alpha.a += 2 * Time.deltaTime;
				}
				if(alpha.a >1)
				{
					fadingIn = false;
					StartCoroutine(WaitForASec());
				}
			}

			if(fadingOut == true){
				if (alpha.a > 0) {
					alpha.a -= 2 * Time.deltaTime;
				}
			}

			GetComponent<GUIText>().color = alpha;

			//
			if(alpha.a <= 0 || transform.position.y > 1.1)
				Destroy(gameObject);
			}
	}

	public IEnumerator WaitForASec(){
		yield return new WaitForSeconds (timeToWait);
		fadingOut = true;
	}

	public void go(string whatToSay){
		moving = true;
		fadingIn = true;
		GetComponent<GUIText>().fontSize = 11;
		GetComponent<GUIText>().text = whatToSay;
		timeToWait = 2;
		rate = .00025f;
	}

	public void GoLong(string whatToSay){
		moving = true;
		fadingIn = true;
		GetComponent<GUIText>().fontSize = 24;
		GetComponent<GUIText>().text = whatToSay;
		timeToWait = 60;
		transform.position = new Vector3 (.75f, 0, 0);
		rate = .001f;
	}
}
