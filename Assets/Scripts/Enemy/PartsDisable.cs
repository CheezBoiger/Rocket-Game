using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDisable : MonoBehaviour {
    private GameObject head;
    private GameObject arm;
    private GameObject leg;
    private GameObject body;
    private GameObject button;

    private float timeToDisable = 3.0f;
    private float currTime = 0.0f;
    
	// Use this for initialization
	void Start () {
	    head = transform.FindChild("Head").gameObject;
        arm = transform.FindChild("Arm").gameObject;
        leg = transform.FindChild("Leg").gameObject;
        body = transform.FindChild("Body").gameObject;
        button = transform.FindChild("Button").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		currTime += Time.deltaTime;
        if (currTime >= timeToDisable) {
            Destroy(head);
            Destroy(arm);
            Destroy(leg);
            Destroy(body);
            Destroy(button);
            Destroy(gameObject);
        }
	}


    public void BlowUp() {
        
    }
}
