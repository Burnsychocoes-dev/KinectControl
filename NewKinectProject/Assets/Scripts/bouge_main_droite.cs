using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouge_main_droite : MonoBehaviour {
    public KinectPointController kpc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (kpc.isTracked)
        {
            transform.position = kpc.transform.position;
        }

	}
}
