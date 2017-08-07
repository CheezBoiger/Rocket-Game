using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMovement : MonoBehaviour {
	private Vector3 posA;
	private Vector3 posB;

	[SerializeField]
	private Vector3 nextPos;
	[SerializeField]
	private Transform childTransform;

	[SerializeField]
	private Transform TransformB;


	[SerializeField]
	private float speed;

	// Use this for initialization
	void Start () {
		posA = childTransform.localPosition;
		posB = TransformB.localPosition;
		nextPos = posB;

	}
	
	// Update is called once per frame
	void Update () {
		move ();

	}
	private void move(){
		childTransform.localPosition = Vector3.MoveTowards (childTransform.localPosition, nextPos, speed * Time.deltaTime); 
		if (Vector3.Distance (childTransform.localPosition, nextPos) <= 0.1) {
			nextPos = nextPos != posA ? posA : posB;
		}
	}

}
