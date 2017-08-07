using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    /// <summary>
    /// Allows the state of the player to be more verbose, which will 
    /// give us more mechanics for the game in the future. We use Ready to 
    /// tell us that this player is ready to do something, surrendered to mean
    /// the player has "died" or "given up". 
    /// </summary>
    public enum PlayerState { READY, SURRENDERED, JUMPING, STUNNED, SHOCKED, FROZEN, ENRAGED, WARY };
	
    /// <summary>
    /// Movement speed of the player, based on acceleration.
    /// </summary>
	public float movSpeed = 8;

	public float jumSpeed = 0.5f;
	public float health = 100;
	public int maxDepth = -10;
    public Vector2 startingPosition;

	private bool right; 
	private bool jumpState;

	private PlayerState playerState;
	private Animator ani;
	private Rigidbody2D rig;

	void Start () {
		rig = GetComponent <Rigidbody2D> ();
		playerState = PlayerState.READY;
		ani = GetComponent <Animator> ();

		right = true;
		jumpState = false;

        transform.position = startingPosition;
	}
	
	// Update is called once per frame
	void Update () {
		checkAlive ();
		move ();
		jump ();

	}

	private void move(){
		float hAxis = Input.GetAxis("Horizontal");
        if (playerState != PlayerState.JUMPING) {
		    rig.velocity = new Vector2(hAxis * movSpeed, 
                rig.velocity.y);
        } else {
            rig.velocity = new Vector2(rig.velocity.x + Time.deltaTime * hAxis * (movSpeed / 2), 
                rig.velocity.y);
        }
		ani.SetFloat ("speed", Mathf.Abs (hAxis));
		flip (hAxis);
	}

	private void jump() {
			if (Input.GetKeyDown(KeyCode.Space) && (playerState != PlayerState.JUMPING 
                    && playerState != PlayerState.SURRENDERED)) {

			    rig.AddForce (new Vector2 (0, jumSpeed),ForceMode2D.Impulse);
                playerState = PlayerState.JUMPING;
				jumpState = true;
				ani.SetBool ("jump", jumpState);
			} else {
                if (rig.velocity.y == 0.0f && playerState == PlayerState.JUMPING) {
                    playerState = PlayerState.READY;
				ani.SetBool ("jump", jumpState);
                }
            }
            print(playerState);
	}

	private void checkAlive(){
		if (rig.position.y <= maxDepth || health <= 0f) {
			//playerState = PlayerState.SURRENDERED;
			Debug.Log("Current State: " + playerState);
            rig.position = startingPosition;
		}

	}

    public void SetState(PlayerState newState) {
        playerState = newState;
    }

	private void flip(float horizontal){
		if (horizontal > 0 && !right || horizontal < 0 && right) {
			right = !right;

			Vector3 theScale = transform.localScale;
			theScale.x *= -1;

			transform.localScale = theScale;
		}
	}

}
