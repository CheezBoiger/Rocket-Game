using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Explosion object, this represents an explosion in the 
/// game world, which will cause damage to those around it.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour {

    public AudioSource explosionSound;
    public float radius;
    public float explodeRate = 1.0f;
    public float damageFalloffRate = 5.0f;
    public GameObject owner;

    public float impactDamage = 10.0f;
    public float impactForce = 1000.0f;
    public float forceFalloffRate = 40.0f;

    private float startingRadius = 0.0f;

    private bool exploding = false;

    private List<GameObject> damaged;
    private float currentForce;
    private float currentDamage;

	// Use this for initialization
	void Start () {
        damaged = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (exploding) {
            startingRadius += explodeRate * Time.deltaTime;

            // Lower the damage and force for each iteration of the explosion
            // radius increasing.
            currentForce -= forceFalloffRate * Time.deltaTime;
            currentDamage -= damageFalloffRate * Time.deltaTime;

            if (currentForce < 0.0f) currentForce = 0.0f;
            if (currentDamage < 0.0f) currentDamage = 0.0f;

            if (startingRadius > radius) {
                Destroy(gameObject);
            }

            GetComponent<CircleCollider2D>().radius = startingRadius;
        } else {
            print("nothing");
        }
	}


    public void Explode() {
        damaged = new List<GameObject>();
        currentForce = impactForce;
        currentDamage = impactDamage;
        exploding = true;
    }


    public void OnCollisionEnter2D(Collision2D collision) {
        print("Collided!");
        if (collision.gameObject == owner) return;
        
        PlayerMovement m = collision.gameObject.GetComponent<PlayerMovement>();
        EnemyMovement em = collision.gameObject.GetComponent<EnemyMovement>();

        if (!damaged.Contains(collision.gameObject)) {
            Vector2 direction = collision.contacts[0].point 
                - new Vector2(transform.position.x, transform.position.y);

            float forceX = direction.x * currentForce;
            float forceY = direction.y * currentForce;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(forceX, forceY), 
                ForceMode2D.Impulse);

            if (m) {
                m.health -= currentDamage;
            } else if (em) {
               em.hp -= currentDamage;
            }
            
            damaged.Add(collision.gameObject);
        }
    }
}
