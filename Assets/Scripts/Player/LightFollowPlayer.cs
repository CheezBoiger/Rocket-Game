using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowPlayer : MonoBehaviour {
    public GameObject owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	   transform.position = owner.transform.position;	
       Light light = GetComponent<Light>();
       light.transform.position = new Vector3(light.transform.position.x,
            light.transform.position.y, -4.0f);
	}
}
