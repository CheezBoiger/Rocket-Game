using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour {

    public float scrollSpeed;
    private Vector2 savedOffset;
    private Vector3 startPosition;
    public float tileSizeX;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = new Vector2(Time.time * scrollSpeed, 0.0f);   
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
