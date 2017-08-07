using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Aim Assist is meant for the cannon, and the user input
/// from the mouse. This will aid the direction of where the cannon 
/// is facing.
/// </summary>
public class PlayerAimAssist : MonoBehaviour {
    /// <summary>
    /// Provide an aiming parameter, tell us where you are aiming so the 
    /// cannon knows where to aim.
    /// </summary>
    public Vector2 aim;

	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update() {
        // A little heavy duty aiming scheme. 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, transform.position);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        float hitDist = 0.0f;
        if (plane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            Debug.DrawRay(transform.position, targetPoint, Color.red);

            Vector3 relative = ray.origin - transform.position;
            relative.Normalize();
            aim = new Vector2(relative.x, relative.y);
            //print(aim);
            Debug.DrawRay(transform.position, aim, Color.yellow);
        }
	}


    private void OnCollisionEnter2D(Collision2D collision) {
    }
}
