using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public GameObject robot1;
    public GameObject roboUI;
    public GameObject roboDeath;
	public PlayerMovement player;
	public float spawnTime = 0.5f;
	public Transform[] spawnPoints;
	int count = 0;
	public float distance ;
	public float spawnDist; 

	[SerializeField]
	private Transform playerT;// to get the player transformation
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 0.0f, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Spawn () {
//		if (player.health == 0) {
//			return;
//		}


			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			GameObject robo = Instantiate (robot1, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation); 
			robo.gameObject.SetActive (true);
            robo.GetComponent<EnemyMovement>().deadAnim = roboDeath;

			GameObject ui = Instantiate (roboUI);
			ui.SetActive (true);
			ui.GetComponent<EnemyHUD> ().enabled = true;
			ui.GetComponent<EnemyHUD> ().player = robo.GetComponent<EnemyMovement> ();
			robo.GetComponent<EnemyMovement> ().hud = ui.GetComponent<EnemyHUD> ();

	}
}
