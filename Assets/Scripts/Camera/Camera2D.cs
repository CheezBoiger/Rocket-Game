using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera projection and view.
/// </summary>
public class Camera2D : MonoBehaviour {

	public Camera cam;
    public GameObject target;

    /// <summary>
    /// Target camera to this point in space.
    /// </summary>
    public Vector3 positionWeight;
    public float SlowPan;


    public float targetOrthoSize = 10.0f;
    public float transitionSpeed = 0.5f;

    private float timeTransition = 0.0f;
    public float maxTimeTransition = 5.0f;


    private Vector3 velocity;
    public float smoothDamp = 0.5f;

    public float maxOffsetX = 10.0f;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
//        cam.allowHDR = true;
//        cam.allowMSAA = true;
    }
	
	// Update is called once per frame
    /// <summary>
    /// Updates and perfects the camera panning, in order to create a more enjoyable
    /// experience.
    /// </summary>
	void Update () {
        offset = target.transform.position - positionWeight;
        if (offset.x > maxOffsetX) {
            positionWeight.x += maxOffsetX * 2.0f;
        } else if (offset.x < -maxOffsetX) {
            positionWeight.x -= maxOffsetX * 2.0f;
        }

        // TODO(An Le): Ensure the camera positionWeight Lerps slowly as the player
        //  progresses through the level. This is dependent on the x axis offset.
        Vector4 average = (positionWeight + target.transform.position) / 2;

        if (timeTransition < maxTimeTransition) {
            Vector3 constraint = new Vector3(average.x, average.y, -10.0f);
            Vector3 pos = Vector3.Lerp(cam.transform.position, constraint, timeTransition * Time.deltaTime);
            cam.transform.position = pos;
            timeTransition += Time.deltaTime;
        } else {
            timeTransition = maxTimeTransition;
            Vector3 constraint = new Vector3(average.x, average.y, -10.0f);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, constraint, ref velocity, smoothDamp);
        }
        targetOrthoSize = 6.0f + Mathf.Abs(offset.x + offset.y + 10.0f);  
	    
        Debug.DrawLine(cam.transform.position, average, Color.yellow);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, transitionSpeed * Time.deltaTime);
	}
}
