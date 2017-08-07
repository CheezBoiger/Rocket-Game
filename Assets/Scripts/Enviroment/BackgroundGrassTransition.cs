using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrassTransition : MonoBehaviour {

    public Vector3 destination = new Vector3(0.13f, -4.27f, -2.05f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
	}
}
