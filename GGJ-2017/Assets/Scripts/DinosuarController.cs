﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosuarController : MonoBehaviour {

	public GameObject wavePrefab;
	[HideInInspector]
	float waveSpeed = 20f;
	[HideInInspector]
	float minVolume = 0.1f;
	[HideInInspector]
	float maxVolume = 0.5f;
	[HideInInspector]
	float maxShootDelay = 4.0f;
	[HideInInspector]
	float maxShootDelayDivider = 20.0f;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		var currentLoudness = AudioInput.MicLoudness;
		var modifiedLoudness = currentLoudness / maxVolume;
		var currentDelay = maxShootDelay / (maxShootDelayDivider * modifiedLoudness);
		var message = "Loudness = " + currentLoudness.ToString ("00.00") + ", modifiedLoudness = " + modifiedLoudness.ToString ("00.00") + 
			", currentDelay = " + currentDelay.ToString ("00.00") + 
			", timer = " + timer.ToString ("00.00");
		//Debug.Log (message);
		if (modifiedLoudness > minVolume) {
			if (timer > currentDelay) {
				timer = 0;
				ShootWave ();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			ShootWave ();
		}
	}

	void ShootWave() {
		var cloneWave = Instantiate (wavePrefab) as GameObject;
		cloneWave.transform.position = new Vector3 (transform.position.x, transform.position.y-0.2f, transform.position.z);
		cloneWave.transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x-100, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z-90));
		cloneWave.GetComponent <Rigidbody> ().velocity = transform.forward * waveSpeed;
	}
}
