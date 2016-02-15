using UnityEngine;
using System.Collections;

public class coffin : MonoBehaviour {

	float startTime;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		//transform.eulerAngles = new Vector3 (0, 90, 0);
		//Vector3 shipVel = GameObject.Find ("Player").transform.GetComponent<Rigidbody>().velocity;
		rb = GetComponent<Rigidbody>();
		//float speed = GameObject.Find ("Player").GetComponent<playerscript> ().speed;
		//rb.AddForce(transform.right *-400);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= startTime + 20.0f)
			Destroy (gameObject);
	
	}
}
