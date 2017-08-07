using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rocket Class script.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]
public class Rocket : Projectile2D {
    /// <summary>
    /// Continued force of our rocket.
    /// </summary>
    private Vector2 continueForce;

    /// <summary>
    /// On Collision of this rocket class.
    /// </summary>
    public override void OnCollision(Collision2D collision) {
        if (collision.gameObject != owner) {
            CreateExplosion();
            GameObject.Destroy(gameObject);
        }
    }


    public override void Fire(Vector2 aim, float forceX, float forceY) {
        base.Fire(aim, forceX, forceY);

        // the 10.0f values are hardcoded, we can set them as parameters instead lulz.
        GetComponent<Rigidbody2D>().velocity = new Vector2(aim.x * 10.0f, aim.y * 10.0f); 
        continueForce = new Vector2(forceX, forceY);
    }

    protected override void Update() {
        base.Update();
        GetComponent<Rigidbody2D>().AddForce(continueForce, ForceMode2D.Force);
    }

    /// <summary>
    /// Handle this rocket by destroying it mid air, if it does not find a target.
    /// </summary>
    protected override void OnTimesUp() {
        CreateExplosion();
        GameObject.Destroy(gameObject);
    }


    protected void CreateExplosion() {
        GameObject explode = Instantiate(explosion);
        explode.transform.position = transform.position;
        explode.GetComponent<SpriteRenderer>().enabled = true;
        explode.GetComponent<CircleCollider2D>().enabled = true;
        explode.GetComponent<Animator>().enabled = true;
        explode.GetComponent<Explosion>().enabled = true;
        explode.GetComponent<AudioSource>().enabled = true;
                
        Explosion exp = explode.GetComponent<Explosion>();
        exp.impactDamage = impactDamage;
        exp.impactForce = impactForce;
        exp.radius = impactRadius;
        exp.owner = owner;

        explode.GetComponent<Explosion>().Explode();
    }
}
