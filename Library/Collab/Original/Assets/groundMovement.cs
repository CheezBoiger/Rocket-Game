using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMovement : MonoBehaviour {
	private Vector3 posA;
	private Vector3 posB;
	private Vector3 posC;

	private Vector3 nextPos;

	private Transform childTransform;

	private Transform TransformB;

	private float speed;

	// Use this for initialization
	void Start () {
		
		nextPos = posB;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void move(){
		childTransform.localPosition = Vector3.MoveTowards (childTransform.position, nextPos, speed * Time.deltaTime); 
	}
}
