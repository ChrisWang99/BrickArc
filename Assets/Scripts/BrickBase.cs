using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBase : MonoBehaviour {

    private float ballVelocityNormal;
    public float ballVelocityReturnStep = 0.1f;

    public GameObject BreakParticle;

    protected virtual void Start()
    {
        ballVelocityNormal = 3.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 ballVelocity = ballRigidbody2D.velocity;
            //Debug.Log("ballVelocity1: "+ ballRigidbody2D.velocity.ToString()+" "+ ballRigidbody2D.velocity.magnitude.ToString());
            if (Mathf.Abs(ballVelocity.magnitude - ballVelocityNormal) <= ballVelocityReturnStep)
                ballRigidbody2D.velocity = ballVelocity.normalized * ballVelocityNormal;
            else if (ballVelocity.magnitude > ballVelocityNormal)
                ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude - ballVelocityReturnStep);
            else if(ballVelocity.magnitude < ballVelocityNormal)
                ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude + ballVelocityReturnStep);
            //Debug.Log("ballVelocity2: " + ballRigidbody2D.velocity.ToString() + " " + ballRigidbody2D.velocity.magnitude.ToString());
            BrickEffect(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }

    protected virtual void BrickEffect(GameObject ball)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().BrickNumberReduce();
        Destroy(gameObject);
        Instantiate(BreakParticle, gameObject.transform);
    }
}
