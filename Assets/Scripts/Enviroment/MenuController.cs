using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // TODO(Sofia Barraza): This is your controller, based on
        // MVC pattern, ensure that the player is able to throw up
        // the menu screen during game play. As the menu pops up,
        // the entire game should be paused, so as to prevent any weird
        // runtime bugs lulz...
		if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}
}
