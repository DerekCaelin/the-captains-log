﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float turnSpeed = 4.0f;
	 Transform player;
	
	Vector3 offset;

	void Start () {
		player = GameObject.Find ("Player").transform;
		offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);
	}
	
	void Update()
	{
		if (Input.GetMouseButton (1)) {
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offset;
		}

	}
}
