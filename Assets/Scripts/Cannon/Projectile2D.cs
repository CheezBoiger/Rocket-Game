using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Projectile2D is the abstract class that defines our projectiles in
/// the game. This will then be transferred over to our Cannon and it's
/// projectile types.
/// </summary>
public abstract class Projectile2D : MonoBehaviour {
    
    /// <summary>
    /// Owner of this Projectile. This will prevent
    /// damage if on collision.
    /// </summary>
    public GameObject owner;


    /// <summary>
    /// The Explosion.
    /// </summary>
    public GameObject explosion;


    /// <summary>
    /// Animation of this projectile. This will be used for triggered,
    /// 
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Impact Radius. It's area of effect. Used to determine how much
    /// push back the projectile will do on objects near it's impact.
    /// </summary>
    public float impactRadius = 10.0f;


    /// <summary>
    /// The force of this projectile when impacting an object. This force falls off
    /// propertionally to the size of the impact radius.
    /// Force is measured in Newtons (N).
    /// </summary>
    public float impactForce = 1000.0f;


    /// <summary>
    /// Total damage dealt on impact. 
    /// </summary>
    public float impactDamage = 100.0f;

    // Timing (in seconds) before this projectile explodes.
    public float effectiveTiming = 3.0f;
    public float currentTime = 0.0f;
    
    // Starting point of this projectile.
    private Vector3 startingPoint;


    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    protected virtual void Update() {
            // Animation and whatnot can be done here.
            Debug.DrawLine(startingPoint, transform.position, Color.grey);

            currentTime += Time.deltaTime;

            if (currentTime >= effectiveTiming) {
                // Times up, time to deal with this projectile...
                OnTimesUp();
                return;
            }
	}


    /// <summary>
    /// Update the Physics fixture.
    /// </summary>
    protected virtual void FixedUpdate() {
        // Testing.
    }


    // Used to calculate if an impact was done.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
         OnCollision(collision);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision) {
        
    }

    /// <summary>
    /// Provides action taken when a projectile collides with a
    /// GameObject.
    /// </summary>
    public abstract void OnCollision(Collision2D collision);

    /// <summary>
    /// Set the owner of this projectile.
    /// </summary>
    /// <param name="owner">Owner of this projectile.</param>
    public void SetOwner(GameObject owner) {
        this.owner = owner;
    }

    /// <summary>
    /// Let this Projectile fly!
    /// </summary>
    public virtual void Fire(Vector2 aim, float forceX, float forceY) {
        startingPoint = owner.transform.position;
        // Fire at will!
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    /// <summary>
    /// If the projectile did not manage to hit something when the time is up,
    /// handle it gracefully.
    /// </summary>
    protected abstract void OnTimesUp();
}
