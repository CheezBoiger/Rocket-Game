using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {
    public GameObject owner;

    /// <summary>
    /// The knockback force that the owner will take when firing 
    /// a round. This is measured in Newtons (N).
    /// </summary>
    public float Knockback = 1000.0f;


    public float launchMultiplier = 1.5f;

    /// <summary>
    /// Current ammunition loaded in this cannon.
    /// </summary>
    public GameObject CurrentAmmo;

    /// <summary>
    /// Cannon animation.
    /// </summary>
    public Animator anim;


    /// <summary>
    /// Current aim direction of the owner.
    /// </summary>
    public Vector2 CurrentAim;

    /// <summary>
    /// Cooldown time of this cannon in seconds.
    /// </summary>
    public float maxCoolDown = 1.0f;

    /// <summary>
    /// cooldown charge.
    /// </summary>
    private float cooldown = 0.0f;

    private bool flippedY = false;

	// Use this for initialization
	void Start () {
		if (!owner) {
            owner = transform.parent.gameObject;
            if (!owner) {
                print("No owner!");
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Check if player.
        if (owner) {
            transform.position = owner.transform.position;
            PlayerAimAssist playerInfo = owner.GetComponent<PlayerAimAssist>();
            if (playerInfo) {
                // Need aim.
                CurrentAim = playerInfo.aim;
                CurrentAim.Scale(new Vector2(1.0f / CurrentAim.magnitude,
                    1.0f / CurrentAim.magnitude)
                );

               // Debugging.
                Debug.DrawLine(transform.position, new Vector3(
                    CurrentAim.x + owner.transform.position.x, 
                    CurrentAim.y + owner.transform.position.y, 0.0f), Color.cyan
                );

                // trigonometry to find our angle of rotation.
                float angle = Mathf.Atan2(CurrentAim.y, CurrentAim.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, transform.forward);

                if ((!flippedY) && (transform.eulerAngles.z > 90.0f) && (transform.eulerAngles.z < 270.0f)) {
                    Vector3 scale = transform.localScale;
                    scale.y *= -1;
                    transform.localScale = scale;
                    flippedY = true;
                } 

                // Short Circuit to flip back the cannon.
                if ((((transform.eulerAngles.z <= 90.0f) || (transform.eulerAngles.z >= 270.0f)) && (flippedY))) {
                    Vector3 scale = transform.localScale;
                    scale.y *= -1;
                    transform.localScale = scale;
                    flippedY = false;
                }
            }
        } else {
            CurrentAim = new Vector2(transform.right.x, transform.right.y);
        }

        if (cooldown <= 0.0f) {
            if (Input.GetAxis("Fire1") >= 1) {
                Fire();              
            }
        } else {
            cooldown -= Time.deltaTime;
            if (cooldown < 0.0f) cooldown = 0.0f;
            //print(cooldown);
        }
	}


    /// <summary>
    /// 
    /// </summary>
    public void Fire() {
        if (owner) {
            Rigidbody2D rigid = owner.GetComponent<Rigidbody2D>();
            if (rigid) {
                // Must move relative to the owner's position, but push them back
                // instead.
                
                float forceX = Time.deltaTime * Knockback * CurrentAim.x;
                float forceY = Time.deltaTime * Knockback * CurrentAim.y;
                
                rigid.velocity += new Vector2(-forceX, -forceY);
                // player enters jumping state when fired.
                PlayerMovement player = owner.GetComponent<PlayerMovement>();
                player.SetState(PlayerMovement.PlayerState.JUMPING);
                // Object clone sent out into the world.
                GameObject projectile = Instantiate(CurrentAmmo);
                projectile.GetComponent<Projectile2D>().enabled = true;
                projectile.GetComponent<SpriteRenderer>().enabled = true;
                projectile.GetComponent<BoxCollider2D>().enabled = true;
                projectile.GetComponent<Animator>().enabled = true;

                projectile.transform.position = new Vector3(
                    CurrentAim.x + owner.transform.position.x, 
                    CurrentAim.y + owner.transform.position.y, 0.0f
                );

                // Get projectile and transform it to be more natural looking.
                Projectile2D p = projectile.GetComponent<Projectile2D>();
                p.transform.rotation = transform.rotation;
                Vector3 scale = p.transform.localScale;
                scale.y *= (transform.localScale.y >= 0 ? 1.0f : -1.0f);
                p.transform.localScale = scale;

                p.SetOwner(owner);
                p.Fire(CurrentAim, forceX * launchMultiplier, forceY * launchMultiplier);
                
                Debug.DrawLine(transform.position, new Vector3(forceX, forceY, 0.0f), Color.green);
                //print(new Vector3(forceX, forceY, 0.0f));
            }
        }
        cooldown = maxCoolDown;
    }


    /// <summary>
    /// Get the current cooldown time on this cannon, while cooldown is greater than
    /// 0, this cannon is unable to fire.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentCoolDown() {
        return cooldown;
    }
}
