using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Collider2D MapArea;
    private Rigidbody2D rb;

    public float attractForce = 10.0f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.value + 1.0f, Random.value + 1.0f) * 2, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		if (!rb.IsTouching(MapArea)) {
            Debug.Log("Player Died!");
            Destroy(gameObject);
        }
	}
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attract")
        {
            Vector2 distance = collision.gameObject.transform.position - gameObject.transform.position;
            Vector2 force = attractForce * distance.normalized / (distance.magnitude * distance.magnitude);
            Debug.Log("force: " + force.ToString());
            rb.AddForce(force);
        }
    }
}
