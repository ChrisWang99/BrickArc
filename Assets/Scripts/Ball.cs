using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rb;

    public float attractForce = 10.0f;
    public float MaxSpeed = 5.0f;
    public float MinSpeed = 3.0f;
    public float InitialSpeed = 3.0f;
    public bool active;

    // Use this for initialization
    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.onUnitSphere * InitialSpeed;

        active = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity.magnitude < MinSpeed)
            rb.velocity = rb.velocity.normalized * MinSpeed;
        else if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = rb.velocity.normalized * MaxSpeed;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attract")
        {
            Vector2 distance = collision.gameObject.transform.position - gameObject.transform.position;
            Vector2 force = attractForce * distance.normalized / (distance.magnitude * distance.magnitude);
            //Debug.Log("force: " + force.ToString());
            rb.AddForce(force);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MapArea" && active)
        {
            Debug.Log("Player Died!");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndGame();
        }
    }
}
