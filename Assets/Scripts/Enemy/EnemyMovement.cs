using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	

	// the distance between two object
	private float distance; 
	public float speed;
	public float hp = 100;

	public float attackRadius;

	[SerializeField]
	private Transform player;

    public EnemyHUD hud;
    public GameObject deadAnim;

	private bool alive;

    private float attackCooldown;

	// Use this for initialization
	void Start () {
		alive = true;
        attackCooldown = 0.0f;
	}

	// Update is called once per frame
	void Update () {
        checkAlive();
		distance = Vector3.Distance (player.position, transform.position);
		move ();
	}

	private void move()
	{
		if (distance <= attackRadius) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, player.position, speed * Time.deltaTime);
		}
	}


    public void OnCollisionEnter2D(Collision2D collision) {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0.0f) attackCooldown = 0.0f;
        if (player) {
            if (attackCooldown <= 0.0f) {
                player.health -= 5.0f;
                attackCooldown = 3.0f;
            }
        }
    }


    public void OnCollisionStay2D(Collision2D collision) {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player) {
            player.health -= 1.0f * Time.deltaTime;
        }
    }


    public void checkAlive() {
        if (hp <= 0.0f) {
            hud.despawn = true;
            GameObject parts = Instantiate(deadAnim);
            parts.SetActive(true);
            parts.transform.position = transform.position;

            Destroy(gameObject);
        }
    }
}
