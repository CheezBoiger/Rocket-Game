using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEffect : MonoBehaviour {

    public Vector2 target;
    public bool startScene;
    public AsyncOperation loadScene;

    public Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector2.Lerp(
            transform.localPosition, target, Time.deltaTime);


        if ((startScene) && (loadScene != null)) {
            print("loading!");
            if (transform.localPosition.y <= (startPosition.y / 1.1)) {
                loadScene.allowSceneActivation = true;
            }
        }
	}
}
