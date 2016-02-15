using UnityEngine;
using System.Collections;

public class ring : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().sortingOrder = 5;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (0,.02f,0);

	}
}
