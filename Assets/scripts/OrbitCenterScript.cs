using UnityEngine;
using System.Collections;

public class OrbitCenterScript : MonoBehaviour {

	public int Revolve;
	public GameObject Star;
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.transform.Rotate(0, Time.deltaTime * Revolve, 0);
	
	}
	
	void SetRevolutionSpeed(int RevolutionSpeed)
	{
		Revolve = RevolutionSpeed;
	}
}
